using System;
using System.Globalization;

namespace NumericalMethods.Lab1
{
    public static class Calculator
    {
        private static readonly Func<double, double>
            Function18 = x => Math.Pow(Math.Cos(x), 3) + Math.Pow(x, 3) * Math.Exp(x) - Math.Pow(x, 6) - 35;
        private static readonly Func<double, double>
            Function31 = x => Math.Pow(x, 3) * Math.Cosh(x) + Math.PI - 9 * Math.PI * x;

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

                MethodBase method = null;

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
                        RunLobachevskyiMethod();
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

        public static void RunLobachevskyiMethod()
        {
            throw new NotImplementedException();
        }

        public static void PrintResult(MethodBase method)
        {
            var root = method?.GetRoot();

            if (root != null)
                ProgressInfoPrinter.PrintMethodResult(root);
        }
    }
}