namespace Web.GraphQL.Types;

public sealed class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Authorize()
            .Description("Root Mutation That Provides Operations to Modify Data");

        descriptor
            .Field(_ => Mutation.LoginAsync(null!, null!, CancellationToken.None))
            .AllowAnonymous()
            .Description("Allows users to log in and obtain authentication credentials.\n" +
                         "Requires valid username/email and password.\n" +
                         "Returns an authentication token upon successful login.");

        descriptor
            .Field(_ => Mutation.CreateEmployeeAsync(null!, null!, CancellationToken.None))
            .Description("Allows users to create an employee");

        descriptor
            .Field(_ => Mutation.UpdateEmployeeAsync(null!, null!, CancellationToken.None))
            .Description("Updates an existing employee with the provided new data.");

        descriptor
            .Field(_ => Mutation.ChangeEmployeeDepartmentAsync(null!, Guid.Empty, Guid.Empty, CancellationToken.None))
            .Description("Changes the department of the specified employee.");

        descriptor
            .Field(_ => Mutation.AssignProjectToEmployeeAsync(null!, Guid.Empty, Guid.Empty, CancellationToken.None))
            .Description("Assigns a project to an employee.");

        descriptor
            .Field(_ => Mutation.UnassignProjectFromEmployeeAsync(null!, Guid.Empty, Guid.Empty, CancellationToken.None))
            .Description("Unassigns a project from an employee.");

        descriptor
            .Field(_ => Mutation.CreateProjectAsync(null!, null!, CancellationToken.None))
            .Description("Creates a new project with the specified information.");

        descriptor
            .Field(_ => Mutation.UpdateProjectAsync(null!, null!, CancellationToken.None))
            .Description("Updates an existing project with new details.");

        descriptor
            .Field(_ => Mutation.CreateDepartmentAsync(null!, null!, CancellationToken.None))
            .Description("Creates a new department with the provided data.");

        descriptor
            .Field(_ => Mutation.UpdateDepartmentAsync(null!, null!, CancellationToken.None))
            .Description("Updates an existing department with new values.");
    }
}