using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Common.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
{
    public EmployeeRepository(IOptions<AppOptions> appOptions) : base(appOptions)
    {
    }
}