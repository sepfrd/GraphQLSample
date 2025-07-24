using Domain.Abstractions;

namespace Application.Services.Employees;

public static class EmployeeErrors
{
    public static readonly Error InvalidDepartmentId = new("400", "Invalid DepartmentId");
    public static readonly Error InvalidProjectId = new("400", "Invalid ProjectId");
    public static readonly Error ProjectAlreadyAssigned = new("400", "The project is already assigned to this employee.");
    public static readonly Error ProjectNotAssigned = new("400", "The project is not assigned to this employee.");
}