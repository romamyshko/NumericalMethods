using System;
using Accord.Math.Differentiation;

namespace NumericalMethods.Lab1
{
    public sealed class SimpleIterationMethod : MethodBase
    {
        private double _lambda;
        private double _q;
        private double _prevA, _prevB;

        public SimpleIterationMethod(Func<double, double> function, double startOfInterval, double endOfInterval, double? epsilon)
            : base(function, startOfInterval, endOfInterval, epsilon)
        {
            if (CheckIsMonotonous())
            {
                var dfOfStartOfInterval = FiniteDifferences.Derivative(_function, _startOfInterval);
                var dfOfEndOfInterval = FiniteDifferences.Derivative(_function, _endOfInterval);

                var alpha = Math.Min(dfOfStartOfInterval, dfOfEndOfInterval);
                var gamma = Math.Max(dfOfStartOfInterval, dfOfEndOfInterval);

                _lambda = 2 / (alpha + gamma);
                _q = (gamma - alpha) / (gamma + alpha);
            }
        }

        public override void CalculateNewIntervalValues(int counter)
        {
            _prevA = _startOfInterval;
            _startOfInterval = CalculatePhi(_prevA);

            _prevB = _endOfInterval;
            _endOfInterval = CalculatePhi(_prevB);

            ProgressInfoPrinter.PrintIterationNearValue(counter, _startOfInterval, _endOfInterval);
        }

        public override bool CheckMethodStopCriteria()
        {
            return Math.Abs(_startOfInterval - _prevA) <= Math.Abs((1 - _q) * (_epsilon ?? Double.Epsilon) / _q)
                && Math.Abs(_endOfInterval - _prevB) <= Math.Abs((1 - _q) * (_epsilon ?? Double.Epsilon) / _q);
        }

        private double Phi(double x)
        {
            return (Math.Pow(x, 3) * Math.Cosh(x) + Math.PI) / (9 * Math.PI);
        }

        private double CalculatePhi(double x)
        {
            return x - _lambda * _function(x);
        }

        public override double CalculateResult()
        {
            return (_startOfInterval - _prevA) < (_endOfInterval - _prevB)
                ? _startOfInterval
                : _endOfInterval;
        }
    }
}