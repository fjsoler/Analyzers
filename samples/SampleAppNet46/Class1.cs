using System.Runtime.Serialization;

namespace SampleAppNet462
{
    [DataContract]
    public class SampleClass : BaseClass
    {
        private string Secret { get; set; }
        [DataMember]
        public bool Activate { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Address { get; set; }
    }
}