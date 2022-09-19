#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestTask.Departments.Api/TestTask.Departments.Api.csproj", "TestTask.Departments.Api/"]
RUN dotnet restore "TestTask.Departments.Api/TestTask.Departments.Api.csproj"
COPY . .
WORKDIR "/src/TestTask.Departments.Api"
RUN dotnet build "TestTask.Departments.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TestTask.Departments.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TestTask.Departments.Api.dll"]