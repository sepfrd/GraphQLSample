using Application.Abstractions;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Common;
using Application.Services.Employees.Dtos;
using Application.Services.Projects.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Projects;

public class ProjectService : ServiceBase<Project, ProjectDto>, IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMappingService _mappingService;

    public ProjectService(
        IProjectRepository projectRepository,
        IEmployeeRepository employeeRepository,
        IMappingService mappingService)
        : base(projectRepository, mappingService)
    {
        _projectRepository = projectRepository;
        _mappingService = mappingService;
        _employeeRepository = employeeRepository;
    }

    public async Task<DomainResult<ProjectDto>> CreateOneAsync(CreateProjectDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mappingService.Map<CreateProjectDto, Project>(dto);

        var createdEntity = await _projectRepository.CreateAsync(entity, cancellationToken: cancellationToken);

        if (createdEntity is null)
        {
            return DomainResult<ProjectDto>.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        var responseDto = _mappingService.Map<Project, ProjectDto>(createdEntity);

        return DomainResult<ProjectDto>.Success(responseDto);
    }

    public async Task<DomainResult<IEnumerable<EmployeeDto>>> GetEmployeesByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        if (project is null)
        {
            return DomainResult<IEnumerable<EmployeeDto>>.Failure(
                Errors.NotFoundById(nameof(Project), projectId),
                StatusCodes.Status404NotFound);
        }

        if (project.EmployeeIds.Count == 0)
        {
            return DomainResult<IEnumerable<EmployeeDto>>.Success([]);
        }

        var employees = await _employeeRepository.GetAllAsync(
            employee => project.EmployeeIds.Contains(employee.Id),
            cancellationToken);

        var employeeDtos = _mappingService.Map<IEnumerable<Employee>, IEnumerable<EmployeeDto>>(employees);

        return DomainResult<IEnumerable<EmployeeDto>>.Success(employeeDtos);
    }

    public async Task<DomainResult<EmployeeDto>> GetManagerByProjectIdAsync(Guid projectId, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(projectId, cancellationToken);

        if (project is null)
        {
            return DomainResult<EmployeeDto>.Failure(
                Errors.NotFoundById(nameof(Project), projectId),
                StatusCodes.Status404NotFound);
        }

        var manager = await _employeeRepository.GetByIdAsync(project.ManagerId, cancellationToken);

        var employeeDto = _mappingService.Map<Employee, EmployeeDto>(manager);

        return DomainResult<EmployeeDto>.Success(employeeDto);
    }

    public async Task<DomainResult<ProjectDto>> UpdateAsync(UpdateProjectDto dto, CancellationToken cancellationToken = default)
    {
        var project = await _projectRepository.GetByIdAsync(dto.Id, cancellationToken);

        if (project is null)
        {
            return DomainResult<ProjectDto>.Failure(
                Errors.NotFoundById(nameof(Project), dto.Id),
                StatusCodes.Status404NotFound);
        }

        if (string.Equals(project.Name, dto.NewName, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(project.Description, dto.NewDescription, StringComparison.InvariantCultureIgnoreCase))
        {
            return DomainResult<ProjectDto>.Failure(Errors.IdenticalValues, StatusCodes.Status400BadRequest);
        }

        _mappingService.Map(dto, project);

        project.MarkAsUpdated();

        var updatedEntity = await _projectRepository.UpdateAsync(project, cancellationToken: cancellationToken);

        if (updatedEntity is null)
        {
            return DomainResult<ProjectDto>.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        var responseDto = _mappingService.Map<Project, ProjectDto>(updatedEntity);

        return DomainResult<ProjectDto>.Success(responseDto);
    }
}