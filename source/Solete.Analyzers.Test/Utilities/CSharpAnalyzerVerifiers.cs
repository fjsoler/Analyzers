using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;
using Microsoft.CodeAnalysis.Testing.Verifiers;

namespace Solete.Analyzers.Test.Utilities;

public static class CSharpAnalyzerVerifier<TAnalyzer,TCodeFix>
    where TAnalyzer : DiagnosticAnalyzer, new()
    where TCodeFix: CodeFixProvider, new()
{
    public static Task VerifyAnalyzerAsync(List<string> sourceFileList, ReferenceAssemblies referenceAssemblies,  params DiagnosticResult[] expected)
    {
        var test = new TestAnalyzer();
        sourceFileList.ForEach(x => test.TestState.Sources.Add(x));
        test.ReferenceAssemblies = referenceAssemblies;
        test.ExpectedDiagnostics.AddRange(expected);
    
        return test.RunAsync();
    }

    public static Task VerifyFixProviderAsync(List<string> sourceFileList, string codeFix, ReferenceAssemblies referenceAssemblies,  params DiagnosticResult[] expected)
    {
        var test = new TestCodeFix();
        sourceFileList.ForEach(x => test.TestState.Sources.Add(x));
        test.ReferenceAssemblies = referenceAssemblies;
        test.ExpectedDiagnostics.AddRange(expected);
        test.FixedCode = codeFix;
        return test.RunAsync();
    }
    public static Task VerifyFixProviderAsync(List<string> sourceFileList, List<string> sourceCodeFixList, ReferenceAssemblies referenceAssemblies,  params DiagnosticResult[] expected)
    {
        var test = new TestCodeFix();
        sourceFileList.ForEach(x => test.TestState.Sources.Add(x));
        test.ReferenceAssemblies = referenceAssemblies;
        test.ExpectedDiagnostics.AddRange(expected);
        sourceCodeFixList.ForEach(x => test.FixedState.Sources.Add(x));
        return test.RunAsync();
    }
    // Code fix tests support both analyzer and code fix testing. This test class is derived from the code fix test
    // to avoid the need to maintain duplicate copies of the customization work.
    private class TestAnalyzer : CSharpCodeFixTest<TAnalyzer, EmptyCodeFixProvider, XUnitVerifier> {}
    
    private class TestCodeFix : CSharpCodeFixTest<TAnalyzer, TCodeFix, XUnitVerifier> {}
}

