using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis.Testing;
using Xunit;
using VerifyCs = Solete.Analyzers.Test.Utilities.CSharpAnalyzerVerifier<Solete.Analyzers.DataContractAttributedRequired, Solete.Analyzers.Fixes.DataContractAttributeRequiredCodeFixProvider>;


namespace Solete.Analyzers.Test;

public class DataContractAttributeRequiredTest
{
    private readonly ReferenceAssemblies _referenceAssemblies;
    
    public DataContractAttributeRequiredTest()
    {
        //Prepare list assemblies reference or packages needed to compile test code
        _referenceAssemblies = ReferenceAssemblies.Default
            // .AddPackages(ImmutableArray.Create( new PackageIdentity("serilog", "2.10.0"))))   
            .AddAssemblies(ImmutableArray.Create("System.Runtime.Serialization"));
    }
    
    [Fact]
    public async void NotThrowDiagnostic()
    {
        var sourceFileList = new List<string>()
        {@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    public class TextClass : BaseClass 
    {
    }
}
        ",@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    public class BaseClass 
    {
    
    }
}
"};
        
        await VerifyCs.VerifyAnalyzerAsync(sourceFileList, _referenceAssemblies);
    }
    
    [Fact]
    public async void OneExpectedDiagnosticPropertyNeedDataMemberAttribute()
    {
        var sourceFileList = new List<string>()
        {@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    public class TestClass : BaseClass 
    {
    }
}
        ",@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    [DataContract]
    public class BaseClass 
    {
    
    }
}
"};

        var expectedDiagnostic = new DiagnosticResult(DataContractAttributedRequired.Rule).WithLocation(9, 5).WithArguments("TestClass");
        
        await VerifyCs.VerifyAnalyzerAsync(sourceFileList, _referenceAssemblies, expectedDiagnostic);
    }
    
    [Fact]
    public async void CheckCorrectFixCodeProvider()
    {
        var sourceFileList = new List<string>()
        {@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    public class TestClass : BaseClass 
    {}
}
",@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    [DataContract]
    public class BaseClass 
    {}
}
"};
        var sourceFixCodeList = new List<string>()
        {@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    [DataContract]
    public class TestClass : BaseClass 
    {}
}
",@"
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TestNamespace
{
    [DataContract]
    public class BaseClass 
    {}
}
"};
        var expectedDiagnostic = new DiagnosticResult(DataContractAttributedRequired.Rule).WithLocation(9, 5).WithArguments("TestClass");
        
        await VerifyCs.VerifyFixProviderAsync(sourceFileList, sourceFixCodeList, _referenceAssemblies, expectedDiagnostic);
    }
}