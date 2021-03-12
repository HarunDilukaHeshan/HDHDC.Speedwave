using HDHDC.Speedwave.Validators;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HDHDC.Speedwave.DTOs
{
    [DataContract]
    public class ThumbnailDto
    {
        [DataMember]
        [Base64ImageValidator]
        public string Base64DataUrl { get; set; }
    }
}
