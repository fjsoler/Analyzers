using System.Collections.Concurrent;
using Microsoft.CodeAnalysis;

namespace Solete.Analyzers.Utilities;

// public enum Category
// {
//     SourceCode,
//     AdditionalFile,
// }
// public class Descriptors
// {
//     static readonly ConcurrentDictionary<Category, string> CategoryMapping = new();
//
//     public static DiagnosticDescriptor Rule(
//         string id,
//         string title,
//         string category,
//         DiagnosticSeverity defaultSeverity,
//         string messageFormat)
//     {
//         var helpLink = $"https://xunit.net/xunit.analyzers/rules/{id}";
//         var categoryString = CategoryMapping.GetOrAdd(category, c => c.ToString());
//
//         return new DiagnosticDescriptor(id, title, messageFormat, categoryString, defaultSeverity,
//             isEnabledByDefault: true, helpLinkUri: helpLink);
//     }
//
//     public static DiagnosticDescriptor X1000_TestClassMustBePublic { get; } =
//         Rule(
//             "xUnit1000",
//             "Test classes must be public",
//             Category.Usage,
//             DiagnosticSeverity.Error,
//             "Test classes must be public. Add or change the visibility modifier of the test class to public."
//         );
//     
//     public const string DiagnosticId = "SR0002";
//
//     private static readonly string Title = "Data member attribute is required";
//     private static readonly string MessageFormat = "The property '{0}' need the data member attribute";
//     private static readonly string Description = "If class have a data contract attribute, data member attribute is required for all public properties.";
//
//     private const string sCategory = "Serialization";
//     
//     public static DiagnosticDescriptor SR0002_DataContractAtributeRequired { get; } =
//         Rule(
//             "SR0002",
//             "Data member attribute is required",
//             SourceCode,
//             DiagnosticSeverity.Error,
//             "The property '{0}' need the data member attribute"
//         );
// }
