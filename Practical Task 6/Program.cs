using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical_Task_6 {
    internal class Program {
        /*
         
        TODO
        Your solution should support the following rules: „rectangle-left”, „rectangle-mid”, „rectangle-right”, „trapezoid”, „simpson” (being the default one).
        You are supposed to split the [a, b] interval into subintervals of length at most 0.1, but not less than 50 intervals.

         
        public static double Integrate(Func<double, double> f, double a, double b, string rule = "simpsons") {
            Input:
                a continuous function to be integrated,
                the lower and upper limits for integrations,
                the optional string argument determining the approximation rule,
            Output:
                an approximation of the definite integral of f on [a, b] based on the chosen rule
            Examples:
                f(x) <- sin(x)
                    Integrate(f, 0, 3.141593) 						-> 2        (using the Simpson’s rule)
                    Integrate(f, 0, 3.141593, rule: "trapezoid") 	-> 2        (using the trapezoid rule)
                    Integrate(f, 2, -3) 							-> Error: improper limits
                    Integrate(f, -3.141593, 3.141593) 				-> 0        (using the Simpson’s rule)
                    Integrate(f, 2, 2) 								-> 0
                g(x) <- exp(x) + 1
                    Integrate(g, 0, 1) 								-> 2.7183   (using the Simpson’s rule)
        */
        public static double Integrate(Func<double, double> f, double a, double b, string rule = "simpsons") {
            double result = 0;
            int counter = 50;

            // weird case guard clause
            if (a == b) return 0;
            // limit guard clause
            if (a > b) throw new ArgumentException("Error: improper limits");

            // set counter based on interval length
            while ((b - a) / counter > 0.1) {
                counter++;
            }

            Console.WriteLine($"Using {counter} subintervals for integration.");

            double intervalLength = (b - a) / counter;
            // calculate all subintervals
            for (int i = 0; i < counter; i++) {
                Console.WriteLine($"Integrating from {a + intervalLength * i} to {a + intervalLength * (i + 1)}");
                // base case
                switch (rule) {
                    case "rectangle-left":
                        result += Left(f, a + intervalLength * i, a + intervalLength * (i + 1));
                        break;
                    case "rectangle-mid":
                        result += Mid(f, a + intervalLength * i, a + intervalLength * (i + 1));
                        break;
                    case "rectangle-right":
                        result += Right(f, a + intervalLength * i, a + intervalLength * (i + 1));
                        break;
                    case "trapezoid":
                        result += Trapezoid(f, a + intervalLength * i, a + intervalLength * (i + 1));
                        break;
                    default:
                        result += Simpson(f, a + intervalLength * i, a + intervalLength * (i + 1));
                        break;
                }
            }

            return result;
        }

        // Leftpoint rule implementation
        // formula taken from slide 10 of lecture 6
        private static double Left(Func<double,double>f, double a, double b) {
            return (b - a) * f(a);
        }

        // Midpoint rule implementation
        // formula taken from slide 10 of lecture 6
        private static double Mid(Func<double, double> f, double a, double b) {
            Console.WriteLine($"Midpoint at {(b-a)}");
            return (b - a) * f((a + b) / 2.0);
        }

        // Rightpoint rule implementation
        // formula taken from slide 10 of lecture 6
        private static double Right(Func<double, double> f, double a, double b) {
            return (b - a) * f(b);
        }

        // Trapezoid rule implementation
        // formula taken from slide 12 of lecture 6
        private static double Trapezoid(Func<double, double> f, double a, double b) {
            return (b - a) * (f(a) + f(b)) / 2.0;
        }

        // Simpson's rule implementation
        // formula taken from slide 14 of lecture 6
        private static double Simpson(Func<double, double> f, double a, double b) {
            return (1.0/6.0 * (b - a) * ( f(a) + 4.0 * f((a + b) / 2.0) + f(b) ));
        }

        public static void Main(string[] args) {
            /* Feel free to use this method to test your solution. */
            Func<double, double> f = (x) => {
                return 2.0 * x;
            };

            Console.WriteLine(f(2));
            Console.WriteLine(Integrate(f, 0, 2));
        }
    }
}