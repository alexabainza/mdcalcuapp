using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdcalcuapp
{
    public static class Calculate
    {
        public static double DoCalculate(double num1, double num2, string operation)
        {
            double result = 0;

            switch (operation)
            {
                case "/":
                    result = num1 / num2;
                    break;
                case "*":
                    result = num1 * num2;
                    break;
                case "-":
                    result = num1 - num2;
                    break;

                case "+":
                    result = num1 + num2;
                    break;
                default:
                    break;
            }
            return result;
        }

    }
}