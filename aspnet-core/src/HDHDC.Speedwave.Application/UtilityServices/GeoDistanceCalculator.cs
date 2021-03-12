using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.UtilityServices
{
    public class GeoDistanceCalculator : ITransientDependency
    {
        /// <summary>
        /// Calculate the distance between two geolocations expressed as latitude and longitude
        /// and return the distance in kilometers
        /// </summary>
        /// <param name="point01">Point 01</param>
        /// <param name="point02">Point 02</param>
        /// <returns>Distance in kilometers</returns>
        public double Calculate(Tuple<double, double> point01, Tuple<double, double> point02)
        {
            int r = 6371;
            double dLat = this.Deg2Rad(point02.Item1 - point01.Item1);
            double dLon = this.Deg2Rad(point02.Item2 - point01.Item2);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(this.Deg2Rad(point01.Item1)) * Math.Cos(this.Deg2Rad(point02.Item1)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = r * c;

            return d;
        }

        private double Deg2Rad(double deg)
        {
            return deg * (Math.PI / 180);
        }
    }
}
