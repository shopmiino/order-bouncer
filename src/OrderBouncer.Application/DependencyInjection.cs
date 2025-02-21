using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderBouncer.Application.DTOs;
using OrderBouncer.Application.Interfaces.Buffer;
using OrderBouncer.Application.Interfaces.Converters;
using OrderBouncer.Application.Interfaces.Executors;
using OrderBouncer.Application.Interfaces.Extractors;
using OrderBouncer.Application.Interfaces.Processors;
using OrderBouncer.Application.Interfaces.UseCases;
using OrderBouncer.Application.Services.Buffer;
using OrderBouncer.Application.Services.Converters;
using OrderBouncer.Application.Services.Executors;
using OrderBouncer.Application.Services.Extractor;
using OrderBouncer.Application.Services.Processors;
using OrderBouncer.Application.UseCases;
using OrderBouncer.Domain.DTOs.Base;

namespace OrderBouncer.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IOutboxExecutor, OutboxExecutor>();
        services.AddScoped<IOrderCreatedUseCase, OrderCreatedUseCase>();

        services.ConfigureBufferServices();

        services.ConfigureDtoConvertersAndUtilities()
                .ConfigureDtoConverterHelpers();

        services.AddScoped<IRequestConverterService<OrderCreatedShopifyRequestDto, OrderDto>, OrderCreatedRequestToOrderDtoConverterService>();

        return services;
    }

    public static IServiceCollection ConfigureBufferServices(this IServiceCollection services)
    {
        services.AddSingleton<ICreateRequestBufferService, CreateRequestBufferService>();
        services.AddTransient<ICreateRequestProcessorService, CreateRequestProcessorService>();

        return services;
    }

    public static IServiceCollection ConfigureDtoConvertersAndUtilities(this IServiceCollection services){
        services.AddScoped<ILineItemsConverterService<FigureDto>,SingleFigureDtoLineItemConverterService>();
        services.AddScoped<ILineItemsConverterService<FigureDto[]>,CoupleFigureDtoLineItemConverterService>();
        services.AddScoped<ILineItemsConverterService<AccessoryDto>,AccessoryDtoLineItemConverterService>();
        services.AddScoped<ILineItemsConverterService<KeychainDto>,KeychainDtoLineItemConverterService>();
        services.AddScoped<ILineItemsConverterService<PetDto>,PetDtoLineItemConverterService>();

        services.AddScoped<ILineItemsBaseConverterService, BaseDtoLineItemConverterService>();
        services.AddScoped<ILineItemExtrasConverterService, LineItemExtrasConverterService>();
        services.AddScoped<ILineItemPropertyExtractor, LineItemPropertyExtractor>();
        services.AddScoped<ILineItemsProcessorService, LineItemsProcessorService>();

        return services;
    }

    public static IServiceCollection ConfigureDtoConverterHelpers(this IServiceCollection services){
        services.AddScoped<ILineItemConverterHelperService, LineItemConverterHelperService>();
        services.AddScoped<ILineItemsProcessorHelperService, LineItemsProcessorHelperService>();
        services.AddScoped<ILineItemPropertyExtractorHelper, LineItemPropertyExtractorHelper>();

        return services;
    }

}
