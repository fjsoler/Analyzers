using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Solete.Analyzers;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DataContractAttributeRequired : DiagnosticAnalyzer
{
    public const string DiagnosticId = "SR0001";

    private static readonly string Title = "Data contract attribute is required";
    private static readonly string MessageFormat = "The class '{0}' need the data contract attribute";
    private static readonly string Description = "If base class have a data contract attribute, data member attribute is required for all derived classes.";

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

        if (classSymbol.BaseType != null && !classSymbol.BaseType.GetAttributes()
                .Any(x => x.AttributeClass is { Name: nameof(DataContractAttribute) }))
            return;

        if (!classSymbol.GetAttributes().Any(x => x.AttributeClass is { Name: nameof(DataContractAttribute) }))
        {
            var diagnostic = Diagnostic.Create(DataContractAttributeRequired.Rule, classNode.GetLocation(),
                classNode.Identifier);
            context.ReportDiagnostic(diagnostic);
        }
    }
}
