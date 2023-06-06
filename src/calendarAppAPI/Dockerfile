#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ARG SERVICE_PATH="/src/calendarAppAPI/"
WORKDIR /src
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY "/src/calendarAppAPI/calendarAppAPI.csproj" .
COPY "/src/calendarAppAPI/" .
RUN dotnet restore "calendarAppAPI.csproj"

FROM build AS publish
RUN dotnet publish "calendarAppAPI.csproj" -c Release -o out

FROM base AS final
WORKDIR /app
COPY --from=publish /src/out .
EXPOSE 64407
ENTRYPOINT ["dotnet", "calendarAppAPI.dll"]