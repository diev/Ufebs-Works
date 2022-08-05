@echo off
rem Add this section to .csproj before </Project>:

rem <Target Name="PostBuild" AfterTargets="PostBuildEvent">
rem   <Exec Command="if $(ConfigurationName) == Release call Properties\PostBuild.bat" />
rem </Target>

setlocal
for %%i in (.) do set project=%%~nxi
set ymd=%date:~-4%-%date:~3,2%-%date:~0,2%

set pack="G:\BankApps\%project%\src%ymd%.7z"

set packer="C:\Program Files\7-Zip\7z.exe" a %pack% -xr!bin -xr!obj
if exist %pack% del %pack%
%1 > build.cmd (
echo @echo off
echo rem .NET 6 Runtime required
rem echo rem .NET 6 WindowsDesktop Runtime required
echo rem Download from get.dot.net
echo rem Use "dotnet --info" to check
echo rem Use "dotnet publish" to build
echo.
echo dotnet publish %project%\%project%.csproj -o Distr\%project%
)
%packer% build.cmd
del build.cmd
pushd ..

call :pack %project% Corr-Lib

popd
endlocal
goto :eof
:pack
if /%1/ == // goto :eof
echo Pack %1
%packer% -r %1\*.cs %1\*.resx
%packer% %1\*.csproj %1\*.json %1\*.ico
shift
goto pack
