using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Services.Departments.Dtos;
using Application.Services.Employees.Dtos;
using Application.Services.Projects.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Employees;

public class EmployeeService : ServiceBase<Employee, EmployeeDto>, IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IProjectRepository _projectRepository;
    private readonly IMappingService _mappingService;

    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IDepartmentRepository departmentRepository,
        IProjectRepository projectRepository,
        IMappingService mappingService)
        : base(employeeRepository, mappingService)
    {
        _employeeRepository = employeeRepository;
        _mappingService = mappingService;
        _projectRepository = projectRepository;
        _departmentRepository = departmentRepository;
    }

    public async Task<DomainResult<EmployeeDto>> CreateOneAsync(CreateEmployeeDto dto, CancellationToken cancellationToken = default)
    {
        var department = await _departmentRepository.GetByIdAsync(dto.DepartmentId, cancellationToken);

        if (department is null)
        {
            return DomainResult<EmployeeDto>.Failure(EmployeeErrors.InvalidDepartmentId, StatusCodes.Status400BadRequest);
        }

        var entity = _mappingService.Map<CreateEmployeeDto, Employee>(dto);

        entity.DepartmentId = department.Id;

        var createdEntity = await _employeeRepository.CreateAsync(entity, cancellationToken: cancellationToken);

        if (createdEntity is null)
        {
            return DomainResult<EmployeeDto>.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        var responseDto = _mappingService.Map<Employee, EmployeeDto>(createdEntity);

        return DomainResult<EmployeeDto>.Success(responseDto);
    }

    public async Task<DomainResult<IEnumerable<ProjectDto>>> GetProjectsByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);

        if (employee is null)
        {
            return DomainResult<IEnumerable<ProjectDto>>.Failure(
                Errors.NotFoundById(nameof(Employee), employeeId),
                StatusCodes.Status404NotFound);
        }

        if (employee.ProjectIds.Count == 0)
        {
            return DomainResult<IEnumerable<ProjectDto>>.Success([]);
        }

        var projects = await _projectRepository.GetAllAsync(
            project => employee.ProjectIds.Contains(project.Id),
            cancellationToken);

        var projectDtos = _mappingService.Map<IEnumerable<Project>, IEnumerable<ProjectDto>>(projects);

        return DomainResult<IEnumerable<ProjectDto>>.Success(projectDtos);
    }

    public async Task<DomainResult<DepartmentDto>> GetDepartmentByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);

        if (employee is null)
        {
            return DomainResult<DepartmentDto>.Failure(
                Errors.NotFoundById(nameof(Employee), employeeId),
                StatusCodes.Status404NotFound);
        }

        var department = await _departmentRepository.GetByIdAsync(employee.DepartmentId, cancellationToken);

        var departmentDto = _mappingService.Map<Department, DepartmentDto>(department);

        return DomainResult<DepartmentDto>.Success(departmentDto);
    }

    public async Task<DomainResult<EmployeeDto>> UpdateAsync(UpdateEmployeeDto dto, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(dto.Id, cancellationToken);

        if (employee is null)
        {
            return DomainResult<EmployeeDto>.Failure(
                Errors.NotFoundById(nameof(Employee), dto.Id),
                StatusCodes.Status404NotFound);
        }

        if
            (string.Equals(employee.Info.FirstName, dto.FirstName, StringComparison.InvariantCultureIgnoreCase) &&
             string.Equals(employee.Info.LastName, dto.LastName, StringComparison.InvariantCultureIgnoreCase) &&
             string.Equals(employee.Position, dto.Position, StringComparison.InvariantCultureIgnoreCase) &&
             employee.Info.BirthDate == dto.BirthDate &&
             employee.Skills.ToHashSet().SetEquals(dto.Skills))
        {
            return DomainResult<EmployeeDto>.Failure(Errors.IdenticalValues, StatusCodes.Status400BadRequest);
        }

        _mappingService.Map(dto, employee);

        employee.Info = new PersonInfo(dto.FirstName, dto.LastName, dto.BirthDate);
        employee.MarkAsUpdated();

        var updatedEntity = await _employeeRepository.UpdateAsync(employee, cancellationToken: cancellationToken);

        if (updatedEntity is null)
        {
            return DomainResult<EmployeeDto>.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        var responseDto = _mappingService.Map<Employee, EmployeeDto>(updatedEntity);

        return DomainResult<EmployeeDto>.Success(responseDto);
    }

    public async Task<DomainResult> ChangeDepartmentAsync(Guid employeeId, Guid newDepartmentId, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);

        if (employee is null)
        {
            return DomainResult.Failure(
                Errors.NotFoundById(nameof(Employee), employeeId),
                StatusCodes.Status404NotFound);
        }

        if (employee.DepartmentId == newDepartmentId)
        {
            return DomainResult.Failure(Errors.IdenticalValues, StatusCodes.Status400BadRequest);
        }

        var department = await _departmentRepository.GetByIdAsync(newDepartmentId, cancellationToken);

        if (department is null)
        {
            return DomainResult.Failure(EmployeeErrors.InvalidDepartmentId, StatusCodes.Status400BadRequest);
        }

        employee.DepartmentId = department.Id;

        employee.MarkAsUpdated();

        var updatedEmployee = await _employeeRepository.UpdateAsync(employee, cancellationToken);

        if (updatedEmployee is null)
        {
            return DomainResult.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        return DomainResult.Success();
    }

    public async Task<DomainResult> AssignProjectToEmployeeAsync(Guid employeeId, Guid projectId, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);

        if (employee is null)
        {
            return DomainResult.Failure(Errors
                .NotFoundById(nameof(Employee), employeeId),
                StatusCodes.Status404NotFound);
        }

        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        if (project is null)
        {
            return DomainResult.Failure(EmployeeErrors.InvalidProjectId, StatusCodes.Status400BadRequest);
        }

        if (!employee.ProjectIds.Add(projectId))
        {
            return DomainResult.Failure(EmployeeErrors.ProjectAlreadyAssigned, StatusCodes.Status400BadRequest);
        }

        project.EmployeeIds.Add(employeeId);

        employee.MarkAsUpdated();
        project.MarkAsUpdated();

        var updatedEmployee = await _employeeRepository.UpdateAsync(employee, cancellationToken);
        var updatedProject = await _projectRepository.UpdateAsync(project, cancellationToken);

        if (updatedEmployee is null || updatedProject is null)
        {
            return DomainResult.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        return DomainResult.Success();
    }

    public async Task<DomainResult> UnassignProjectFromEmployeeAsync(Guid employeeId, Guid projectId, CancellationToken cancellationToken = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(employeeId, cancellationToken);

        if (employee is null)
        {
            return DomainResult.Failure(Errors.NotFoundById(nameof(Employee), employeeId), StatusCodes.Status404NotFound);
        }

        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        if (project is null)
        {
            return DomainResult.Failure(EmployeeErrors.InvalidProjectId, StatusCodes.Status400BadRequest);
        }

        if (!employee.ProjectIds.Remove(projectId))
        {
            return DomainResult.Failure(EmployeeErrors.ProjectNotAssigned, StatusCodes.Status400BadRequest);
        }

        project.EmployeeIds.Remove(employeeId);

        employee.MarkAsUpdated();
        project.MarkAsUpdated();

        var updatedEmployee = await _employeeRepository.UpdateAsync(employee, cancellationToken);
        var updatedProject = await _projectRepository.UpdateAsync(project, cancellationToken);

        if (updatedEmployee is null || updatedProject is null)
        {
            return DomainResult.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        return DomainResult.Success();
    }
}