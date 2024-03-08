using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace mdcalcuapp
{
    public partial class MainPage : ContentPage
    {
        bool isNewOperand = false;

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
                if (!result.Text.Contains("."))
                {
                    this.result.Text += ".";
                }
            }
            else if (currentState == -2)
            {
                this.result.Text = "0.";
                currentState *= -1;
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

                    result = Math.Round(result, 4);

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

            if (currentState < 0)
            {
                currentState *= -1;
                isNewOperand = true;
            }

            if (isNewOperand)
            {
                this.result.Text = b.Text;
                isNewOperand = false;
            }
            else
            {
                this.result.Text += b.Text; 
            }

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
