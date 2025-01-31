using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderBouncer.Application.Interfaces.OutboxPublisher;
using OrderBouncer.GoogleSheets.Interfaces;
using OrderBouncer.GoogleSheets.Interfaces.Factories;
using OrderBouncer.GoogleSheets.Interfaces.Helpers;
using OrderBouncer.GoogleSheets.Interfaces.Repositories;
using OrderBouncer.GoogleSheets.Interfaces.Services;
using OrderBouncer.GoogleSheets.Services;
using OrderBouncer.GoogleSheets.Services.Factories;
using OrderBouncer.GoogleSheets.Services.Helpers;
using OrderBouncer.GoogleSheets.Services.Repositories;

namespace OrderBouncer.GoogleSheets;

public static class GoogleSheets
{
    public static IServiceCollection AddGoogleSheets(this IServiceCollection services){
        services.AddSingleton<SheetsService>(provider => {
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

            //string[] Scopes = [SheetsService.Scope.Spreadsheets];
            GoogleCredential credential;
            string accountKeyFilePath = configuration["Settings:Google:AccountKeyFilePath"] ?? string.Empty;

            using (Stream stream = new FileStream(accountKeyFilePath, FileMode.Open, FileAccess.Read)){
                credential = GoogleCredential.FromStream(stream).CreateScoped(SheetsService.ScopeConstants.Spreadsheets);
            }

            return new SheetsService(new BaseClientService.Initializer{
                HttpClientInitializer = credential,
                ApplicationName = configuration["Settings:Google:Sheets:ApplicationName"]
            });
        });
        
        services.AddScoped<IOutboxPublisher, GoogleSheetsOutboxPublisher>();
        services.AddScoped<IGoogleSheetsRepository, GoogleSheetsRepository>();

        services.AddScoped<IRowConverterService, RowConverterService>();
        services.AddScoped<IRowConverterHelperService, RowConverterHelperService>();

        services.AddScoped<IRowFillerService, RowFillerService>();
        services.AddScoped<IRowFillerHelperService, RowFillerHelperService>();

        services.AddScoped<IRowOrganizerService, RowOrganizer>();
        services.AddScoped<IRowOrganizerHelper, RowOrganizerHelper>();

        services.AddScoped<IRowDiagramService, RowDiagramService>();
        
        services.AddScoped<IGoogleSheetsEngine, GoogleSheetsEngine>();
        
        services.AddTransient<IRowFactory, RowFactory>();
        return services;
    }
}
