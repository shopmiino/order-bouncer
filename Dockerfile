FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy solution and restore dependencies
COPY OrderBouncer.sln ./
COPY src/OrderBouncer.Web/OrderBouncer.Web.csproj src/OrderBouncer.Web/
COPY src/OrderBouncer.Application/OrderBouncer.Application.csproj src/OrderBouncer.Application/
COPY src/OrderBouncer.Domain/OrderBouncer.Domain.csproj src/OrderBouncer.Domain/
COPY src/OrderBouncer.Infrastructure/OrderBouncer.Infrastructure.csproj src/OrderBouncer.Infrastructure/
COPY src/OrderBouncer.GoogleSheets/OrderBouncer.GoogleSheets.csproj src/OrderBouncer.GoogleSheets/
COPY src/OrderBouncer.GoogleDrive/OrderBouncer.GoogleDrive.csproj src/OrderBouncer.GoogleDrive/
COPY src/OrderBouncer.Api/OrderBouncer.Api.csproj src/OrderBouncer.Api/
COPY src/SharedKernel/SharedKernel.csproj src/SharedKernel/

RUN dotnet restore

# Copy everything and build the application
COPY . ./
RUN dotnet publish src/OrderBouncer.Web/OrderBouncer.Web.csproj -c Release -o /publish --no-restore

# Use the .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Expose port 8080
EXPOSE 8080

# Copy published files from the build stage
COPY --from=build /publish .

# Set environment variables for ASP.NET
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "OrderBouncer.Web.dll"]
