using Domain.Entities;
using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class EmployeeRepository : RepositoryBase<Employee>
{
    public EmployeeRepository(IOptions<AppOptions> appOptions) : base(appOptions)
    {
    }
}