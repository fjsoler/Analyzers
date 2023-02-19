using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Solete.Analyzers;

/// <summary>
/// This analyzer checks if a class has the data contract attribute and if so checks that all public properties
/// contain the data member attribute. If they do not, an compilation error is generated. 
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public class DataMemberAttributeRequired : DiagnosticAnalyzer
{
    public const string DiagnosticId = "SR0002";

    private static readonly LocalizableString Title = "Data member attribute is required";
    private static readonly LocalizableString MessageFormat = "The property '{0}' need the data member attribute";
    private static readonly LocalizableString Description = "If class have a data contract attribute, data member attribute is required for all public properties.";

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

        if (!classSymbol.GetAttributes().Any(x => x.AttributeClass is { Name: nameof(DataContractAttribute) }))
        {
            if (classSymbol.BaseType is INamedTypeSymbol baseClassSymbol)
                return;
            //Check if class has base class
            if (classSymbol.BaseType.GetAttributes().Any(x => x.AttributeClass is { Name: nameof(DataContractAttribute) }))
            {
                //Has DataContract Attribute
            }
            return;
        }

        foreach (var propertyNode in classNode.DescendantNodes().OfType<PropertyDeclarationSyntax>())
        {
            if (!propertyNode.Modifiers.Any(x => x.IsKind(SyntaxKind.PublicKeyword)))
                continue;
            
            if (propertyNode.AttributeLists.Any(x =>
                    x.DescendantNodes().OfType<IdentifierNameSyntax>()
                        .Any(y => y.Identifier.ValueText != nameof(DataMemberAttribute))))
                continue;
            
            var diagnostic = Diagnostic.Create(Rule, propertyNode.GetLocation(), propertyNode.Identifier);
            context.ReportDiagnostic(diagnostic);
        }
    }
}