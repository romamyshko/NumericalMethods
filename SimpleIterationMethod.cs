using System;

namespace NumericalMethods.Lab1
{
    public sealed class SimpleIterationMethod : MethodBase
    {
        public SimpleIterationMethod(Func<double, double> function, double startOfInterval, double endOfInterval, double? epsilon)
            : base(function, startOfInterval, endOfInterval, epsilon)
        {
        }

        public override void CalculateNewIntervalValues(int counter)
        {
        }

        public override bool CheckMethodStopCriteria()
        {
            return base.CheckSimplifiedStopCriteria();
        }
    }
}