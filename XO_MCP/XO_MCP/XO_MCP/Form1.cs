namespace XO_MCP
{
    public partial class Form1 : Form
    {
        private double alpha = 0.1;
        private int[] buttonValues = new int[25];
        static double tehtha = 0.25;

        public Form1()
        {
            InitializeComponent();
            InitializeButtonClickEvents();
        }

        private void InitializeButtonClickEvents()
        {
            for (int i = 1; i <= 25; i++)
            {
                string buttonName = "btn" + i.ToString();

                Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;

                if (button != null)
                {
                    button.Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                if (clickedButton.BackColor == Color.Black)
                {
                    clickedButton.BackColor = SystemColors.Control;
                }
                else
                {
                    clickedButton.BackColor = Color.Black;
                }

            }
        }
        public class ButtonInfo
        {
            public Button Button { get; set; }
            public Color ButtonColor { get; set; }
            // Add other properties if needed
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TrainBtn_Click(object sender, EventArgs e)
        {
            try
            {
                alpha = double.Parse(AlphaRateText.Text);
                TrainInfoLabel.Text = "Alpha Rate Succesfuly Changed To : " + alpha;
            }
            catch (Exception ex)
            {
                TrainInfoLabel.Text = "Please Enter Just Numbers!";
            }
            // string buttonName = "btn" + i.ToString();

            //Button button = Controls.Find(buttonName, true).FirstOrDefault() as Button;
            // Get the selected value from 
            ButtonInfo[] buttonsArray = new ButtonInfo[25];

            // Iterate through buttons on the form
            for (int i = 1; i <= 25; i++)
            {
                string buttonName = "btn" + i.ToString();
                Button button = (Button)this.Controls.Find($"btn{i}", true)[0];

                // Store Button and its color in the array
                buttonsArray[i - 1] = new ButtonInfo
                {
                    Button = button,
                    ButtonColor = button.BackColor
                    // Add other properties if needed
                };
            }
            for (int j = 0; j < 25; j++)
            {
                if (buttonsArray[j].ButtonColor == Color.Black)
                {
                    buttonValues[j] = 1;
                }
                else
                {
                    buttonValues[j] = -1;
                }
            }
            foreach (var buttonInfo in buttonsArray)
            {
                buttonInfo.Button.BackColor = Color.White;
                // You can customize other properties as needed
            }
            string selectedValue = XOCombo.SelectedItem?.ToString();

            // Now you can use the selectedValue as needed
            if (!string.IsNullOrEmpty(selectedValue))
            {
                //SaveButtonValuesToFile();
                //DetermineWeights();
                TrainInfoLabel.Text = "Trained Succesfuly as " + selectedValue;
            }
            else
            {
                TrainInfoLabel.Text = "Not Success!";
            }
        }

        private void ApplyAlphaBtn_Click(object sender, EventArgs e)
        {
            try
            {
                alpha = double.Parse(AlphaRateText.Text);
                TrainInfoLabel.Text = "Alpha Rate Succesfuly Changed To : " + alpha;
            }
            catch (Exception ex)
            {
                TrainInfoLabel.Text = "Please Enter Just Numbers!";
            }
        }
    }
}