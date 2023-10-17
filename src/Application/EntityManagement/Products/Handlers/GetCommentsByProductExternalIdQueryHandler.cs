using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Products.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Products.Handlers;

public class GetCommentsByProductExternalIdQueryHandler
    : IRequestHandler<GetCommentsByProductExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetCommentsByProductExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetCommentsByProductExternalIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByExternalIdAsync(request.ProductExternalId, new Func<Product, object?>[]
            {
                entity => entity.Comments
            },
            cancellationToken);

        if (product is null)
        {
            return new QueryResponse
                (
                null,
                true,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (product.Comments is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Product), typeof(GetCommentsByProductExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var commentDtos = _mappingService.Map<IEnumerable<Comment>, IEnumerable<CommentDto>>(product.Comments);

        if (commentDtos is not null)
        {
            return new QueryResponse
                (
                commentDtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(Comment), typeof(GetCommentsByProductExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );

    }
}