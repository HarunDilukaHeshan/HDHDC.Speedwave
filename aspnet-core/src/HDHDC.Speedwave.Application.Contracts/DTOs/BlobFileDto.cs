using HDHDC.Speedwave.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace HDHDC.Speedwave.DTOs
{
    [DataContract]
    public class BlobFileDto
    {
        public BlobFileDto(string fileName, string base64DataUri)
        {
            FileName = fileName;
            Base64DataUri = base64DataUri;
        }

        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        [Required]
        [Base64ImageValidator]
        public string Base64DataUri { get; set; }
    }
}
