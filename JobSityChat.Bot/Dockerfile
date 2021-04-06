#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["JobSityChat.Bot/JobSityChat.Bot.csproj", "JobSityChat.Bot/"]
COPY ["JobSityChat.Core/JobSityChat.Core.csproj", "JobSityChat.Core/"]
COPY ["JobSityChat.Entities/JobSityChat.Entities.csproj", "JobSityChat.Entities/"]
COPY ["JobSityChat.Common/JobSityChat.Common.csproj", "JobSityChat.Common/"]
RUN dotnet restore "JobSityChat.Bot/JobSityChat.Bot.csproj"
COPY . .
WORKDIR "/src/JobSityChat.Bot"
RUN dotnet build "JobSityChat.Bot.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JobSityChat.Bot.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobSityChat.Bot.dll"]