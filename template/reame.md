1.- Command to create a classlib project
    dotnet new classlib -o Analizers
2.- Install template. Go to folder "..\working\templates\analyzer" and run the following command: 
    dotnet new install .\
    dotnet new install .\ --force
3.- Go to test folder and execute the template to create a new analyzer project
    dotnet new analyzersSolution -n solete
4.- Uninstall template. Go to folder "..\working\templates\analyzer"
    dotnet new install .\
5.- Install and use the template in Rider (Write intructions)
Note: solve the error when use check 'solution in the same folder to projects.

Resources:
https://craftbakery.dev/make-your-own-custom-netcore-template/
https://damienbod.com/2022/08/15/creating-dotnet-solution-and-project-templates/
