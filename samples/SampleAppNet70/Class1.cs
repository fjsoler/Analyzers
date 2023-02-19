using System.Runtime.Serialization;

namespace ClassLibrary1;

[DataContract]
public class SampleClass
{
    private string Secret { get; set; }
    [DataMember]
    public bool Activate { get; set; }
    [DataMember]
    public string Description { get; set; }
    public string Address { get; set; }
}