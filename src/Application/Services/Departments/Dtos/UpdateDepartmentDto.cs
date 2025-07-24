namespace Application.Services.Departments.Dtos;

public record UpdateDepartmentDto(Guid Id, string NewName, string NewDescription);