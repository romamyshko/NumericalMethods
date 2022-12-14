using System;
using System.Globalization;
using System.Linq;

namespace NumericalMethods.Lab1
{
    public static class Calculator
    {
        private static readonly double[] Coefficients = {18.0, 84.0, -225.0, -811.0, 565.0, 842.0, -489.0};

        private static readonly Func<double, double>
            Function18 = x => Math.Pow(Math.Cos(x), 3) + Math.Pow(x, 3) * Math.Exp(x) - Math.Pow(x, 6) - 35;
        private static readonly Func<double, double>
            Function31 = x => Math.Pow(x, 3) * Math.Cosh(x) + Math.PI - 9 * Math.PI * x;

        private static readonly Func<double, double>
            FunctionLb = x => Coefficients[0] * Math.Pow(x, 7) + Coefficients[1] * Math.Pow(x, 6) + 
                              Coefficients[2] * Math.Pow(x, 5) + Coefficients[3] * Math.Pow(x, 4) + 
                              Coefficients[4] * Math.Pow(x, 3) + Coefficients[5] * Math.Pow(x, 2) + 
                              Coefficients[6] * Math.Pow(x, 1);

        private static double _startOfInterval;
        private static double _endOfInterval;
        private static double? _precision;

        public static void Run()
        {
            while (true)
            {
                ProgressInfoPrinter.PrintStartMenuOptions();

                var startMenuUserInput = Console.ReadLine();

                while (true)
                {
                    if (startMenuUserInput.Equals("4")) break;

                    ProgressInfoPrinter.PrintIntervalQuery();

                    var intervalInput = Console.ReadLine();
                    var interval = intervalInput.Split(" ");

                    if (!double.TryParse(interval[0], out var firstNum)
                        || !double.TryParse(interval[1], out var secondNum)
                        || firstNum > secondNum)
                    {
                        ProgressInfoPrinter.PrintInputIsIncorrect(intervalInput);
                        continue;
                    }

                    _startOfInterval = firstNum;
                    _endOfInterval = secondNum;
                    break;
                }

                while (true)
                {
                    ProgressInfoPrinter.PrintPrecisionQuery();

                    var precisionInput = Console.ReadLine();
                    if (!double.TryParse(precisionInput, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var precision))
                    {
                        if (precisionInput == String.Empty)
                            break;

                        ProgressInfoPrinter.PrintPrecisionIsIncorrect(precisionInput);
                        continue;
                    }

                    _precision = precision;
                    break;
                }

                IMethod method;

                switch (startMenuUserInput)
                {
                    case "1":
                        method = new SimplifiedNewtonsMethod(Function18, _startOfInterval, _endOfInterval, _precision);
                        break;
                    case "2":
                        method = new BisectionMethod(Function31, _startOfInterval, _endOfInterval, _precision);
                        break;
                    case "3":
                        method = new SimpleIterationMethod(Function31, _startOfInterval, _endOfInterval, _precision);
                        break;
                    case "4":
                        method = new LobachevskyiMethod(FunctionLb, Coefficients, _precision);
                        break;
                    case "exit":
                        return;
                    default:
                        ProgressInfoPrinter.PrintUnknownCommandError();
                        continue;
                }

                PrintResult(method);

                ProgressInfoPrinter.PrintSeveralEndingVariants();

                var variantsInput = Console.ReadLine();
                if (variantsInput.Equals("y", StringComparison.InvariantCulture)) 
                    continue;

                break;
            }
        }

        public static void PrintResult(IMethod method)
        {
            var roots = method.GetRoots().ToList();

            if (roots.Count > 1)
            {
                roots.Add(0.0);
                roots.Sort();
            }

            foreach (var root in roots)
            {
                ProgressInfoPrinter.PrintMethodResult(root);
            }
        }
    }
}