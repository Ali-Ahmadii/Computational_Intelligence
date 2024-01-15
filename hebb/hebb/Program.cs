using ScottPlot;
class hebb_AND_OR
{
    static int[,] Weights = new int[4, 3];
    static void Main (string[] args)
    {
        int [,] AND_Truth_Table = {{1,1,1,1}, { 1, -1, 1 ,-1} , { -1, 1, 1 ,-1} , { -1, -1, 1 ,-1} };  // A,B,Output of and gate
        int[,] OR_Truth_Table = { { 1, 1, 1, 1 }, { 1, -1, 1, 1 }, { -1, 1, 1, 1 }, { -1, -1, 1, -1 } };
        //training(AND_Truth_Table);
        training(OR_Truth_Table);

        // test
        Console.WriteLine("Inputs : ");
        int a = Convert.ToInt32(Console.ReadLine());
        if (a != 1 && a!=-1)
        {
            Console.WriteLine("Just 1 or -1 Please");
            Environment.Exit(0);
        }
        int b = Convert.ToInt32(Console.ReadLine());
        if (b != 1 && b != -1)
        {
            Console.WriteLine("Just 1 or -1 Please");
            Environment.Exit(0);
        }
        int bias = Convert.ToInt32(Console.ReadLine());
        if (bias != 1 && bias != -1)
        {
            Console.WriteLine("Just 1 or -1 Please");
            Environment.Exit(0);
        }
        int[] input = { a, b ,bias}; //A,B,bias
        int output = Compute(input);

        Console.WriteLine("output with training: ");
        Console.WriteLine("AND Gate (" +input[0].ToString() + " , "+input[1].ToString() +") = " + output);
        Console.WriteLine("And These Are Weights : ");
        foreach (int w in Weights)
            Console.WriteLine(w);
    }
    static void training(int[,] trainingData)
    {
        int [,] temp_data = { {0, 0, 0 },{ 1, 1, 1 } }; //w1old,w2old,bold
                                                        //,w1delta,w2delta,bdelta
        for (int i = 0; i < 4; i++)
        {
            temp_data[1, 0] = trainingData[i, 0] * trainingData[i,3];
            temp_data[1,1] = trainingData[i, 1] * trainingData[i, 3];
            temp_data[1,2] = trainingData[i, 3];
            for (int j = 0; j < 3; j++)
            {
                Weights[i, j] = temp_data[0, j] + temp_data[1, j]; // first w1old then w2odl then bold
                if (j == 2)
                    for (int k = 0; k < 3; k++) {
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
            sum += input[z] * Weights[3,z];
        }
        if (sum >= 0)
            return 1;
        else
            return -1;
    }
}