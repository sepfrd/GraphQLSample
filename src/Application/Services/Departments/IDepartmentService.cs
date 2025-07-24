using Application.Abstractions.Services;
using Application.Services.Departments.Dtos;
using Application.Services.Employees.Dtos;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.Departments;

public interface IDepartmentService : IServiceBase<Department, DepartmentDto>
{
    Task<DomainResult<DepartmentDto>> CreateOneAsync(CreateDepartmentDto dto, CancellationToken cancellationToken = default);

    Task<DomainResult<IEnumerable<EmployeeDto>>> GetEmployeesByDepartmentIdAsync(Guid departmentId, CancellationToken cancellationToken = default);

    Task<DomainResult<DepartmentDto>> UpdateAsync(UpdateDepartmentDto dto, CancellationToken cancellationToken = default);
}