@echo off
rem Add this section to .csproj before </Project>:

rem <Target Name="PostBuild" AfterTargets="PostBuildEvent">
rem   <Exec Command="if $(ConfigurationName) == Release call ..\PostBuild.bat" />
rem </Target>

rem --------------------------------------------------------------------
setlocal
rem Set the common path to publish projects
set pub=G:\BankApps

rem --------------------------------------------------------------------
for %%i in (.) do set project=%%~nxi

set runtime=.NET 7
find "<TargetFramework>net7.0-windows</TargetFramework>" %project%.csproj
if %errorlevel%==0 set runtime=.NET 7 WindowsDesktop

find "<ProjectReference Include="..\Corr-Lib\Corr-Lib.csproj" />" %project%.csproj
if %errorlevel%==0 set libs=%libs% Corr-Lib

rem --------------------------------------------------------------------
%1 > build.cmd (
echo @echo off
echo rem %runtime% Runtime required
echo rem Download from get.dot.net
echo rem Use "dotnet --info" to check
echo rem Use "dotnet publish" to build
echo.
echo dotnet publish %project%\%project%.csproj -o Distr\%project%
)

rem --------------------------------------------------------------------
md %pub%\%project%
set ymd=%date:~-4%-%date:~3,2%-%date:~0,2%
set pack="%pub%\%project%\src%ymd%.7z"
if exist %pack% del %pack%

set packer="C:\Program Files\7-Zip\7z.exe" a %pack% -xr!bin -xr!obj
%packer% build.cmd
del build.cmd

pushd ..
%packer% *.sln
call :pack %project% %libs%
popd
endlocal
goto :eof

rem --------------------------------------------------------------------
:pack
if /%1/ == // goto :eof
echo Pack %1
%packer% -r %1\*.cs %1\*.resx
%packer% %1\*.csproj %1\*.json %1\*.ico
shift
goto pack
