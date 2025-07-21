using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common;
using Infrastructure.Services.AuthService;
using Microsoft.Extensions.Options;

namespace Infrastructure.Persistence.Repositories;

public class UserRepository : RepositoryBase<User>
{
    public UserRepository(IOptions<AppOptions> appOptions) : base(appOptions)
    {
    }
}