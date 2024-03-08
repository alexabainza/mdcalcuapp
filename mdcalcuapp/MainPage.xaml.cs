using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace mdcalcuapp
{
    public partial class MainPage : ContentPage
    {
        bool isNewOperand = false;  // Flag to track new operand

        int currentState = 1;
        string operation;
        double firstNum, secondNum;

        public MainPage()
        {
            InitializeComponent();
            btnCanc_Clicked(this, null);
        }

        private void btnOperation_Clicked(object sender, EventArgs e)
        {
            currentState = -2;

            Button b = (Button)sender;
            operation = b.Text;
        }

        private void btnCanc_Clicked(object sender, EventArgs e)
        {
            currentState = 1;
            firstNum = 0;
            secondNum = 0;
            currentState = 1;
            this.result.Text = "0";
        }

        private void btnDel_Clicked(object sender, EventArgs e)
        {
            if (result.Text != "0")
            {
                result.Text = result.Text.Remove(result.Text.Length - 1);
                if (string.IsNullOrEmpty(result.Text))
                {
                    result.Text = "0";
                }
            }
        }
        private void btnDot_Clicked(object sender, EventArgs e)
        {
            if (currentState >= 0)
            {
                this.result.Text += ".";
            }
            else if (currentState == -2)
            {
                // If operation is clicked but no digits entered for second operand
                this.result.Text = "0.";  // Start with "0."
                currentState *= -1;  // Move to positive state to allow number input
            }
        }
        private void btnGetResult_Clicked(object sender, EventArgs e)
        {
            if (currentState == 2)
            {
                if (operation == "/" && secondNum == 0)
                {
                    this.result.Text = "INVALID!";
                    currentState = -99;
                }
                else
                {
                    var result = Calculate.DoCalculate(firstNum, secondNum, operation);
                    this.result.Text = result.ToString();
                    firstNum = result;
                    currentState = -1;
                }

            }
        }
        private void btnNumber_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (currentState == -99)
            {
                btnCanc_Clicked(this, null);
            }

            // Reset current state and set new operand flag
            if (currentState < 0)
            {
                currentState *= -1;
                isNewOperand = true;
            }

            if (isNewOperand)
            {
                this.result.Text = b.Text;  // Set result.Text to new operand
                isNewOperand = false;
            }
            else
            {
                this.result.Text += b.Text;  // Append to existing number
            }
            // No need to clear result.Text here, append allows both whole and decimal

            double number;
            if (double.TryParse(result.Text, out number))
            {
                this.result.Text = number.ToString("G");
                if (currentState == 1)
                {
                    firstNum = number;
                }
                else
                {
                    secondNum = number;
                }
            }
        }


    }

}
