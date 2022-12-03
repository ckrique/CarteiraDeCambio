#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CarteiraDeCambio/CarteiraDeCambio.csproj", "CarteiraDeCambio/"]
RUN dotnet restore "CarteiraDeCambio/CarteiraDeCambio.csproj"
COPY . .
WORKDIR "/src/CarteiraDeCambio"
RUN dotnet build "CarteiraDeCambio.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarteiraDeCambio.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarteiraDeCambio.dll"]