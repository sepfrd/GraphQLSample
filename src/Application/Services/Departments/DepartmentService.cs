using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Services.Departments.Dtos;
using Application.Services.Employees.Dtos;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.Departments;

public class DepartmentService : ServiceBase<Department, DepartmentDto>, IDepartmentService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMappingService _mappingService;

    public DepartmentService(
        IDepartmentRepository departmentRepository,
        IEmployeeRepository employeeRepository,
        IMappingService mappingService)
        : base(departmentRepository, mappingService)
    {
        _departmentRepository = departmentRepository;
        _employeeRepository = employeeRepository;
        _mappingService = mappingService;
    }

    public async Task<DomainResult<DepartmentDto>> CreateOneAsync(CreateDepartmentDto dto, CancellationToken cancellationToken = default)
    {
        var department = _mappingService.Map<CreateDepartmentDto, Department>(dto);

        var createdEntity = await _departmentRepository.CreateAsync(department, cancellationToken: cancellationToken);

        if (createdEntity is null)
        {
            return DomainResult<DepartmentDto>.Failure(Errors.InternalServerError);
        }

        var responseDto = _mappingService.Map<Department, DepartmentDto>(createdEntity);

        return DomainResult<DepartmentDto>.Success(responseDto);
    }

    public async Task<DomainResult<IEnumerable<EmployeeDto>>> GetEmployeesByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetByIdAsync(departmentId, cancellationToken);

        if (department is null)
        {
            return DomainResult<IEnumerable<EmployeeDto>>.Failure(Errors.NotFoundById(nameof(Department), departmentId));
        }

        var employees = await _employeeRepository.GetAllAsync(
            employee => employee.DepartmentId == departmentId,
            cancellationToken);

        var employeeDtos = _mappingService.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

        return DomainResult<IEnumerable<EmployeeDto>>.Success(employeeDtos);
    }

    public async Task<DomainResult<DepartmentDto>> UpdateAsync(UpdateDepartmentDto dto, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetByIdAsync(dto.Id, cancellationToken);

        if (department is null)
        {
            return DomainResult<DepartmentDto>.Failure(Errors.NotFoundById(nameof(Department), dto.Id));
        }

        if (string.Equals(department.Name, dto.NewName, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(department.Description, dto.NewDescription, StringComparison.InvariantCultureIgnoreCase))
        {
            return DomainResult<DepartmentDto>.Failure(Errors.IdenticalValues);
        }

        _mappingService.Map(dto, department);

        department.MarkAsUpdated();

        var updatedEntity = await _departmentRepository.UpdateAsync(department, cancellationToken: cancellationToken);

        if (updatedEntity is null)
        {
            return DomainResult<DepartmentDto>.Failure(Errors.InternalServerError);
        }

        var responseDto = _mappingService.Map<Department, DepartmentDto>(updatedEntity);

        return DomainResult<DepartmentDto>.Success(responseDto);
    }
}