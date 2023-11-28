using Application.Common;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Events;
using Application.EntityManagement.Products.Queries;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Products;

public class ProductService
{
    private readonly IMediator _mediator;

    public ProductService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var pagination = new Pagination();

        var productsQuery = new GetAllProductsQuery(pagination, product => product.ExternalId == externalId);

        var productResult = await _mediator.Send(productsQuery, cancellationToken);

        if (!productResult.IsSuccessful ||
            productResult.Data is null ||
            !productResult.Data.Any())
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deleteProductCommand = new DeleteProductByExternalIdCommand(externalId);

        await _mediator.Send(deleteProductCommand, cancellationToken);

        var productDeletedEvent = new ProductDeletedEvent(productResult.Data.First());

        await _mediator.Publish(productDeletedEvent, cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}