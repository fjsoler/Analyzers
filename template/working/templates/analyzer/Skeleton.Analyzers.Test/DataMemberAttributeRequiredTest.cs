using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;
using Xunit;
using VerifyCs = Skeleton.Analyzers.Test.Utilities.CSharpAnalyzerVerifier<Skeleton.Analyzers.DataMemberAttributeRequired, Skeleton.Analyzers.Fixes.DataMemberAttributedRequiredCodeFixProvider>;


namespace Skeleton.Analyzers.Test
{
    public class DataMemberAttributeRequiredTest
    {
        private readonly ReferenceAssemblies _referenceAssemblies;

        public DataMemberAttributeRequiredTest()
        {
            //Prepare list assemblies reference or packages needed to compile test code
            _referenceAssemblies = ReferenceAssemblies.Default
                // .AddPackages(ImmutableArray.Create( new PackageIdentity("serilog", "2.10.0"))))   
                .AddAssemblies(ImmutableArray.Create("System.Runtime.Serialization"));
        }

        [Fact]
        public async void NotDiagnosticExpected()
        {
            var sourceFileList = new List<string>()
            {
                @"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    public class TextClass 
    {
    }
}
        "
            };

            await VerifyCs.VerifyAnalyzerAsync(sourceFileList, _referenceAssemblies);
        }

        [Fact]
        public async void OneExpectedDiagnosticPropertyNeedDataMemberAttribute()
        {
            var sourceFileList = new List<string>()
            {
                @"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
           
namespace TestNamespace
{
    [DataContract]                
    public class TestClass 
    {
        public bool Property1 { get; set; }
    }
}
"
            };

            var expectedDiagnostic = new DiagnosticResult(DataMemberAttributeRequired.Rule).WithLocation(12, 9)
                .WithArguments("Property1");

            await VerifyCs.VerifyAnalyzerAsync(sourceFileList, _referenceAssemblies, expectedDiagnostic);
        }

        [Fact]
        public async void TwoExpectedDiagnosticPropertyNeedDataMemberAttribute()
        {
            var sourceFileList = new List<string>()
            {
                @"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
           
namespace TestNamespace
{
    [DataContract]                
    public class TestClass 
    {
        public bool Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
"
            };
            DiagnosticResult[] expectedDiagnostic =
            {
                new DiagnosticResult(DataMemberAttributeRequired.Rule).WithLocation(12, 9).WithArguments("Property1"),
                new DiagnosticResult(DataMemberAttributeRequired.Rule).WithLocation(13, 9).WithArguments("Property2")
            };

            await VerifyCs.VerifyAnalyzerAsync(sourceFileList, _referenceAssemblies, expectedDiagnostic);
        }

        [Fact]
        public async void CheckCorrectFixCodeProvider()
        {
            var sourceFileList = new List<string>()
            {
                @"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
           
namespace TestNamespace
{
    [DataContract]                
    public class TestClass 
    {
        public bool Property1 { get; set; }
        [DataMember]
        public string Property2 { get; set; }
        private string Property3 { get; set; }
    }
}
"
            };

            var fixCode = @"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
           
namespace TestNamespace
{
    [DataContract]                
    public class TestClass 
    {
        [DataMember]
        public bool Property1 { get; set; }
        [DataMember]
        public string Property2 { get; set; }
        private string Property3 { get; set; }
    }
}
";
            var expectedDiagnostic = new DiagnosticResult(DataMemberAttributeRequired.Rule).WithLocation(12, 9)
                .WithArguments("Property1");

            await VerifyCs.VerifyFixProviderAsync(sourceFileList, fixCode, _referenceAssemblies, expectedDiagnostic);
        }

        [Fact]
        public async void TwoPropertyFixCodeProviderCheck()
        {
            var sourceFileList = new List<string>()
            {
                @"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
           
namespace TestNamespace
{
    [DataContract]                
    public class TestClass 
    {
        public bool Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
"
            };

            var fixCode = @"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
           
namespace TestNamespace
{
    [DataContract]                
    public class TestClass 
    {
        [DataMember]
        public bool Property1 { get; set; }
        [DataMember]
        public string Property2 { get; set; }
    }
}
";
            DiagnosticResult[] expectedDiagnostic =
            {
                new DiagnosticResult(DataMemberAttributeRequired.Rule).WithLocation(12, 9).WithArguments("Property1"),
                new DiagnosticResult(DataMemberAttributeRequired.Rule).WithLocation(13, 9).WithArguments("Property2")
            };


            await VerifyCs.VerifyFixProviderAsync(sourceFileList, fixCode, _referenceAssemblies, expectedDiagnostic);
        }
    }
}
