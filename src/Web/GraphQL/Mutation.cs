using Application.Services.Departments;
using Application.Services.Departments.Dtos;
using Application.Services.Employees;
using Application.Services.Employees.Dtos;
using Application.Services.Projects;
using Application.Services.Projects.Dtos;
using Domain.Abstractions;
using Infrastructure.Abstractions;
using Infrastructure.Services.AuthService.Dtos.LoginDto;

namespace Web.GraphQL;

public class Mutation
{
    public static async Task<DomainResult<string>> LoginAsync(
        [Service] IAuthService authService,
        LoginDto loginDto,
        CancellationToken cancellationToken) =>
        await authService.LogInAsync(loginDto, cancellationToken);

    public static async Task<DomainResult<EmployeeDto>> CreateEmployeeAsync(
        [Service] IEmployeeService employeeService,
        CreateEmployeeDto employeeDto,
        CancellationToken cancellationToken) =>
        await employeeService.CreateOneAsync(employeeDto, cancellationToken);

    public static async Task<DomainResult<EmployeeDto>> UpdateEmployeeAsync(
        [Service] IEmployeeService employeeService,
        UpdateEmployeeDto employeeDto,
        CancellationToken cancellationToken) =>
        await employeeService.UpdateAsync(employeeDto, cancellationToken);

    public static async Task<DomainResult> ChangeEmployeeDepartmentAsync(
        [Service] IEmployeeService employeeService,
        Guid employeeId,
        Guid newDepartmentId,
        CancellationToken cancellationToken) =>
        await employeeService.ChangeDepartmentAsync(employeeId, newDepartmentId, cancellationToken);

    public static async Task<DomainResult> AssignProjectToEmployeeAsync(
        [Service] IEmployeeService employeeService,
        Guid employeeId,
        Guid projectId,
        CancellationToken cancellationToken) =>
        await employeeService.AssignProjectToEmployeeAsync(employeeId, projectId, cancellationToken);

    public static async Task<DomainResult> UnassignProjectFromEmployeeAsync(
        [Service] IEmployeeService employeeService,
        Guid employeeId,
        Guid projectId,
        CancellationToken cancellationToken) =>
        await employeeService.UnassignProjectFromEmployeeAsync(employeeId, projectId, cancellationToken);

    public static async Task<DomainResult<ProjectDto>> CreateProjectAsync(
        [Service] IProjectService projectService,
        CreateProjectDto projectDto,
        CancellationToken cancellationToken) =>
        await projectService.CreateOneAsync(projectDto, cancellationToken);

    public static async Task<DomainResult<ProjectDto>> UpdateProjectAsync(
        [Service] IProjectService projectService,
        UpdateProjectDto projectDto,
        CancellationToken cancellationToken) =>
        await projectService.UpdateAsync(projectDto, cancellationToken);

    public static async Task<DomainResult<DepartmentDto>> CreateDepartmentAsync(
        [Service] IDepartmentService departmentService,
        CreateDepartmentDto departmentDto,
        CancellationToken cancellationToken) =>
        await departmentService.CreateOneAsync(departmentDto, cancellationToken);

    public static async Task<DomainResult<DepartmentDto>> UpdateDepartmentAsync(
        [Service] IDepartmentService departmentService,
        UpdateDepartmentDto departmentDto,
        CancellationToken cancellationToken) =>
        await departmentService.UpdateAsync(departmentDto, cancellationToken);
}