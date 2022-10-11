using System;
using Accord.Math.Differentiation;

namespace NumericalMethods.Lab1
{
    public sealed class SimplifiedNewtonsMethod : MethodBase
    {
        private readonly double _a, _b;

        public SimplifiedNewtonsMethod(Func<double, double> function, double startOfInterval, double endOfInterval, double? epsilon) 
            : base(function, startOfInterval, endOfInterval, epsilon)
        {
            _a = startOfInterval;
            _b = endOfInterval;
        }

        public override void CalculateNewIntervalValues(int counter)
        {
            _startOfInterval -= _function(_startOfInterval) / FiniteDifferences.Derivative(_function, _a);
            _endOfInterval -= _function(_endOfInterval) / FiniteDifferences.Derivative(_function, _b);

            ProgressInfoPrinter.PrintIterationNearValue(counter, _startOfInterval, _endOfInterval);
        }

        public override bool CheckMethodStopCriteria()
        {
            return _function(base.CalculateMiddleOfInterval()) == 0 || base.CheckSimplifiedStopCriteria();
        }
    }
}