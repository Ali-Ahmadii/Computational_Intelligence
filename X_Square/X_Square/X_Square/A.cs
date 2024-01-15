using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X_Square
{
    public class A
    {
        static List <my_numbers> information_table = new List<my_numbers> ();
        static List<my_numbers> Choosen_members = new List<my_numbers>();
        public static void print_information(List<my_numbers> sample)
        {
            int sum = 0;
            int max = 0;
            for (int i = 0; i < sample.Count; i++)
            {
                my_numbers yyy = sample[i];
                if (yyy.get_value() > max)
                    max = yyy.get_value();
                sum += yyy.get_value();
            }

            double average = (double)sum / sample.Count;
            //max we have
            Console.WriteLine("Maximum  Is : " + max);
            Console.WriteLine("Average Is : " + average);
            Console.WriteLine("Summation Is : " + sum);
            Console.WriteLine("Maximum Fitness Is : " + max * max);
            Console.WriteLine("\n");

        }
        public static void cross_over()
        {
            Random rnn = new Random ();
            for (int i = 0; i < Choosen_members.Count; i = i + 2)
            {
                try//expect that we will have null in choosen_members[i+1]
                {
                    my_numbers father = Choosen_members[i];
                    my_numbers mother = Choosen_members[i + 1];
                    int[] father_b = father.binary_form();
                    int[] mother_b = mother.binary_form();
                    //int[] father_binary = father.binary_form();
                    int cross_over_point = rnn.Next(0, 8);
                    int[] ch1 = new int[8];
                    int[] ch2 = new int[8];
                    int j = 0;
                    while (j < cross_over_point)
                    {
                        int temp1 = father.binary_form()[j];
                        int temp2 = mother.binary_form()[j];
                        ch1[j] = temp1;
                        ch2[j] = temp2;
                        j++;
                    }
                    for (int y = j; y < 8; y++)
                    {
                        int temp1 = father.binary_form()[y];
                        int temp2 = mother.binary_form()[y];
                        ch2[y] = temp1;
                        ch1[y] = temp2;
                    }
                    string h = "";
                    for (int v = 7; v >= 0; v--)
                        h += ch1[v];
                    Choosen_members[i].change_value(Convert.ToInt32(h,2));
                    h = "";
                    for (int v = 7; v >= 0; v--)
                        h += ch2[v];
                    Choosen_members[i+1].change_value(Convert.ToInt32(h, 2));

                }
                catch (Exception e)
                {
                    break;
                }
            }
        }
        public static void mutation()
        {
            //i+1 < length
            //mutate binary forms and save them
            //get decimal forms
            //change value by decimal form

            
            Random random = new Random();
            for(int i = 0; i  < Choosen_members.Count; i++)
            {
                my_numbers a = Choosen_members[i];
                int [] a_binary = a.binary_form();
                int[] new_number = new int[8];
                for (int j = 0; j+1 < 8; j++)
                {
                    int get_or = a.binary_form()[j] | a.binary_form()[j+1];

                    if (get_or == 1)
                    {
                        new_number[j] = 1;
                    }
                    else
                    {
                        new_number[j] = 0;
                    }

                }
                new_number[7] = a.binary_form()[7] ^ a.binary_form()[0];
                string z = "";
                for (int g = 7; g>=0;g--)
                    z += new_number[g];
                int newnew = Convert.ToInt32(z, 2);
                a.change_value(Convert.ToInt32(z, 2));

            }
        }

        public static int[] num_to_bianry(int num)
        {
            int[] result = new int[8];
            int n, i;
            n = num;
            for (i = 0; n > 0; i++)
            {
                result[i] = n % 2;
                n = n / 2;
            }
            return result;
        }
        public class my_numbers
        {
            List<int> members = new List<int>();
            public void add_to_members(int num)
            {
                members.Add(num);
            }
            public void change_value(int value)
            {
                members[0] = value;
            }
            public int [] binary_form()
            {
                return num_to_bianry(members[0]);
            } 
            public int get_value()
            {
                return members[0];
            }
            public int get_fitness(int index)
            {
                int x = members[index];
                return x * x;
            }
        }


        static void Main()
        {
            Console.WriteLine("Please Enter Population Size :");
            int size = Convert.ToInt32(Console.ReadLine());
            Random rn = new Random();
            for(int i = 0;i<size; i++)
            {
                my_numbers a_number = new my_numbers();
                int a_random = rn.Next(0, 255);
                a_number.add_to_members(a_random);
                information_table.Add(a_number);
            }
            int sum = 0;
            int max = 0;
            for (int i = 0; i < information_table.Count; i++)
            {
                my_numbers yyy = information_table[i];
                if (yyy.get_value() > max)
                    max = yyy.get_value();
                sum += yyy.get_value();
            }

            double average = (double)sum / information_table.Count;
            //max we have

            for (int i = 0; i < information_table.Count; i++)
            {
                my_numbers newnumbers = information_table[i];
                double expected_count = (double)(information_table[i].get_value()/average);
                int decimal_part = (int)(expected_count);
                double double_part = expected_count - decimal_part;
                int how_mant_repeat = decimal_part;
                if (double_part >= 0.5)
                    how_mant_repeat++;
                for (int j = 0; j < how_mant_repeat; j++)
                {
                    Choosen_members.Add(newnumbers);
                }
            }

            Console.WriteLine("Before Cross Over : ");
            print_information(information_table);
            cross_over();
            Console.WriteLine("After Cross Over : ");
            print_information(Choosen_members);
            Console.WriteLine("After Mutation : ");
            mutation();
            print_information(Choosen_members);


        }
    }
}
