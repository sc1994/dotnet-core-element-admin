#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM node:9 AS nodepage
WORKDIR /usr/src/app
COPY pages/package*.json ./
RUN npm install
COPY pages ./
RUN npm run build:prod

FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["App/App.csproj", "App/"]
RUN dotnet restore "App/App.csproj"
COPY . .
WORKDIR "/src/App"
RUN dotnet build "App.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "App.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=nodepage /usr/src/wwwroot ./wwwroot

EXPOSE 5000

ENTRYPOINT ["dotnet", "App.dll"]