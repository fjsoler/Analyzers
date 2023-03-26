# Install template
# Delete folders that are not included in template (bin,obj,.idea)
Get-ChildItem -Recurse -Directory -Include bin,obj,.idea | Remove-Item -Recurse -Force *> D:\Javier\Projects\Analyzers\template\install.log
# Install the template
dotnet new install .\working\templates\analyzer --force >> D:\Javier\Projects\Analyzers\template\install.log