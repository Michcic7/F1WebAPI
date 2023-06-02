#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /API
COPY API/*.csproj .
RUN dotnet restore
COPY API .
RUN dotnet publish -c Release -o /publish

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS publish
WORKDIR /publish
COPY --from=build-env /publish .
EXPOSE 80

ENTRYPOINT ["dotnet", "API.dll"]