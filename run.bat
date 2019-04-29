echo build web pages
@cd App/pages
REM call npm i
@call npm run build:prod
echo build dotnet core
@cd ../
@dotnet run
@cd ../