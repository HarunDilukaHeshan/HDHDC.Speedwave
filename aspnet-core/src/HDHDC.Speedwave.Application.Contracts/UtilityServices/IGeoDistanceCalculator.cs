using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.DependencyInjection;

namespace HDHDC.Speedwave.UtilityServices
{
    public interface IGeoDistanceCalculator : ITransientDependency
    {
        public double Calculate(Tuple<double, double> point01, Tuple<double, double> point02);

    }
}
