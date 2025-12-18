using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical_Task_6 {
    internal class Program {
        /*
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
            /* Replace this with your code */
            return 0;
        }

        public static void Main(string[] args) {
            /* Feel free to use this method to test your solution. */
            Func<double, double> f = (x) => {
                return 2 * x;
            };

            Console.WriteLine(Integrate(f, 0, 2));
        }
    }
}