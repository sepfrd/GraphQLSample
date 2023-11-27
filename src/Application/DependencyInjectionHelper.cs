using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.Comments;
using Application.EntityManagement.Orders;
using Application.EntityManagement.Products;
using Application.EntityManagement.Questions;
using Application.EntityManagement.Roles;
using Application.EntityManagement.Users;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectionHelper
{
    public static IServiceCollection InjectApplicationLayer(this IServiceCollection services) =>
        services
            .AddMediator()
            .AddServices()
            .AddFluentValidation();

    private static IServiceCollection AddMediator(this IServiceCollection services) =>
        services
            .AddMediatR(configuration => configuration
                .RegisterServicesFromAssembly(typeof(CommandResult).Assembly));

    private static IServiceCollection AddFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<AnswerDtoValidator>();

    private static IServiceCollection AddServices(this IServiceCollection services) =>
        services
            .AddScoped<CommentService>()
            .AddScoped<OrderService>()
            .AddScoped<ProductService>()
            .AddScoped<QuestionService>()
            .AddScoped<RoleService>()
            .AddScoped<UserService>();
}