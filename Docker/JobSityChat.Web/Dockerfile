#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["JobSityChat.Web/JobSityChat.Web.csproj", "JobSityChat.Web/"]
COPY ["JobSityChat.Core/JobSityChat.Core.csproj", "JobSityChat.Core/"]
COPY ["JobSityChat.Entities/JobSityChat.Entities.csproj", "JobSityChat.Entities/"]
COPY ["JobSityChat.Common/JobSityChat.Common.csproj", "JobSityChat.Common/"]
COPY ["JobSityChat.Infraestructure/JobSityChat.Infraestructure.csproj", "JobSityChat.Infraestructure/"]
RUN dotnet restore "JobSityChat.Web/JobSityChat.Web.csproj"
COPY . .
WORKDIR "/src/JobSityChat.Web"
RUN dotnet build "JobSityChat.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobSityChat.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobSityChat.Web.dll"]