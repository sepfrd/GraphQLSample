using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Categories.Handlers;

public class GetAllCategoriesQueryHandler(IRepository<Category> categoryRepository)
    : BaseGetAllQueryHandler<Category>(categoryRepository);