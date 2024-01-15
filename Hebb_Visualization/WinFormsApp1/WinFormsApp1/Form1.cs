using ScottPlot;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        static int[,] Weights = new int[4, 3];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int[,] AND_Truth_Table = { { 1, 1, 1, 1 }, { 1, -1, 1, -1 }, { -1, 1, 1, -1 }, { -1, -1, 1, -1 } };  // A,B,Output of and gate
            int[,] OR_Truth_Table = { { 1, 1, 1, 1 }, { 1, -1, 1, 1 }, { -1, 1, 1, 1 }, { -1, -1, 1, -1 } };
            training(AND_Truth_Table);
            //training(OR_Truth_Table);
            Console.ReadLine();
            int[] input = { 1, -1, 1 }; //A,B,bias
            int output = Compute(input);

            Console.WriteLine("output with training: ");
            Console.WriteLine("AND Gate (" + input[0].ToString() + " , " + input[1].ToString() + ") = " + output);
            static void training(int[,] trainingData)
            {
                int[,] temp_data = { { 0, 0, 0 }, { 1, 1, 1 } }; //w1old,w2old,bold
                                                                 //,w1delta,w2delta,bdelta
                for (int i = 0; i < 4; i++)
                {
                    temp_data[1, 0] = trainingData[i, 0] * trainingData[i, 3];
                    temp_data[1, 1] = trainingData[i, 1] * trainingData[i, 3];
                    temp_data[1, 2] = trainingData[i, 3];
                    for (int j = 0; j < 3; j++)
                    {
                        Weights[i, j] = temp_data[0, j] + temp_data[1, j]; // first w1old then w2odl then bold
                        if (j == 2)
                            for (int k = 0; k < 3; k++)
                            {
                                temp_data[0, k] = Weights[i, k];
                            }
                    }
                }
            }
            static int Compute(int[] input)
            {
                int sum = 0;
                for (int z = 0; z < 3; z++)
                {
                    sum += input[z] * Weights[3, z];
                }
                if (sum >= 0)
                    return 1;
                else
                    return 0;
            }
            CreateChart();
        }

        private void CreateChart()
        {
            int w1 = Weights[3, 0];
            int w2 = Weights[3, 1];
            int b = Weights[3, 2];

            double[] x1 = {1,0}; // Define your x1 values
            double[] x2 = new double[x1.Length];
            //function is x2 = (-w1/w2)*x1 -b/w2
            formsPlot4.Plot.PlotScatter(x1, calculate(w1,w2,b),lineStyle: LineStyle.Solid);
            formsPlot4.Refresh();

            double[] calculate(int w1, int w2, int b)
            {
                for (int i = 0; i < x1.Length; i++)
                {
                    x2[i] = (-w1 / w2) * x1[i] - b / w2;
                }
                return x2;
            }
        }

        private void formsPlot4_Load(object sender, EventArgs e)
        {

        }
    }
}