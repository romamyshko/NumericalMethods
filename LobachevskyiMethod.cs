using System;
using System.Collections.Generic;
using System.Linq;
using Accord.IO;

namespace NumericalMethods.Lab1
{
    public sealed class LobachevskyiMethod
    {
        private readonly Func<double, double> _func;

        private List<double> _coefs;
        private List<double> _roots;
        private double? _epsilon;
        private int _p = 1;

        public LobachevskyiMethod(Func<double, double> func, IEnumerable<double> coefs, double? precision)
        {
            _func = func;
            _coefs = coefs.ToList();
            _roots = new List<double>(7);
            _epsilon = precision ?? Math.Pow(10, -7);
        }

        public IEnumerable<double> GetRoots()
        {
            while (CalculateCoefficients())
            {
                CalculateRoots();
            }

            for (var i = 0; i < _roots.Count; i++)
            {
                var bisectionMethod = new SimplifiedNewtonsMethod(_func, _roots[i] - 0.1, _roots[i] + 0.1, _epsilon);
                _roots[i] = bisectionMethod.GetRoot() ?? Double.NaN;
            }

            return _roots;
        }

        private void CheckRoots()
        {
            for (var i = 0; i < _roots.Count; i++)
            {
                if (Math.Abs(_func(_roots[i])) > Math.Abs(_func(_roots[i] * -1)))
                    _roots[i] *= -1;
            }
        }

        private bool CalculateCoefficients()
        {
            var coefsCopy = _coefs.DeepClone();

            for (var i = 0; i < _coefs.Count; i++)
            {
                _coefs[i] *= _coefs[i];

                if (i != 0 && i != _coefs.Count - 1)
                {
                    for (var j = 0; j < i; j++)
                    {
                        if (i + j + 1 < _coefs.Count)
                        {
                            var value = Math.Pow(-1, j + 1) * 2 * coefsCopy[i - j - 1] * coefsCopy[i + j + 1];

                            if (value.Equals(double.NaN) || double.IsInfinity(value))
                                return false;

                            _coefs[i] += value;
                        }
                           
                    }
                }
            }

            _p *= 2;

            return true;
        }

        private void CalculateRoots()
        {
            _roots.Clear();

            for (var i = 1; i < _coefs.Count; i++)
            {
                _roots.Add(Math.Pow(_coefs[i] / _coefs[i - 1], 1.0 / _p));
            }

            CheckRoots();
        }
    }
}