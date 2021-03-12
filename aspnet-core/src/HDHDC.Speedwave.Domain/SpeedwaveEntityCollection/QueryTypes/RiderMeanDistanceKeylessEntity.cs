using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HDHDC.Speedwave.SpeedwaveEntityCollection
{
    [NotMapped]
    public class RiderMeanDistanceKeylessEntity
    {        
        public double MeanDistance { get; set; }
    }
}
