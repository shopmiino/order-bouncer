using System;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Mappings;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Application.Services.Background;
using OrderBouncer.Application.Services.Buffer;
using OrderBouncer.Application.Services.Converters;
using OrderBouncer.Application.Services.Executors;
using OrderBouncer.Application.Services.Extractors;
using OrderBouncer.Application.Services.Extractors.Profiles;
using OrderBouncer.Application.Services.Mappings;
using OrderBouncer.Application.Services.Processors;
using OrderBouncer.Application.UseCases;
using OrderBouncer.Domain.Aggregates;
using OrderBouncer.Domain.DTOs;
using OrderBouncer.Domain.DTOs.Base;
using OrderBouncer.Domain.Entities;
using OrderBouncer.Domain.Factories;
using OrderBouncer.Domain.Interfaces.Factories;

namespace OrderBouncer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration){
        services.AddScoped<IOutboxExecutor, OutboxExecutor>();
        services.AddScoped<IOrderCreatedUseCase, OrderCreatedUseCase>();

        services.AddJsonMappings()
                .AddJsonExtractors()
                .AddEntityFactories();

        services.ConfigureBufferServices()
                .ConfigureHangfire(configuration);

        services.AddScoped<IRequestConverterService<OrderCreatedShopifyRequestDto,OrderDto>,OrderCreatedRequestToOrderDtoConverterService>();

        return services;
    }

    public static IServiceCollection AddJsonMappings(this IServiceCollection services){
        services.AddScoped<IJsonMapping<Order>, OrderJsonMapping>();

        services.AddScoped<IJsonMapping<ProductEntity>, ProductJsonMapping>();
        services.AddScoped<IJsonMapping<FigureEntity>, FigureJsonMapping>();
        services.AddScoped<IJsonMapping<AccessoryEntity>, AccessoryJsonMapping>();
        services.AddScoped<IJsonMapping<PetEntity>, PetJsonMapping>();
        services.AddScoped<IJsonMapping<ImageEntity>, ImageJsonMapping>();
        services.AddScoped<IJsonMapping<NoteEntity>, NoteJsonMapping>();

    
        return services;
    }

    public static IServiceCollection AddJsonExtractors(this IServiceCollection services){
        services.AddScoped<IJsonExtractor, JsonExtractor>();

        services.AddScoped<IJsonExtractorProfile, PetExtractorProfile>();
        services.AddScoped<IJsonExtractorProfile, NoteExtractorProfile>();
        services.AddScoped<IJsonExtractorProfile, ImageExtractorProfile>();
        services.AddScoped<IJsonExtractorProfile, OrderExtractorProfile>();
        services.AddScoped<IJsonExtractorProfile, FigureExtractorProfile>();
        services.AddScoped<IJsonExtractorProfile, ProductExtractorProfile>();
        services.AddScoped<IJsonExtractorProfile, AccessoryExtractorProfile>();

        return services;
    }

    public static IServiceCollection AddEntityFactories(this IServiceCollection services){
        services.AddScoped<IEntityFactory<OrderCreateDto, Order>,OrderFactory>();
        services.AddScoped<IEntityFactory<AccessoryCreateDto, AccessoryEntity>, AccessoryFactory>();
        services.AddScoped<IEntityFactory<FigureCreateDto, FigureEntity>, FigureFactory>();
        services.AddScoped<IEntityFactory<ImageCreateDto, ImageEntity>, ImageFactory>();
        services.AddScoped<IEntityFactory<NoteCreateDto, NoteEntity>, NoteFactory>();
        services.AddScoped<IEntityFactory<PetCreateDto, PetEntity>, PetFactory>();
        services.AddScoped<IEntityFactory<ProductCreateDto, ProductEntity>, ProductFactory>();

        return services;
    }

    public static IServiceCollection ConfigureHangfire(this IServiceCollection services, IConfiguration configuration){
        string? connString = configuration["Hangfire:SQLiteStorage"];
        if (connString is null) throw new ArgumentNullException("SQLite connection string is null");

        services.AddHangfire(config => config.UseSQLiteStorage(connString));

        services.AddHangfireServer(options => {
            options.WorkerCount = 1;
        });
        services.AddHostedService<CreateRequestProcessorWorker>();

        services.AddTransient<ICreateRequestProcessorService, CreateRequestProcessorService>();

        return services;
    }

    public static IServiceCollection ConfigureBufferServices(this IServiceCollection services){
        services.AddSingleton<ICreateRequestBufferService, CreateRequestBufferService>();

        return services;
    }

    public static WebApplication ConfigureHangfireDashboard(this WebApplication app){
        app.UseHangfireDashboard();

        return app;
    }
}
