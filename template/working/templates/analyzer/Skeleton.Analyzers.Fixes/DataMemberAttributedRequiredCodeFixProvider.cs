﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Immutable;
using System.Composition;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Skeleton.Analyzers.Fixes;

[ExportCodeFixProvider(LanguageNames.CSharp, Name = nameof(DataMemberAttributedRequiredCodeFixProvider)), Shared]
public class DataMemberAttributedRequiredCodeFixProvider : CodeFixProvider
{
    const string title = "Add data member attribute";
    
    public sealed override ImmutableArray<string> FixableDiagnosticIds
    {
        get { return ImmutableArray.Create(DataMemberAttributeRequired.DiagnosticId); }
    }

    public sealed override FixAllProvider GetFixAllProvider()
    {
        // See https://github.com/dotnet/roslyn/blob/main/docs/analyzers/FixAllProvider.md for more information on Fix All Providers
        return WellKnownFixAllProviders.BatchFixer;
    }

    public sealed override async Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        var root = await context.Document.GetSyntaxRootAsync(context.CancellationToken).ConfigureAwait(false);

        var diagnostic = context.Diagnostics.First();
        var diagnosticSpan = diagnostic.Location.SourceSpan;

        var declaration = root.FindToken(diagnosticSpan.Start).Parent.AncestorsAndSelf().OfType<PropertyDeclarationSyntax>().First();

        // Register a code action that will invoke the fix.
        // TODO: Review value equivalenceKey to check de correct behaviour 
        context.RegisterCodeFix( 
            CodeAction.Create(
                title: title,
                createChangedSolution: c => AddDataMemberAttributeAsync(context.Document, declaration, c),
                equivalenceKey: title),diagnostic);
    }

    private async Task<Solution> AddDataMemberAttributeAsync(Document document, PropertyDeclarationSyntax propertyDeclarationSyntax, CancellationToken cancellationToken)
    {
        var attributes = propertyDeclarationSyntax.AttributeLists.Add(
            SyntaxFactory.AttributeList(
                SyntaxFactory.SingletonSeparatedList(
                    SyntaxFactory.Attribute(SyntaxFactory.IdentifierName("DataMember"))
                         
                    )).WithLeadingTrivia(propertyDeclarationSyntax.GetLeadingTrivia())
            .WithTrailingTrivia(propertyDeclarationSyntax.GetTrailingTrivia()));
        
        SyntaxNode oldRoot = await document.GetSyntaxRootAsync(cancellationToken).ConfigureAwait(false);
        
        SyntaxNode newRoot = oldRoot.ReplaceNode(propertyDeclarationSyntax, propertyDeclarationSyntax.WithAttributeLists(attributes));

        return document.WithSyntaxRoot(newRoot).Project.Solution;
     }
}