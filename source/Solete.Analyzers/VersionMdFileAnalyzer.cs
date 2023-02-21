using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;

namespace Solete.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class VersionMdFileAnalyzer : DiagnosticAnalyzer
{
    public const string DiagnosticId = "SR0003";

    private const string Title = "Check format of version.md file";
    private const string MessageFormat = "Format error of version.md file, '{0}'.";

    private const string Category = "AditionalFile";

    private static readonly DiagnosticDescriptor Rule =
        new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationStartAction(compilationStartContext =>
        {
            // // Find the additional file with the terms.
            // ImmutableArray<AdditionalText> additionalFiles = compilationStartContext.Options.AdditionalFiles;
            // AdditionalText termsFile = additionalFiles.FirstOrDefault(file => Path.GetFileName(file.Path).Equals("version.md"));
            //
            // if (termsFile != null)
            // {
            //     HashSet<string> terms = new HashSet<string>();
            //
            //     // Read the file line-by-line to get the terms.
            //     SourceText fileText = termsFile.GetText(compilationStartContext.CancellationToken);
            //     foreach (TextLine line in fileText.Lines)
            //     {
            //         terms.Add(line.ToString());
            //     }
            //
            //     // Check every named type for the invalid terms.
            //     compilationStartContext.RegisterSymbolAction(symbolAnalysisContext =>
            //         {
            //             INamedTypeSymbol namedTypeSymbol = (INamedTypeSymbol)symbolAnalysisContext.Symbol;
            //             string symbolName = namedTypeSymbol.Name;
            //
            //             foreach (string term in terms)
            //             {
            //                 if (symbolName.Contains(term))
            //                 {
            //                     symbolAnalysisContext.ReportDiagnostic(
            //                         Diagnostic.Create(Rule, namedTypeSymbol.Locations[0], term));
            //                 }
            //             }
            //         },
            //         SymbolKind.NamedType);
            // }
        });
    }
}
