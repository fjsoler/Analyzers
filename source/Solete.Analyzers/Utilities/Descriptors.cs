using System.Collections.Concurrent;
using Microsoft.CodeAnalysis;

namespace Solete.Analyzers.Utilities;

public enum Category
{
    // 1xxx
    Usage,

    // 2xxx
    Assertions,

    // 3xxx
    Extensibility,
}
public class Descriptors
{
    static readonly ConcurrentDictionary<Category, string> CategoryMapping = new();

    public static DiagnosticDescriptor Rule(
        string id,
        string title,
        Category category,
        DiagnosticSeverity defaultSeverity,
        string messageFormat)
    {
        var helpLink = $"https://xunit.net/xunit.analyzers/rules/{id}";
        var categoryString = CategoryMapping.GetOrAdd(category, c => c.ToString());

        return new DiagnosticDescriptor(id, title, messageFormat, categoryString, defaultSeverity,
            isEnabledByDefault: true, helpLinkUri: helpLink);
    }

    public static DiagnosticDescriptor X1000_TestClassMustBePublic { get; } =
        Rule(
            "xUnit1000",
            "Test classes must be public",
            Category.Usage,
            DiagnosticSeverity.Error,
            "Test classes must be public. Add or change the visibility modifier of the test class to public."
        );

    public static DiagnosticDescriptor X1001_FactMethodMustNotHaveParameters { get; } =
        Rule(
            "xUnit1001",
            "Fact methods cannot have parameters",
            Category.Usage,
            DiagnosticSeverity.Error,
            "Fact methods cannot have parameters. Remove the parameters from the method or convert it into a Theory."
        );
}
