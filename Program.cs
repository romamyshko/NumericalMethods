using System;
using Accord.Math.Differentiation;

namespace NumericalMethods.Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        public static void PrintStartMessage()
        {

        }
    }

    public static class ProgressInfoPrinter
    {
        public static void PrintStartMenuOptions()
        {
            Console.WriteLine("Find root(s) of the equation:\r\n" +
                              "18 by 'Simplified Newton's Method'\t[1]\r\n" +
                              "31 by 'Bisection Method'\t[2]\r\n" +
                              "31 by 'Simple-Iteration Method'\t[3]\r\n");
        }


    }

    public abstract class MethodBase
    {
        private Func<double, double> _function;
        private double _startOfInterval;
        private double _endOfInterval;
        private double _epsilon = Math.Pow(10, -7);
        //private FiniteDifferences _derivative;

        protected MethodBase(Func<double, double> function, double startOfInterval, double endOfInterval)
        {
            _function = function;
            _startOfInterval = startOfInterval;
            _endOfInterval = endOfInterval;
            //FiniteDifferences.Derivative()
        }

        protected MethodBase(Func<double, double> function, double startOfInterval, double endOfInterval, double epsilon) 
            : this(function, startOfInterval, endOfInterval)
        {
            _epsilon = epsilon;
        }

        public virtual double GetRoot()
        {

        }

        private bool CheckIsRootOnRange()
        {
            return _function(_startOfInterval) * _function(_endOfInterval) < 0;
        }

        private bool CheckSimplifiedStopCriteria()
        {
            return Math.Abs(_startOfInterval - _endOfInterval) < _epsilon;
        }

        private bool CheckIsMonotonous()
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
