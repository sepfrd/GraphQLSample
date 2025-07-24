using Application.Abstractions.Services;
using Application.Services.Departments.Dtos;
using Application.Services.Employees.Dtos;
using Application.Services.Projects.Dtos;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.Employees;

public interface IEmployeeService : IServiceBase<Employee, EmployeeDto>
{
    Task<DomainResult<EmployeeDto>> CreateOneAsync(CreateEmployeeDto dto, CancellationToken cancellationToken = default);

    Task<DomainResult<IEnumerable<ProjectDto>>> GetProjectsByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);

    Task<DomainResult<DepartmentDto>> GetDepartmentByEmployeeIdAsync(Guid employeeId, CancellationToken cancellationToken);

    Task<DomainResult<EmployeeDto>> UpdateAsync(UpdateEmployeeDto dto, CancellationToken cancellationToken = default);

    Task<DomainResult> ChangeDepartmentAsync(Guid employeeId, Guid newDepartmentId, CancellationToken cancellationToken = default);

    Task<DomainResult> AssignProjectToEmployeeAsync(Guid employeeId, Guid projectId, CancellationToken cancellationToken = default);

    Task<DomainResult> UnassignProjectFromEmployeeAsync(Guid employeeId, Guid projectId, CancellationToken cancellationToken = default);
}