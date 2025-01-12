using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OrderBouncer.GoogleSheets;

public static class GoogleSheets
{
    public static IServiceCollection AddGoogleSheets(this IServiceCollection services){
        services.AddSingleton<SheetsService>(provider => {
            IConfiguration configuration = provider.GetRequiredService<IConfiguration>();

            string[] Scopes = [SheetsService.Scope.Spreadsheets];
            GoogleCredential credential;
            string accountKeyFilePath = configuration["Settings:Google:AccountKeyFilePath"] ?? string.Empty;

            using (Stream stream = new FileStream(accountKeyFilePath, FileMode.Open, FileAccess.Read)){
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }

            return new SheetsService(new BaseClientService.Initializer{
                HttpClientInitializer = credential,
                ApplicationName = configuration["Settings:Google:Sheets:ApplicationName"]
            });
        });
        return services;
    }
}
