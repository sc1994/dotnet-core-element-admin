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
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["dotnet-core-element-admin.csproj", "."]
RUN dotnet restore "dotnet-core-element-admin.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "dotnet-core-element-admin.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "dotnet-core-element-admin.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
COPY --from=nodepage /usr/src/wwwroot ./wwwroot
ENTRYPOINT ["dotnet", "dotnet-core-element-admin.dll"]