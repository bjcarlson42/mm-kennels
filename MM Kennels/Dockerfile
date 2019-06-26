FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["MM Kennels/MM Kennels.csproj", "MM Kennels/"]
RUN dotnet restore "MM Kennels/MM Kennels.csproj"
COPY . .
WORKDIR "/src/MM Kennels"
RUN dotnet build "MM Kennels.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MM Kennels.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MM Kennels.dll"]
