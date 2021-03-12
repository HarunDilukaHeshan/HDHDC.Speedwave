using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HDHDC.Speedwave.DTOs
{
    [DataContract]
    public class SlideShowPictureDto
    {
        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Base64Picture { get; set; }
        [DataMember]
        public string Uri { get; set; }
    }
}
