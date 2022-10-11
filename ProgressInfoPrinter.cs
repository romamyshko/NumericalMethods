using System;

namespace NumericalMethods.Lab1
{
    public static class ProgressInfoPrinter
    {
        public static void PrintStartMenuOptions()
        {
            Console.WriteLine("Find root(s) of the equation:\r\n" +
                              "[1]\t18  by 'Simplified Newton's Method'\r\n" +
                              "[2]\t31  by 'Bisection Method'\r\n" +
                              "[3]\t31  by 'Simple-Iteration Method'\r\n" +
                              "[4]\t24* by 'Lobachevskyi Method'\r\n");
        }

        public static void PrintStartInterval(double startOfInterval, double endOfInterval)
        {
            Console.WriteLine($"Start interval [{startOfInterval};{endOfInterval}]");
        }

        public static void PrintMonotonousCheckFailed()
        {
            Console.WriteLine("Function is not monotonous on this interval. Please choose other interval.");
        }

        public static void PrintRootCheckFailed()
        {
            Console.WriteLine("Function do not have root(s) on the interval. Please choose other interval.");
        }

        public static void PrintIterationNearValue(int count, double nearValueA, double nearValueB)
        {
            Console.WriteLine($"№{count} Near values: [{nearValueA}, {nearValueB}]");
        }

        public static void PrintUnknownCommandError()
        {
            Console.WriteLine("I can't understand you, please try again.");
        }

        public static void PrintIntervalQuery()
        {
            Console.WriteLine("Write interval for localization: [a, b].\r\n" +
                              "Two values must be separated by space, where a < b.\r\n" +
                              "For example: \"0.95 1.17\"");
        }

        public static void PrintInputIsIncorrect(string input)
        {
            Console.WriteLine($"'{input}' is incorrect, please try again.");
        }

        public static void PrintPrecisionQuery()
        {
            Console.WriteLine("Write precision or press Enter [default precision = 10^(-7)]");
        }

        public static void PrintPrecisionIsIncorrect(string precision)
        {
            Console.WriteLine($"'{precision}' is incorrect, please try again.");
        }

        public static void PrintMethodResult(double? root)
        {
            Console.WriteLine($"Root is '{root}'.");
        }

        public static void PrintSeveralEndingVariants()
        {
            Console.WriteLine("Return to main menu [y] or exit [e]?");
        }
    }
}