using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Abstractions;
using Infrastructure.Common.Configurations;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
{
    public DepartmentRepository(IOptions<AppOptions> appOptions) : base(appOptions)
    {
    }
}