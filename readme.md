# Roslyn Analyzers (v1.0.2)

This project is a tutorial to learn to develop a Roslyng Analizers.

Goals: TDD, compatibility with JetBrains Rider & Visual Studio 2022, Net Standard 2.0 for compatibility with net framework 4.6.2+ or net core project 5.0, 6.0, 7.0.

## What is an Analyzer?

A code analyser is software that allows you to analyse code without the need to execute it. It deals with the detection of vulnerabilities and functional errors in software in phases prior to deployment. In this example we are going to focus on the development phase and on the analysers that the Roslyn Net Compiler allows us to develop. 

## What is a Roslyn Analyzer?

Roslyn is Net compiler... 


## Why do I need an analyser?



## How develop an analyzer?

## How to test an debug analyser?

## How deploy an analyzer?

## Solete.Analizer

The version 1.0.2 contains two samples analizers:

|DiagnosticId | Severity | Description |
|-------------|----------|-------------|
|SR0001       | Error    | Data contract attribute required. If a class have a base class with data contract attribute, this class must have data contract attribute.|
|SR0002       | Error    | Data member attribute required. If a class have a data contract attribute, all public properties must have data member attribute. |
|SR0003       | Error    | The version.md file not exist. If the file version.md exists, check the value of Build Action is 'AdditionalFiles' in its properties.|
|SR0004       | Error    | The version.md file have not contain the title section.|
|SR0005       | Error    | The version.md file have not contain the version section.|

## References:

Roslyn SDK
https://github.com/dotnet/roslyn-sdk

Types of analyzers (Samples)
https://github.com/dotnet/roslyn/blob/main/docs/analyzers/Analyzer%20Samples.md

Testing analizers with xUnit 
https://github.com/dotnet/roslyn-sdk/blob/main/src/Microsoft.CodeAnalysis.Testing/README.md

Working with types in a Roslyn analyzer
https://www.meziantou.net/working-with-types-in-a-roslyn-analyzer.htm

Complex Refactoring With Roslyn Compilers
https://hackernoon.com/complex-refactoring-with-roslyn-compilers-h32n310k

Roslyn-analyzers-docs
https://roslyn-analyzers.readthedocs.io/en/latest/nuget-packages.html
