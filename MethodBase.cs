using System;
using System.Collections.Generic;

namespace NumericalMethods.Lab1
{
    public abstract class MethodBase : IMethod
    {
        protected readonly Func<double, double> _function;
        protected readonly double? _epsilon;

        protected double _startOfInterval;
        protected double _endOfInterval;

        protected MethodBase(Func<double, double> function, double startOfInterval, double endOfInterval, double? epsilon)
        {
            _function = function;
            _startOfInterval = startOfInterval;
            _endOfInterval = endOfInterval;
            _epsilon = epsilon ?? Math.Pow(10, -7);
        }

        public abstract void CalculateNewIntervalValues(int counter);
        public abstract bool CheckMethodStopCriteria();

        public IEnumerable<double> GetRoots()
        {
            if (!CheckIsMonotonous())
            {
                ProgressInfoPrinter.PrintMonotonousCheckFailed();
                return null;
            }

            if (!CheckIsRootOnRange(_startOfInterval, _endOfInterval))
            {
                ProgressInfoPrinter.PrintRootCheckFailed();
                return null;
            }

            ProgressInfoPrinter.PrintStartInterval(_startOfInterval, _endOfInterval);

            var counter = 0;
            while (!CheckMethodStopCriteria())
            {
                CalculateNewIntervalValues(counter);
                counter++;
            }

            return new List<double> { CalculateResult() };
        }

        public virtual double CalculateResult()
        {
            return (_startOfInterval + _endOfInterval) / 2;
        }

        protected bool CheckSimplifiedStopCriteria()
        {
            return Math.Abs(_startOfInterval - _endOfInterval) < _epsilon;
        }

        protected double CalculateMiddleOfInterval()
        {
            return (_startOfInterval + _endOfInterval) / 2;
        }

        protected bool CheckIsRootOnRange(double startOfInterval, double endOfInterval)
        {
            return _function(startOfInterval) * _function(endOfInterval) < 0;
        }

        protected bool CheckIsMonotonous()
        {
            var step = 0.0001;
            var grows = _function(_startOfInterval) < _function(_startOfInterval + step);

            for (var x = _startOfInterval; x < _endOfInterval; x += step)
            {
                if (_function(x) < _function(x + step) != grows)
                    return false;
            }
            return true;
        }
    }
}