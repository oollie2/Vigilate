@echo off

set "installpath=C:\Program Files\Vigilate\"

echo Building the service
dotnet clean
dotnet publish -o Release Vigilate.sln

echo Copying application to install directory
xcopy Release "%installpath%" /e /i /y
rd /s /q Release

echo Installing service
sc.exe delete "Vigilate"
sc.exe create "Vigilate" binpath="%installpath%Vigilate.exe"
sc.exe description "Vigilate" "Prevents the PC from locking without interfering with anything."

echo Service installed, can be started using services.msc