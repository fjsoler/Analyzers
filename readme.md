# Roslyn Analyzers

This project is a tutorial to learn to develop a rosling analizers.

Goals: TDD, Compatibility with Visual Studio (2019,2022) and Rider, unique version compatible with net framework or net core project.

## Solete.Analizer:

This project have two samples analizers:

|DiagnosticId | Severity | Description |
|-------------|----------|-------------|
|SR0001       | Error    | Data contract attribute required. If a class have a base class with data contract attribute, this class must have data contract attribute.|
|SR0002       | Error    | Data member attribute required. If a class have a data contract attribute, all public properties must have data member attribute. |

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
