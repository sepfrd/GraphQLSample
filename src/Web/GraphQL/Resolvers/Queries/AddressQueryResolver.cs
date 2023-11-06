using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Addresses.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Resolvers.Queries;

public class AddressQueryResolver
{
    public async Task<IEnumerable<AddressDto>?> GetAddressesByUserExternalId(int userExternalId, Pagination pagination, IMediator mediator)
    {
        var query = new GetAllAddressesByUserExternalIdQuery(userExternalId, pagination);
        
        var result = await mediator.Send(query);

        return result.Data;
    }
}