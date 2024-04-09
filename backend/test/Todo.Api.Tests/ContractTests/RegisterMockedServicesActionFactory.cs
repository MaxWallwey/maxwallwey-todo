using System;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Todo.Api.Tests.ContractTests;

public static class RegisterMockedServicesActionFactory
{
    private static void AddMockOf<T>(this IServiceCollection services) where T : class
    {
        var mock = new Mock<T> { DefaultValue = DefaultValue.Mock };
        services.AddTransient(_ => mock.Object);
    }

    public static Action<IServiceCollection> Create<T1>() where T1 : class => (services) =>
    {
        services.AddMockOf<T1>();
    };

    public static Action<IServiceCollection> Create<T1, T2>() where T1 : class where T2 : class => (services) =>
    {
        services.AddMockOf<T1>();
        services.AddMockOf<T2>();
    };

    public static Action<IServiceCollection> Create<T1, T2, T3>() where T1 : class where T2 : class where T3 : class => (services) =>
    {
        services.AddMockOf<T1>();
        services.AddMockOf<T2>();
        services.AddMockOf<T3>();
    };

    public static Action<IServiceCollection> Create<T1, T2, T3, T4>()
        where T1 : class where T2 : class where T3 : class where T4 : class => (services) =>
    {
        services.AddMockOf<T1>();
        services.AddMockOf<T2>();
        services.AddMockOf<T3>();
        services.AddMockOf<T4>();
    };

    public static Action<IServiceCollection> Create<T1, T2, T3, T4, T5>()
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class => (services) =>
    {
        services.AddMockOf<T1>();
        services.AddMockOf<T2>();
        services.AddMockOf<T3>();
        services.AddMockOf<T4>();
        services.AddMockOf<T5>();
    };

    public static Action<IServiceCollection> Create<T1, T2, T3, T4, T5, T6>()
        where T1 : class where T2 : class where T3 : class where T4 : class where T5 : class where T6 : class => (services) =>
    {
        services.AddMockOf<T1>();
        services.AddMockOf<T2>();
        services.AddMockOf<T3>();
        services.AddMockOf<T4>();
        services.AddMockOf<T5>();
        services.AddMockOf<T6>();
    };
}
