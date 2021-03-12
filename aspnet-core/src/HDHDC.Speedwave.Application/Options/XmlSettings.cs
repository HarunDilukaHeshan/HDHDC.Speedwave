using System;
using System.Collections.Generic;
using System.Text;

namespace HDHDC.Speedwave.Options
{
    public class XmlSettings
    {
        public static readonly string Position = "XmlSettings";
        public Uri NamespaceUrl { get; set; }
        public Uri SlideShowSchemaUri { get; set; }
        public Uri SlideShowTemplateUri { get; set; }
    }
}
