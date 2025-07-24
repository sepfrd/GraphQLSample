namespace Application.Services.Projects.Dtos;

public record CreateProjectDto(
    string Name,
    string Description,
    Guid ManagerId);