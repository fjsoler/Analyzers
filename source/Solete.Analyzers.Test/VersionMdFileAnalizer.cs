using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;
using Xunit;
using VerifyCs = Solete.Analyzers.Test.Utilities.CSharpAnalyzerVerifier<Solete.Analyzers.VersionMdFileAnalyzer, Microsoft.CodeAnalysis.Testing.EmptyCodeFixProvider>;

namespace Solete.Analyzers.Test;

public class VersionMdFileAnalyzer
{
    [Fact]
    public async void NotThrowDiagnostic()
    {
        string additionalFileName = "version.md";
        string additionalFileContent = @"
#Version Product X

##Current

##v1.3.3.3
";
        
        await VerifyCs.VerifyAnalyzerAsync(additionalFileName, additionalFileContent);
    }

}