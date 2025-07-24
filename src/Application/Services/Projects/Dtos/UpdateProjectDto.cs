namespace Application.Services.Projects.Dtos;

public record UpdateProjectDto(Guid Id, string NewName, string NewDescription);