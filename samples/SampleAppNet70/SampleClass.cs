using System.Runtime.Serialization;

namespace ClassLibrary1;

public class SampleClass : BaseClass
{
    private string Secret { get; set; }
    public bool Activate { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
}