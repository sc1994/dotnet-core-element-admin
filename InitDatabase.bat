@echo off
cd ElementAdmin.Infrastructure.Data
echo Why was the change made?
set /p content=
for /f "tokens=2 delims==" %%a in ('wmic OS Get localdatetime /value') do set "dt=%%a"
set time=%dt:~0,14%
dotnet ef migrations add Init_%time%_%content%
dotnet ef database update
cd ../
@pause
