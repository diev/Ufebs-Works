rem Usage: if $(ConfigurationName) == Release call PostBuild.bat
for %%i in (.) do set d=%%~nxi
set z="G:\BankApps\%d%\src.7z"
del %z%
"C:\Program Files\7-Zip\7z.exe" a %z% -xr!obj -r *.cs *.resx *.csproj
