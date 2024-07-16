using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Events;
using Application.EntityManagement.Products.Queries;
using MediatR;

namespace Application.EntityManagement.Products;

public class ProductService
{
    private readonly IMediator _mediator;

    public ProductService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId,
        CancellationToken cancellationToken = default)
    {
        var productsQuery = new GetAllProductsQuery(product => product.ExternalId == externalId);

        var productResult = await _mediator.Send(productsQuery, cancellationToken);

        if (!productResult.IsSuccessful ||
            productResult.Data is null ||
            !productResult.Data.Any())
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deleteProductCommand = new DeleteProductByExternalIdCommand(externalId);

        await _mediator.Send(deleteProductCommand, cancellationToken);

        var productDeletedEvent = new ProductDeletedEvent(productResult.Data.First());

        await _mediator.Publish(productDeletedEvent, cancellationToken);

        return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
    }
}