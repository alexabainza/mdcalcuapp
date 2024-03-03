using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace mdcalcuapp
{
    public partial class MainPage : ContentPage
    {


        public MainPage()
        {
            InitializeComponent();
        }

        private void btnOperation_Clicked(object sender, EventArgs e)
        {
            Button b=(Button)sender;
            if(lblHistory.Text == "0")
            {
                lblHistory.Text = string.Format("{0} {1}", lblValue.Text, b.Text);
            }
            else
            {
                lblHistory.Text = string.Format("{0} {1} {2}", lblHistory.Text, lblValue.Text, b.Text);
            }

            //adds text to the previous values
            lblValue.Text = "0";
        }

        private void btnCanc_Clicked(object sender, EventArgs e)
        {
            lblHistory.Text = "0";
            lblValue.Text = "0";
        }

        private void btnDel_Clicked(object sender, EventArgs e)
        {
            if(lblValue.Text != "0") { 
                lblValue.Text = lblValue.Text.Remove(lblValue.Text.Length-1);
                if(string.IsNullOrEmpty(lblValue.Text) ) {
                    lblValue.Text = "0";
                        }
            }
        }

        private void btnDot_Clicked(object sender, EventArgs e)
        {
            if (lblValue.Text.Contains(".") is false)
            {
                lblValue.Text += ".";
            }
        }

        private void btnGetResult_Clicked(object sender, EventArgs e)
        {
            string final = lblHistory.Text + " " + lblValue.Text;
            string[] arr = final.Split(" ");
            float result = 0;
            float tempR = 0;
            string tempS = string.Empty;
            for(int i=0; i<arr.Length; i++)
            {
                if (i % 2 == 0)
                {
                    tempR = float.TryParse(arr[i], out float res) ? res : 0;
                    if (result == 0)
                    {
                        result = tempR;
                    }
                    if(string.IsNullOrEmpty(tempS) is false) {
                        switch(tempS)
                        {
                            case "/":
                                result /= tempR;
                                break;
                            case "*":
                                result *= tempR;
                                break;
                            case "+":
                                result += tempR;
                                break;
                            case "-":
                                result -= tempR;
                                break;
                        }
                    }
                    tempS = string.Empty;
                }
                else
                {
                    tempS = arr[i];
                    lblValue.Text = result.ToString();
                    lblHistory.Text = "0";
                }
            }
        }
        private void btnNumber_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            float f = float.TryParse(b.Text, out float res)? res: 0;
            lblValue.Text = lblValue.Text == "0" ? f.ToString() : lblValue.Text + f.ToString();
        }
    }
      
}
