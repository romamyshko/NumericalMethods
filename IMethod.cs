using System.Collections.Generic;

namespace NumericalMethods.Lab1
{
    public interface IMethod
    {
        IEnumerable<double> GetRoots();
    }
}