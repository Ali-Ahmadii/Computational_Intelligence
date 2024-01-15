class And_Perceptron
{
    static int[,] And_Dataset = { {1, 1,1}, { 1, -1,-1}, { -1, 1,-1}, { -1, -1,-1} };//a1,a2,t
    static int[] weights = new int[3];//x1,x2,b
    static int[] delta_weights = new int[3];
    static int alpha = 1;
    static double tehtha = 0.25;
    //static int tehtha = 0;
    static int yn = 0;
    static void Main(string[] args)
    {
        int[] input = new int[2];
        Console.WriteLine("Please Entere Your Inputs : ");
        for (int i = 0; i < 2; i++)
            input[i] = Convert.ToInt32(Console.ReadLine());

        //initialize weights
        for (int i = 0; i < 3; i++)
            weights[i] = 0;
        for (int i = 0; i < 100; i++)
            train();

        Console.WriteLine("Output Is : " + FYNI(YNI(input[0], input[1], 1)));
    }
    static int YNI(int a1, int a2, int b)
    {
        int sum = 0;
        sum = a1 * weights[0] + a2 * weights[1] + b * weights[2];
        return sum;
    }
    static int FYNI(int YNI)
    {
        if (YNI > tehtha)
        {
            return 1;
        }
        else if (YNI < -1 * tehtha)
        {
            return -1;
        }
        else //((YNI >= -1 * tehtha) || (YNI <= tehtha))
        {
            return 0;
        }
    }
    static void train()
    {
        for (int i = 0; i < 4; i++)
        {
            int fyni = FYNI(YNI(And_Dataset[i, 0], And_Dataset[i, 1],1));
            if (fyni != And_Dataset[i,2])
            {
                delta_weights[0] = alpha * And_Dataset[i, 0] * And_Dataset[i, 2];
                delta_weights[1] = alpha * And_Dataset[i, 1] * And_Dataset[i, 2];
                delta_weights[2] = alpha * 1 * And_Dataset[i, 2];
                weights[0] = weights[0] + delta_weights[0];
                weights[1] = weights[1] + delta_weights[1];
                weights[2] = weights[2] + delta_weights[2];
            }
            else
            {
                delta_weights[0] = 0;
                delta_weights[1] = 0;
                delta_weights[2] = 0;
            }
        }
    }
}