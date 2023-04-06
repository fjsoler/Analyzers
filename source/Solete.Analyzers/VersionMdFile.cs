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
    public const string DiagnosticIdVersionMdNotExists = "SR0003";
    private const string SR0003Title = "The version.md file is mandatory that exists";
    private const string SR0003MessageFormat = "The version.md file not exist. If the file version.md exists, check the value of Build Action is 'AdditionalFiles' in its properties.";
    private const string SR0003Description = "The version.md file not exist. If the file version.md exists, check the value of Build Action is 'AdditionalFiles' in its properties.";
    
    public const string DiagnosticVersionMdTittleNotExist = "SR0004";
    private const string SR0004Title = "The version.md file have not contain the title section";
    private const string SR0004MessageFormat = "The version.md file have not contain the title section";
    private const string SR0004Description = "The version.md file have not contain the title section.";
    
    public const string DiagnosticVersionMdVersionNotExist = "SR0005";
    private const string SR0005Title = "The version.md file have not contain the version section";
    private const string SR0005MessageFormat = "The version.md file have not contain the version section";
    private const string SR0005Description = "The version.md file have not contain the version section.";
    
    private const string Category = "AditionalFileValidator";

    public static readonly DiagnosticDescriptor RuleVersionMdFileNotExist =
        new DiagnosticDescriptor(DiagnosticIdVersionMdNotExists, SR0003Title, SR0003MessageFormat, Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true, description: SR0003Description);
    
    public static readonly DiagnosticDescriptor RuleVersionMdTittleNotExist =
        new DiagnosticDescriptor(DiagnosticVersionMdTittleNotExist, SR0004Title, SR0004MessageFormat, Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true, description: SR0004Description);
    
    public static readonly DiagnosticDescriptor RuleVersionMdVersionNotExist =
        new DiagnosticDescriptor(DiagnosticVersionMdVersionNotExist, SR0005Title, SR0005MessageFormat, Category,
            DiagnosticSeverity.Error,
            isEnabledByDefault: true, description: SR0005Description);
    
    public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics => ImmutableArray.Create(
        RuleVersionMdFileNotExist,
        RuleVersionMdTittleNotExist,
        RuleVersionMdVersionNotExist
    );

    public override void Initialize(AnalysisContext context)
    {
        context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
        context.EnableConcurrentExecution();
        context.RegisterCompilationAction(compilationActionContext =>
        {
            // Check if file version.md exists
            var additionalFiles = compilationActionContext.Options.AdditionalFiles;
            AdditionalText versionMdFile = additionalFiles.FirstOrDefault(file => Path.GetFileName(file.Path).Equals("version.md"));
            
            if (versionMdFile == null)
            {
                var diagnostic = Diagnostic.Create(RuleVersionMdFileNotExist, null, null, null, null);
                compilationActionContext.ReportDiagnostic(diagnostic);
                return;
            }
            
            SourceText fileText = versionMdFile.GetText(compilationActionContext.CancellationToken);
            //TODO: Check is needed diagnostic de file version md is empty
            bool existsTittle = false;
            bool existsVersion = false;
            foreach (TextLine line in fileText.Lines)
            {
                if (line.ToString().StartsWith("# Product"))
                    existsTittle = true;
                else if (line.ToString().StartsWith("## Version"))
                    existsVersion = true;
                
                if (existsTittle && existsVersion)
                    return;
            }
            
            if (!existsTittle)
                compilationActionContext.ReportDiagnostic(
                    Diagnostic.Create(RuleVersionMdTittleNotExist, null, null, null, null));
            
            if (!existsVersion)
                compilationActionContext.ReportDiagnostic(
                    Diagnostic.Create(RuleVersionMdVersionNotExist, null, null, null, null));
        });
    }
}
