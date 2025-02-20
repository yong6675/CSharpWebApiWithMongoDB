# Use the official ASP.NET Core runtime as the base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

# Use the official .NET Core SDK as a base image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore any dependencies
COPY ["CSharpWebApi/CSharpWebApi.csproj", "CSharpWebApi/"]
RUN dotnet restore "CSharpWebApi/CSharpWebApi.csproj"

# Copy the rest of the application code
COPY . .
RUN dotnet publish -c Release -o /app/publish

# Build the final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CSharpWebApi.dll"]
