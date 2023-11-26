using Application.Common;
using Application.EntityManagement.Answers.Dtos;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectionHelper
{
    public static IServiceCollection InjectApplicationLayer(this IServiceCollection services) =>
        services
            .AddMediator()
            .AddFluentValidation();

    private static IServiceCollection AddMediator(this IServiceCollection services) =>
        services
            .AddMediatR(configuration => configuration
                .RegisterServicesFromAssembly(typeof(CommandResult).Assembly));

    private static IServiceCollection AddFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<AnswerDtoValidator>();
}