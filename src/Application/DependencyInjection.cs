using Application.Abstractions;
using Application.Commands;
using Application.Handlers;
using Application.Queries;
using Domain.Entities;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddValidatorsFromAssembly(assembly);

        services.AddTransient<ICommandHandler<CreateUserCommand, User>, CreateUserHandler>();
        services.AddTransient<ICommandHandler<LoginUserCommand, string>, LoginUserHandler>();
        services.AddTransient<IUserQueryService, UserQueryService>();

        services.AddTransient<ICommandHandler<CreateProductCommand, Product>, CreateProductHandler>();        
        services.AddTransient<IProductQueryService, ProductQueryService>();

        return services;
    }
}
