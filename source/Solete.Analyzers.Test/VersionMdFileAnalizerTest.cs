using Microsoft.CodeAnalysis.Testing;
using Xunit;
using VerifyCs = Solete.Analyzers.Test.Utilities.CSharpAnalyzerVerifier<Solete.Analyzers.VersionMdFileAnalyzer, Microsoft.CodeAnalysis.Testing.EmptyCodeFixProvider>;

namespace Solete.Analyzers.Test;

public class VersionMdFileTest
{
    private const string SourceCode = "using System; namespace TestNamespace { public class NecessaryForTestingAdditionalFile {}}";

    [Fact]
    public async void NoExistsVersionMdFileShouldTrowDiagnostic()
    {
        var expectedDiagnostic = new DiagnosticResult(VersionMdFileAnalyzer.RuleVersionMdFileNotExist);
        await VerifyCs.VerifyAnalyzerAsync(SourceCode, "","",expectedDiagnostic);
    }
    
    [Fact]
    public async void CorrectVersionMdFileShouldNotThrowDiagnostic()
    {
        string additionalFileName = "version.md";
        string additionalFileContent = @"
# Product ProductName

## Current

## Version v1.3.3.3
";
        
        await VerifyCs.VerifyAnalyzerAsync(SourceCode, additionalFileName,additionalFileContent);
    }

    [Fact]
    public async void VersionMdFileWithoutTagProductShouldThrowDiagnostic()
    {
        const string additionalFileName = "version.md";
        const string additionalFileContent = @"
## Current

## Version v1.3.3.3
";
        var expectedDiagnostic = new DiagnosticResult(VersionMdFileAnalyzer.RuleVersionMdTittleNotExist);
        await VerifyCs.VerifyAnalyzerAsync(SourceCode, additionalFileName, additionalFileContent,expectedDiagnostic);
    }
    
    [Fact]
    public async void VersionMdFileWithoutTagVersionShouldThrowDiagnostic()
    {
        const string additionalFileName = "version.md";
        const string additionalFileContent = @"
# Product ProductName

## Current
";
        var expectedDiagnostic = new DiagnosticResult(VersionMdFileAnalyzer.RuleVersionMdVersionNotExist);
        await VerifyCs.VerifyAnalyzerAsync(SourceCode, additionalFileName, additionalFileContent,expectedDiagnostic);
    }
    
    //TODO: Test all messages of diagnostic in all Analizers diagnotic
}