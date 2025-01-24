using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.GoogleDrive.Architectors;
using OrderBouncer.GoogleDrive.Interfaces;
using OrderBouncer.GoogleDrive.Interfaces.Architectors;
using OrderBouncer.GoogleDrive.Interfaces.Helpers;
using OrderBouncer.GoogleDrive.Interfaces.Services;
using OrderBouncer.GoogleDrive.Interfaces.UseCases;
using OrderBouncer.GoogleDrive.Repositories;
using OrderBouncer.GoogleDrive.Services;
using OrderBouncer.GoogleDrive.Services.Helpers;
using OrderBouncer.GoogleDrive.UseCases;

namespace OrderBouncer.GoogleDrive;

public static class GoogleDrive
{
    public static IServiceCollection AddGoogleDrive(this IServiceCollection services){
        services.AddSingleton<DriveService>(provider => {
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

            GoogleCredential credential;
            string accountKeyFilePath = configuration["Settings:Google:AccountKeyFilePath"] ?? string.Empty;

            using (Stream stream = new FileStream(accountKeyFilePath, FileMode.Open, FileAccess.Read)){
                credential = GoogleCredential.FromStream(stream).CreateScoped(DriveService.ScopeConstants.Drive);
            }

            return new DriveService(new BaseClientService.Initializer{
                HttpClientInitializer = credential,
                ApplicationName = configuration["Settings:Google:Drive:ApplicationName"]
            });
        });

        services.AddScoped<IGoogleDriveArchitector, GoogleDriveArchitector>();

        services.AddScoped<IOutboxPublisher, GoogleDriveOutboxPublisher>();
        
        services.AddScoped<IGoogleDriveRepositoryHelper, GoogleDriveRepositoryHelper>();
        services.AddScoped<IGoogleDriveRepository, GoogleDriveRepository>();

        services.AddScoped<INamingHelperService, NamingHelperService>();

        //Open generic for usecases below
        services.AddScoped(typeof(IOneToOneUseCase<>), typeof(OneToOneUseCase<>));
        services.AddScoped(typeof(IOneToManyUseCase<>), typeof(OneToManyUseCase<>));
        services.AddScoped(typeof(IManyToOneUseCase<>), typeof(ManyToOneUseCase<>));
        services.AddScoped(typeof(IManyToManyUseCase<>), typeof(ManyToManyUseCase<>));

        services.AddScoped<IGoogleDriveArchitector, GoogleDriveArchitector>();
        services.AddScoped<IArchitectorHelperService, ArchitectorHelperService>();

        services.AddScoped<IGoogleDriveEngine, GoogleDriveEngine>();
        return services;
    }
}
