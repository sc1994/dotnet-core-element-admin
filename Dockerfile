FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["ElementAdmin.Application/ElementAdmin.Application.csproj", "ElementAdmin.Application/"]
RUN dotnet restore "ElementAdmin.Application/ElementAdmin.Application.csproj"
COPY . .
WORKDIR "/src/ElementAdmin.Application"
RUN dotnet build "ElementAdmin.Application.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ElementAdmin.Application.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ElementAdmin.Application.dll"]