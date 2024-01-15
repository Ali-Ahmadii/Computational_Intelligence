namespace XO_ADALINE
{
    public partial class Form1 : Form
    {
        private double alpha = 1;
        private int[] buttonValues = new int[25];
        static double tehtha = 0.25;

        private static double YNI(int[] x_values, double[] weights, int categoryIndex)
        {
            double sum = 0;
            int offset = categoryIndex * 25;
            for (int i = 0; i < 25; i++)
            {
                sum += x_values[i] * weights[offset + i];
            }
            sum += weights[offset + 25]; // Add the bias weight
            return step_function(sum);
        }
        private void DetermineWeights()
        {
            double[] old_weights_values = new double[26 * 2];

            string w;
            string[] w_to_array = new string[26];
            string[] x = new string[26];

            int[] x_values = new int[26];

            double[] new_weights = new double[26];

            double[] delta_w = new double[26];

            string[] new_weights_string = new string[26];

            string solutionPath = Path.GetDirectoryName(Application.StartupPath);
            string filePath = Path.Combine(solutionPath, "Weights.txt");
            string filepath1 = Path.Combine(solutionPath, "DataSet.txt");
            if (File.Exists(filePath))
            {
                string line1 = File.ReadLines(filepath1).Last();
                x = line1.Split(",");
                x_values = Array.ConvertAll(x, int.Parse);
                w = File.ReadLines(filePath).Last();
                w_to_array = w.Split(",");
                old_weights_values = Array.ConvertAll(w_to_array, double.Parse);
                double y = x_values[25];




                double tolerance = 0.001;
                double maxweightchange = 0;
                Boolean flag = true;
                while(flag)
                {
                    //
                    for (int c = 0; c < 2; c++)
                    {
                        double yni = YNI(x_values, old_weights_values, c);
                        for (int i = 0; i < 25; i++)
                        {
                            // Simplified delta_w calculation for two categories
                            delta_w[c * 25 + i] = alpha * (y == c ? 1 : -1 - yni) * x_values[i];
                        }
                        delta_w[c * 25 + 25] = alpha * (y == c ? 1 : -1 - yni);
                    }

                    // Update weights for each category
                    for (int i = 0; i < 26 * 2; i++)
                    {
                        old_weights_values[i] = old_weights_values[i] + delta_w[i];
                    }
                }
                        //







                new_weights = old_weights_values;
                for (int i = 0; i < 25; i++)
                {
                    string what_to_write;
                    what_to_write = new_weights[i].ToString() + ",";
                    new_weights_string[i] = what_to_write;
                }
                new_weights_string[25] = new_weights[25].ToString();

                //string.Join(string.Empty, array);
                var ready_to_write_string = string.Concat(new_weights_string);
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        writer.Write(ready_to_write_string);
                        writer.Write("\n");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving weights values: {ex.Message}");
                }

            }
        }
        private void SaveButtonValuesToFile()
        {

            string solutionPath = Path.GetDirectoryName(Application.StartupPath);
            string filePath = Path.Combine(solutionPath, "Weights.txt");
            if (!File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {
                        for (int i = 0; i < 26; i++)
                        {
                            if (i != 25) //initialize first time weights as 0
                                writer.Write(0 + ",");
                            else //it's about bias
                                writer.Write(0 + "\n");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving weights values: {ex.Message}");
                }
            }
            string filePath1 = Path.Combine(solutionPath, "DataSet.txt");
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath1, true))
                {
                    // Write each set of button values on a new line
                    writer.Write(string.Join(",", buttonValues));
                    string y = XOCombo.Text.ToString();
                    writer.Write(",");
                    if (y.Equals("X"))
                    {
                        writer.Write("1");
                    }
                    else
                    {
                        writer.Write("-1");
                    }
                    writer.Write("\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving button values: {ex.Message}");
            }
        }
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
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn1_Click(object sender, EventArgs e)
        {

        }

        public class ButtonInfo
        {
            public Button Button { get; set; }
            public Color ButtonColor { get; set; }
            // Add other properties if needed
        }
        public static double step_function(double sum)
        {
            if (sum >= 0)
                return 1;
            else
                return -1;
        }


        private void TrainBtn_Click_1(object sender, EventArgs e)
        {

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
                    SaveButtonValuesToFile();
                    DetermineWeights();
                    TrainInfoLabel.Text = "Trained Succesfuly as " + selectedValue;
                }
                else
                {
                    TrainInfoLabel.Text = "Not Success!";
                }
            }
        }

        private void ApplyAlphaBtn_Click_1(object sender, EventArgs e)
        {

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

        private void CheckBtn_Click(object sender, EventArgs e)
        {

            {
                ButtonInfo[] buttonsArray = new ButtonInfo[25];

                // Iterate through buttons on the form
                for (int i = 1; i <= 25; i++)
                {
                    Button button = (Button)this.Controls.Find($"btn{i}", true)[0];

                    // Store Button and its color in the array
                    buttonsArray[i - 1] = new ButtonInfo
                    {
                        Button = button,
                        ButtonColor = button.BackColor
                        // Add other properties if needed
                    };
                }
                foreach (var buttonInfo in buttonsArray)
                {
                    buttonInfo.Button.BackColor = Color.White;
                    // You can customize other properties as needed
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

                string w;
                string[] w_to_string = new string[26];
                double[] w_values = new double[26];
                double sum = 0;
                string solutionPath = Path.GetDirectoryName(Application.StartupPath);
                string filePath = Path.Combine(solutionPath, "Weights.txt");
                try
                {
                    w = File.ReadLines(filePath).Last();
                    w_to_string = w.Split(",");
                    w_values = Array.ConvertAll(w_to_string, double.Parse);
                    for (int i = 0; i < 25; i++)
                    {
                        sum = sum + (w_values[i] * buttonValues[i]);
                    }
                    sum += w_values[25];
                    double sum_to_step;
                    sum_to_step = step_function(sum);
                    ResultInfo.Text.ToString();

                    if (sum_to_step == 1)
                        ResultInfo.Text = "X";
                    else
                        ResultInfo.Text = "o";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error in calculating");
                }
            }
        }
    }
}