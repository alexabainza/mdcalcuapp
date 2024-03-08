using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace mdcalcuapp
{
    public partial class MainPage : ContentPage
    {
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

            // Reset current state to positive if negative (after operation)
            if (currentState < 0)
            {
                currentState *= -1;
            }

            // If number is being entered after an operation
            if (currentState > 0)
            {
                this.result.Text += b.Text;  // Append number to displayed text
            }
            else  // If starting a new number after operation
            {
                this.result.Text = b.Text;  // Set result.Text to the new number
            }

            double number;
            if (double.TryParse(this.result.Text, out number))
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
