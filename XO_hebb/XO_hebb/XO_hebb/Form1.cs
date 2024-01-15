using System;
using System.IO;

namespace XO_hebb
{
    public partial class Form1 : Form
    {
        private int[] buttonValues = new int[25];
        private void DetermineWeights()
        {
            int [] old_weights_values = new int[26];

            string w;
            string [] w_to_array = new string[26];
            string [] x = new string[26];

            int [] x_values = new int[26];

            int [] new_weights = new int[26];

            int [] delta_w = new int[26];

            string [] new_weights_string = new string[26];

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
                    old_weights_values = Array.ConvertAll(w_to_array, int.Parse);
                    int y = x_values[25];
                    for(int i = 0; i < 25; i++)
                    {
                        delta_w[i] = x_values[i] * y;
                    }
                    delta_w[25] = y;//this is delta b
                    for(int i = 0;i<26; i++)
                    {
                        new_weights[i] = delta_w[i] + old_weights_values[i];
                        //again new_weights[25] = new b
                    }
                    for(int i = 0; i < 25; i++)
                    {
                        string what_to_write;
                        what_to_write = new_weights[i].ToString() + ",";
                        new_weights_string[i] = what_to_write;
                    }
                    new_weights_string[25] = new_weights[25].ToString();
                    //string.Join(string.Empty, array);
                    string ready_to_write_string = string.Concat(new_weights_string);
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
            // Assuming you have an array named buttonValues declared somewhere in your code
            // int[] buttonValues = new int[25];

            // Your existing code to populate buttonValues array...
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
                    string y = TrainBox.Text.ToString();
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
        public class ButtonInfo
        {
            public Button Button { get; set; }
            public Color ButtonColor { get; set; }
            // Add other properties if needed
        }
        public int step_function(int sum)
        {
            if (sum >= 1)
                return 1;
            else
                return -1;
        }
        //public int output(int weigths,int bias)
        //{
        //    sum
        //}
        public Form1()
        {
            InitializeComponent();
            InitializeButtonEvents();
            TrainBox.SelectedIndex = TrainBox.FindStringExact("X");
            label2.Text = null;
        }


        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void InitializeButtonEvents()
        {
            for (int i = 1; i <= 25; i++)
            {
                // Assuming your buttons are named button1, button2, ..., button25
                Button button = (Button)this.Controls.Find($"button{i}", true)[0];
                button.Click += Button_Click;
            }

            // Assuming you have a button named resetButton
            CheckBtn.Click += ResetButton_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            // Handle button click event
            Button clickedButton = sender as Button;

            if (clickedButton != null)
            {
                clickedButton.BackColor = Color.Black;
                // You can customize other properties as needed
            }
        }

        private void ResetButton_Click(object sender, EventArgs e)
        {
            ButtonInfo[] buttonsArray = new ButtonInfo[25];

            // Iterate through buttons on the form
            for (int i = 1; i <= 25; i++)
            {
                Button button = (Button)this.Controls.Find($"button{i}", true)[0];

                // Store Button and its color in the array
                buttonsArray[i-1] = new ButtonInfo
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
            string w;
            string[] w_to_string = new string[26];
            int [] w_values = new int[26];
            int sum = 0;
            string solutionPath = Path.GetDirectoryName(Application.StartupPath);
            string filePath = Path.Combine(solutionPath, "Weights.txt");
            try
            {
                w = File.ReadLines(filePath).Last();
                w_to_string = w.Split(",");
                w_values = Array.ConvertAll(w_to_string, int.Parse);
                for(int i = 0; i < 25; i++)
                {
                    sum = sum + (w_values[i] * buttonValues[i]);
                }
                sum+=w_values[25];
                int sum_to_step;
                sum_to_step = step_function(sum);
                if (sum_to_step == 1)
                    label1.Text = "X";
                else
                    label1.Text = "O";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in calculating");
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Result";
        }

        private void TrainBtn_Click(object sender, EventArgs e)
        {
            // Get the selected value from 
            ButtonInfo[] buttonsArray = new ButtonInfo[25];

            // Iterate through buttons on the form
            for (int i = 1; i <= 25; i++)
            {
                Button button = (Button)this.Controls.Find($"button{i}", true)[0];

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
            string selectedValue = TrainBox.SelectedItem?.ToString();

            // Now you can use the selectedValue as needed
            if (!string.IsNullOrEmpty(selectedValue))
            {
                label2.Text = "Trained Succesfuly as " + selectedValue;
                SaveButtonValuesToFile();
                DetermineWeights();
            }
            else
            {
                label2.Text = "Not Success!";
            }




        }

        private void TrainBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}