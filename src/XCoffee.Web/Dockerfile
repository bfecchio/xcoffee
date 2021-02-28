#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/XCoffee.Web/XCoffee.Web.csproj", "src/XCoffee.Web/"]
RUN dotnet restore "src/XCoffee.Web/XCoffee.Web.csproj"
COPY . .
WORKDIR "/src/src/XCoffee.Web"
RUN dotnet build "XCoffee.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "XCoffee.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "XCoffee.Web.dll"]