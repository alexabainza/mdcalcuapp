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

            Button b=(Button)sender;
            operation = b.Text;
        }

        private void btnCanc_Clicked(object sender, EventArgs e)
        {
            firstNum = 0;
            secondNum = 0;
            currentState = 1;
            this.result.Text = "0";
        }

        private void btnDel_Clicked(object sender, EventArgs e)
        {
            if(result.Text != "0") { 
                result.Text = result.Text.Remove(result.Text.Length-1);
                if(string.IsNullOrEmpty(result.Text) ) {
                    result.Text = "0";
                        }
            }
        }

        private void btnDot_Clicked(object sender, EventArgs e)
        {
            if (result.Text.Contains(".") is false)
            {
                result.Text += ".";
            }
        }

        private void btnGetResult_Clicked(object sender, EventArgs e)
        {
           if(currentState == 2)
            {
                var result = Calculate.DoCalculate(firstNum, secondNum, operation);
                this.result.Text = result.ToString();
                firstNum = result;
                currentState = -1;
            }
        }
        private void btnNumber_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if(this.result.Text == "0" || currentState < 0)
            {
                this.result.Text = string.Empty;
                if(currentState < 0)
                {
                    currentState *= -1;
                }
            }

            this.result.Text += b.Text;

            double number;
            if (double.TryParse(this.result.Text, out number))
            {
                this.result.Text = number.ToString("N0");
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
