using Domain.Abstractions;

namespace Application.Services.Employees;

public static class EmployeeErrors
{
    public static readonly Error InvalidDepartmentId = new("Invalid DepartmentId");
    public static readonly Error InvalidProjectId = new("Invalid ProjectId");
    public static readonly Error ProjectAlreadyAssigned = new("The project is already assigned to this employee.");
    public static readonly Error ProjectNotAssigned = new("The project is not assigned to this employee.");
}