# Start with a .NET 8 SDK on existing OS
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set working directory
WORKDIR /app

# Copy over source code including Models and Tests
COPY DropTablesSocial.API/ /app/DropTablesSocial.API
COPY DropTablesSocial.Data/ /app/DropTablesSocial.Data
COPY DropTablesSocial.Models/ /app/DropTablesSocial.Models 
# COPY DropTablesSocial.Tests/ /app/DropTablesSocial.Tests

# Restore dependencies for the API project
RUN dotnet restore DropTablesSocial.API/DropTablesSocial.csproj

# Run and publish .NET artifact
RUN dotnet publish DropTablesSocial.API/DropTablesSocial.csproj -c Release -o dist 

# Make new layer 
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS run 

# Set the correct environment variable for ASP.NET Core 8
ENV ASPNETCORE_HTTP_PORTS=80

WORKDIR /app

# Copy over artifacts from build layer
COPY --from=build /app/dist .

# Expose port 80
EXPOSE 80

# What to do when docker run is called
CMD ["dotnet", "DropTablesSocial.dll"]
