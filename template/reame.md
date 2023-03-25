Using a Template 


1.- Install template. Go to folder "..\working\templates\analyzer" and run the following command: 
    dotnet new install .\
    dotnet new install .\ --force

2.- Go to test folder and execute the template to create a new analyzer project
    dotnet new analyzersSolution -n solete

3.- Uninstall template. Go to folder "..\working\templates\analyzer"
    dotnet new install .\

4.- Install and use the analizers solution templates in JerBrains Rider
 

Resources:
https://craftbakery.dev/make-your-own-custom-netcore-template/ (Review and test this sample, it seen that rename the project name correctly)
https://damienbod.com/2022/08/15/creating-dotnet-solution-and-project-templates/
