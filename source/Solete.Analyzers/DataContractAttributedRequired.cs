using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Solete.Analyzers;

/// <summary>
/// This analyzer checks if a base class has data contract attribute. All derived classes must have data contract attribute.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DataContractAttributedRequired : DiagnosticAnalyzer
{
    public const string DiagnosticId = "SR0001";

    private static readonly LocalizableString Title = "Data contract attribute is required";
    private static readonly LocalizableString MessageFormat = "The class '{0}' need the data contract attribute";
    private static readonly LocalizableString Description = "If base class have a data contract attribute, data member attribute is required for all derived classes.";

    private const string Category = "Serialization";

    public static readonly DiagnosticDescriptor Rule = new DiagnosticDescriptor(DiagnosticId, Title, MessageFormat, Category, DiagnosticSeverity.Error, isEnabledByDefault: true, description: Description);

    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(Rule);

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();

        context.RegisterSyntaxNodeAction(AnalyzeNode, SyntaxKind.ClassDeclaration);
    }

    private static void AnalyzeNode(SyntaxNodeAnalysisContext context)
    {
        if (context.Node is not ClassDeclarationSyntax classNode)
            return;

        if (context.SemanticModel.GetDeclaredSymbol(context.Node) is not ITypeSymbol classSymbol)
            return;

        if (classSymbol.BaseType == null )
            return;

        if (!classSymbol.BaseType.GetAttributes().Any(x => x.AttributeClass is { Name: nameof(DataContractAttribute) }))
            return;
        
        if (!classSymbol.GetAttributes().Any(x => x.AttributeClass is { Name: nameof(DataContractAttribute) }))
        {
            var diagnostic = Diagnostic.Create(Rule, classNode.GetLocation(), classNode.Identifier);
            context.ReportDiagnostic(diagnostic);
        }
    }
}