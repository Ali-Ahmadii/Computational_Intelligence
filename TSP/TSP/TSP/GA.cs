using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP
{
    public class GA
    {
        static List<city> cityList = new List<city>();
        static int population_size = 100;
        static int mutation_rate = 7; // 0 ta 10
        static List<city_sequence> population = new List<city_sequence>();
        public class city
        {
            public int label;
            public int x;
            public int y;
            public city(int x, int y, int label)
            {
                this.x = x;
                this.y = y;
                this.label = label;
            }
        }
        public class city_sequence
        {
            public List<city> sequence = new List<city>();
            public void add_city(city c)
            {
                sequence.Add(c);
            }
            public double distance = 0;
            public int conflict { get; set; }
            public city get_city(int index)
            {
                return sequence[index];
            }
            public void change_cities(int index1, int index2)
            {
                city temp = sequence[index1];
                sequence[index1] = sequence[index2];
                sequence[index2] = temp;
            }
            public int count()
            {
                return sequence.Count;
            }
            public void print_cities()
            {
                foreach(city c in sequence)
                {
                    Console.Write(c.label);
                    Console.Write(",");
                }
                Console.WriteLine("\n");
            }


        }
        public static void conflicts()
        {
            for(int i = 0; i < population.Count; i++)
            {
                city_sequence x = population[i];
                int conf = 0;
                for(int j = 0; j < x.count(); j++)
                {
                    int z = x.get_city(j).label;
                    for(int k = j+1; k < x.count(); k++)
                    {
                        int f = x.get_city(k).label;
                        if(z == f)
                        {
                            conf += 1;
                        }
                    }
                }
                x.conflict = conf;
            }
        }

        public static void initialize_cities()
        {
            string file_path = "Z:\\UN\\Computational Intelligence\\TSP\\TSP\\TSP\\TSP51.txt";
            string[] lines = File.ReadAllLines(file_path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(' ');
                city a = new city(Convert.ToInt32(values[1]), Convert.ToInt32(values[2]), Convert.ToInt32(values[0]));
                cityList.Add(a);
            }
        }
        public static void population_initialize()
        {
            Random rn = new Random();
            for (int i = 0; i < population_size; i++)
            {
                List<int> x = new List<int>();
                city_sequence z = new city_sequence();
                for (int j = 0; j < cityList.Count; j++)
                {
                    x.Add(j);
                }
                for (int j = 0; j < cityList.Count; j++)
                {
                    int random_index = rn.Next(0, x.Count);
                    int number = x.ElementAt(random_index);
                    z.add_city(cityList.ElementAt(number));
                    x.RemoveAt(random_index);
                }
                population.Add(z);
            }
        }
        public  static double cities_distance(city n1, city n2)
        {
            int delta_y = Math.Abs(n2.y - n1.y);
            int delta_x = Math.Abs(n2.x - n1.x);
            int x_square = delta_x * delta_x;
            int y_square = delta_y * delta_y;
            return Math.Sqrt(y_square + x_square);
        }
        public static void cross_over()
        {
            Random rn = new Random();
            for (int i = 0; i < population_size-1; i++)
            {
                city_sequence father = population[i];
                city_sequence mother = population[i + 1];
                city_sequence child = new city_sequence();
                int index = rn.Next(0,51);
                for (int j = 0; j < index; j++)
                {
                    child.add_city(father.get_city(j));
                }

                List<int> ch_labels = new List<int>();
                for(int j = 0; j < child.count(); j++)
                {
                    ch_labels.Add(child.sequence[j].label);
                }
                for(int j = 0;j<father.count(); j++)
                {
                    int m_label = mother.sequence[j].label;
                    if (!ch_labels.Contains(m_label))
                    {
                        child.add_city(mother.get_city(j));
                    }
                }
                population.Add(child);
                ch_labels.Clear();
            }
        }
        public static void mutation()
        {
            Random rn = new Random();
            Random ry = new Random();
            for (int i = population_size; i < ((population.Count * mutation_rate) / 10); i++)
            {
                city_sequence mutated = population[i];
                for (int j = 0; j < 10; j++)
                {
                Again:
                    int random_index1 = rn.Next(0, cityList.Count);
                    int random_index2 = ry.Next(0, cityList.Count);
                    if (random_index1 == random_index2)
                        goto Again;
                    mutated.change_cities(random_index1, random_index2);
                    mutated.get_city(random_index1).label = rn.Next(0, 51);
                }

            }
        }
        public static double total_distance(city_sequence a)
        {
            double total = 0;
            for (int j = 0; j < 51; j++)
            {
                if (j == cityList.Count - 1)
                    total += cities_distance(a.get_city(j), a.get_city(0));
                else
                    total += cities_distance(a.get_city(j), a.get_city(j + 1));
            }
            return total;
        }

        static void Main()
        {
            //ezafe kardan tamam city ha
            initialize_cities();
            population_initialize();
            cross_over();
            mutation();
            conflicts();
            for (int i = 0; i < population.Count; i++)
            {
                population[i].distance = total_distance(population[i]);
            }
            double min_distance = population[0].distance;
            foreach (city_sequence z in population)
                if (z.distance < min_distance)
                    min_distance = z.distance;
            int o = 0;
           foreach(city_sequence z in population)
            { 
                Console.WriteLine(o+":");
                z.print_cities();
                Console.WriteLine("And Cost Is :" + z.conflict);
                Console.WriteLine("And Distance Is :" + z.distance);
                o++;
            }

            Console.WriteLine();
            Console.WriteLine("And Minimum distance IS : " + min_distance);
        }
    }
}
