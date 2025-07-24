using Application.Services.Departments.Dtos;
using Application.Services.Employees.Dtos;
using Application.Services.Projects.Dtos;
using Infrastructure;
using Infrastructure.Common.Configurations;
using Infrastructure.Common.Constants;
using Infrastructure.Services.Logging;
using Microsoft.Extensions.Options;
using Web.GraphQL.Types;
using Web.GraphQL.Types.FilterTypes;

namespace Web;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var appOptions = configuration.Get<AppOptions>()!;

        services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddCors(options =>
            {
                options.AddPolicy(PolicyConstants.CorsPolicy, policyBuilder =>
                {
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            })
            .AddSingleton(Options.Create(appOptions))
            .AddSingleton<ILogger, CustomLogger>()
            .AddInfrastructure(appOptions)
            .AddAllGraphQLServices(appOptions)
            .AddHealthChecks();

        return services;
    }

    private static IServiceCollection AddAllGraphQLServices(this IServiceCollection services, AppOptions appOptions)
    {
        services
            .AddGraphQLServer()
            .AddFairyBread()
            .AddAuthorization()
            .AddMaxExecutionDepthRule(15)
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .AddType<EmployeeType>()
            .AddType<ProjectType>()
            .AddType<DepartmentType>()
            .AddType<EmployeeFilterType>()
            .AddType<SkillFilterType>()
            .AddType<DepartmentFilterType>()
            .AddType<ProjectFilterType>()
            .AddInputObjectType<CreateEmployeeDto>()
            .AddInputObjectType<UpdateEmployeeDto>()
            .AddInputObjectType<CreateProjectDto>()
            .AddInputObjectType<UpdateProjectDto>()
            .AddInputObjectType<CreateDepartmentDto>()
            .AddInputObjectType<UpdateDepartmentDto>()
            .ModifyPagingOptions(options =>
            {
                options.IncludeTotalCount = appOptions.GraphQLOptions.IncludeTotalCount;
                options.MaxPageSize = appOptions.GraphQLOptions.MaxPageSize;
            })
            .AddFiltering()
            .AddSorting()
            .ModifyRequestOptions(options =>
            {
                options.ExecutionTimeout = TimeSpan.FromSeconds(appOptions.GraphQLOptions.ExecutionTimeoutSeconds);
                options.IncludeExceptionDetails = appOptions.GraphQLOptions.IncludeExceptionDetails;
            })
            .ModifyCostOptions(options =>
            {
                options.MaxFieldCost = appOptions.GraphQLOptions.MaxFieldCost;
                options.MaxTypeCost = appOptions.GraphQLOptions.MaxTypeCost;
                options.EnforceCostLimits = appOptions.GraphQLOptions.EnforceCostLimits;
                options.ApplyCostDefaults = appOptions.GraphQLOptions.ApplyCostDefaults;
                options.DefaultResolverCost = appOptions.GraphQLOptions.DefaultResolverCost;
            })
            .DisableIntrospection(false);

        return services;
    }
}