using Application.Abstractions.Services;
using Application.Services.Employees.Dtos;
using Application.Services.Projects.Dtos;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.Services.Projects;

public interface IProjectService : IServiceBase<Project, ProjectDto>
{
    Task<DomainResult<ProjectDto>> CreateOneAsync(CreateProjectDto dto, CancellationToken cancellationToken = default);

    Task<DomainResult<IEnumerable<EmployeeDto>>> GetEmployeesByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);

    Task<DomainResult<EmployeeDto>> GetManagerByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default);

    Task<DomainResult<ProjectDto>> UpdateAsync(UpdateProjectDto dto, CancellationToken cancellationToken = default);
}