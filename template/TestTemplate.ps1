#-------------------------------------------------------------------------------
# PS Name: TestTemplate.ps1
# Author: Fco Javier Soler
# Version: 1.0 (6/4/2023)
#-------------------------------------------------------------------------------
# Description: powershell script to test the template of analyzers solution  
#-------------------------------------------------------------------------------
# Actions:
# 1.- Delete the folders and files that should not be included in the template
# 2.- Force update of install of the template
# 3.- Crate a test project
# 4.- Build de solution
# 5.- Run unit test solution
#-------------------------------------------------------------------------------
# Setup global error action
$ErrorActionPreference = "Stop"
# Display scrpt name and version
$ScriptVersion = "v1.0 (6/4/2023)" 
Write-Host “Running script: TestTemplate.ps1 $ScriptVersion”
# Saved initial path
$InitialPath = $pwd
# Delete the files that should not be included in the template
Write-Host “Deleting the files that should not be included in the template (*.user)...” -ForegroundColor Green
Get-ChildItem -include *.user | Remove-Item -Force
# Delete the folders that should not be included in the template
Write-Host “Deleting the folders that should not be included in the template (bin,obj,.idea)...” -ForegroundColor Green
Get-ChildItem -Recurse -Directory -Include bin,obj,.idea | Remove-Item -Recurse -Force
if($?)
{
    Write-Host “Folders deleted succesfully!” -ForegroundColor Green
}
else
{
    Write-Host “Error in delete comand!” -ForegroundColor Yellow
}
# Install the template
dotnet new install .\working\templates\analyzer --force
# Deleted the test solution
Set-Location -Path .\test\
Remove-Item * -Recurse -Force
# Create a new solution using the template
dotnet new analyzersSolution -n Solete
# Build the solution
Set-Location -Path .\Solete\
dotnet build
# run the unit tests
dotnet test
# return to the initial folder
Set-Location $InitialPath