using System;

namespace NumericalMethods.Lab1
{
    public sealed class BisectionMethod : MethodBase
    {
        public BisectionMethod(Func<double, double> function, double startOfInterval, double endOfInterval, double? epsilon)
            : base(function, startOfInterval, endOfInterval, epsilon)
        {
        }

        public override void CalculateNewIntervalValues(int counter)
        {
            var middleOfInterval = CalculateMiddleOfInterval();

            if (CheckIsRootOnRange(_startOfInterval, middleOfInterval))
            {
                _endOfInterval = middleOfInterval;
            }
            else
            {
                _startOfInterval = middleOfInterval;
            }

            ProgressInfoPrinter.PrintIterationNearValue(counter, _startOfInterval, _endOfInterval);
        }

        public override bool CheckMethodStopCriteria()
        {
            return Math.Abs(_function(base.CalculateMiddleOfInterval())) < _epsilon;
        }
    }
}