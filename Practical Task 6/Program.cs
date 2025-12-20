using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

            double intervalLength = (b - a) / counter;
            // calculate all subintervals
            for (int i = 0; i < counter; i++) {
                // Console.WriteLine($"Integrating from {a + intervalLength * i} to {a + intervalLength * (i + 1)}");
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

        // returns selected rule by user as a string
        private static string SelectRule() {
            int input;
            string[] rules = { "rectangle - left", "rectangle - mid", "rectangle - right", "trapezoid", "simpson" };

            // simple menu to select rule
            do {
                Console.Clear();
                Console.WriteLine("Select the integration rule to use: \n");

                for (int i = 0; i < rules.Length; i++) {
                    Console.WriteLine($"{i + 1}. {rules[i]}");
                }
                int.TryParse(Console.ReadKey().KeyChar.ToString(), out input);
                input -= 1; // adjust for list index
            } while (input < 0 || input >= rules.Length);

            return rules[input];
        }

        // returns selected function type by user as a Func<Double,Double>
        private static Func<Double,Double> SelectFunc() {
            int input;
            string[] functions = { "Polynomial", "Trigonometric", "Exponential", "Rational", "Logarithmic" };
            Func<Double,Double> selectedFunc;

            // simple menu to select rule
            do {
                Console.Clear();
                Console.WriteLine("Select the function type to be used with the integration: \n");

                for (int i = 0; i < functions.Length; i++) {
                    Console.WriteLine($"{i + 1}. {functions[i]}");
                }
                int.TryParse(Console.ReadKey().KeyChar.ToString(), out input);
                input -= 1; // adjust for list index
            } while (input < 0 || input >= functions.Length);

            // construct selected function
            switch (functions[input]) {
                case "Trigonometric":

                    int trigInput;
                    string[] trigFunctions = { "Sine", "Cosine", "Tangent", "Cotangent", "Secant", "Cosecant" };

                    // simple menu to select trig function
                    do {
                        Console.Clear();
                        Console.WriteLine("Select the trig function to be used with the integration: \n");

                        for (int i = 0; i < trigFunctions.Length; i++) {
                            Console.WriteLine($"{i + 1}. {trigFunctions[i]}");
                        }
                        int.TryParse(Console.ReadKey().KeyChar.ToString(), out trigInput);
                        trigInput -= 1; // adjust for list index
                    } while (trigInput < 0 || trigInput >= trigFunctions.Length);

                    double a, b, c, d;

                    // get parameters for function
                    do {
                        Console.Clear();
                        Console.WriteLine($"Enter parameter A ( A * trig( B * x - C ) + D ): ");
                    } while (!double.TryParse(Console.ReadLine(), out a));

                    do {
                        Console.Clear();
                        Console.WriteLine($"Enter parameter B ( {a} * trig( B * x - C ) + D ): ");
                    } while (!double.TryParse(Console.ReadLine(), out b));

                    do {
                        Console.Clear();
                        Console.WriteLine($"Enter parameter C ( {a} * trig( {b} * x - C ) + D ): ");
                    } while (!double.TryParse(Console.ReadLine(), out c));

                    do {
                        Console.Clear();
                        Console.WriteLine($"Enter parameter D ( {a} * trig( {b} * x - {c} ) + D ): ");
                    } while (!double.TryParse(Console.ReadLine(), out d));

                    Console.WriteLine($"Final equation:  {a} * trig( {b} * x - {c} ) + {d} ): ");
                    Console.ReadKey();


                    // consstruct selected trig function
                    switch (trigFunctions[trigInput]) { 
                        case "Sine":
                            selectedFunc = (x) => { return a * Math.Sin((b * x) - c) + d; }; // A*sin(B*x - c) + D
                            break;
                        case "Cosine":
                            selectedFunc = (x) => { return a * Math.Cos((b * x) - c) + d; }; // cos(x)
                            break;
                        case "Tangent":
                            selectedFunc = (x) => { return a * Math.Tan((b * x) - c) + d; }; // tan(x)
                            break;
                        case "Cotangent":
                            selectedFunc = (x) => { return a / Math.Tan((b * x) - c) + d; }; // cot(x)
                            break;
                        case "Secant":
                            selectedFunc = (x) => { return a / Math.Cos((b * x) - c) + d; }; // sec(x)
                            break;
                        case "Cosecant":
                            selectedFunc = (x) => { return a / Math.Sin((b * x) - c) + d; }; // csc(x)
                            break;
                        default:
                            throw new ArgumentException("Error: invalid trig function");
                    }
                    break;

                case "Polynomial":
                    selectedFunc = (x) => { return x * x + 2 * x + 1; }; // x^2 + 2x + 1
                    break;
                case "Exponential":
                    selectedFunc = (x) => { return Math.Exp(x); }; // e^x
                    break;
                case "Rational":
                    selectedFunc = (x) => { return 1 / (x + 1); }; // 1/(x+1)
                    break;
                case "Logarithmic":
                    selectedFunc = (x) => { return Math.Log(x + 1); }; // ln(x+1)
                    break;
                default:
                    throw new ArgumentException("Error: invalid function type");
            }

            return selectedFunc;
        }

        private static int[] SelectLimits() {
            int lowerLimit = 0;
            int upperLimit = 2;
            Console.Clear();
            Console.WriteLine("Enter the lower limit of integration: ");

            return new int[] { lowerLimit, upperLimit };
        }

        public static void Main(string[] args) {
            string rule = SelectRule();
            Func<Double,Double> func = SelectFunc();
            int[] limits = SelectLimits();

            // Execute integration and print result
            Console.Clear();
            Console.WriteLine($"Result of the calculation: {Integrate(func, limits[0], limits[1], rule)}");
        }
    }
}