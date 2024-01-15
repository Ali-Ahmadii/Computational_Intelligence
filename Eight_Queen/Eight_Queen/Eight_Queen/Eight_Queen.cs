using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eight_Queen
{
    public class Eight_Queen
    {

        // static List<int> member = new List<int>();
        static List<member> population_list = new List <member>();
        //4 marhale
        //Initializr population
        //cross over
        //mutation
        //fitness
        //check
        //elimination

        //index list haman moshakhas konande shomare sate ma boode va adad dakhel an neshan dahande shomare sotoon ma ast
        class member 
        {
            List<int> members = new List<int>();
            public int count_me()
            {
                return members.Count();
            }
            public void add_member(int value)
            {
                members.Add(value);
            }
            public bool check_contain(int num)
            {
                if (members.Contains(num))
                    return true;
                else
                    return false;
            }
            public void change_member(int index,int num)
            {
                members[index] = num;
            }
            public int get_member(int index)
            {
                return members[index];
            }
            public void print_all_members()
            {
                foreach(int i in members)
                    Console.Write(i+",");
            }
        }


        static void initialize_population(int size, int n)
        {
            member xx = new member();
            Random rn = new Random();
            List<int> ts = new List<int>();
            //hazf khod element
            //hazf index element(taghir size)
            //amalan yekie

            for (int i = 0; i < size; i++)
            {
                for (int k = 1; k <= n; k++)
                    ts.Add(k);
                for (int j = 0; j < n; j++)
                {
                    int random_index = rn.Next(0, ts.Count);
                    int number = ts.ElementAt(random_index);
                    xx.add_member(number);
                    int index = ts.IndexOf(number);
                    ts.RemoveAt(index);
                }
                ts.Clear();
                xx.add_member(0);
                population_list.Add(xx);
                xx = new member();
            }
        }
        static void crossover()
        {
            int population_list_count = population_list.Count;
            for (int i = 0; i < population_list_count; i = i + 2)
            {
                member father = population_list[i];
                member mother = population_list[i + 1];
                int[] father_members = new int[8];
                int[] mother_members = new int[8];
                for (int j = 0; j < 8; j++)
                {
                    father_members[j] = father.get_member(j);
                    mother_members[j] = mother.get_member(j);
                }
                member ch1 = new member();
                member ch2 = new member();
                for (int k = 0; k < 4; k++)
                {
                    ch1.add_member(father_members[k]);
                    ch2.add_member(mother_members[k]);

                }
                for (int k = 0; k < 4; k++) 
                {
                    ch1.add_member(mother_members[4 + k]);
                    ch2.add_member(father_members[4 + k]);
                }
                //member ch2 = population_list[i + 1];
                ch1.add_member(0);
                ch2.add_member(0);
                population_list.Add(ch1);
                population_list.Add(ch2);
                
            }

        }
        static void mutation(int mrate,int size,int n)
        {
            Random rn = new Random();
            List<int> ts = new List<int>();
            int u = (size * 8) / 10;
            for (int k = size; k < size+u; k++)
                ts.Add(k);
            for (int i = size;i< size+u; i++)
            {

                int random_index = rn.Next(0, ts.Count);
                int number = ts.ElementAt(random_index);
                int index = ts.IndexOf(number);
                ts.RemoveAt(index);
                member x = population_list[number];
                int y = rn.Next(1,n);
                x.change_member(y, y);
            }
        }
        static void fitness(int n)
        {
            int hit = 0;
            for (int i = 0; i < population_list.Count; i++)
            {
                hit = 0;
                member x = population_list[i];
                for (int j = 0; j<n; j++)
                {
                    for (int l = j+1; l<n; l++)
                    {
                        if (x.get_member(j) == x.get_member(l))
                            hit += 1;
                        if (Math.Abs(j - l) == Math.Abs(x.get_member(j) - x.get_member(l)))
                            hit += 1;
                    }
                }
                x.change_member(n, hit);
            }
        }
        static void Main()
        {
            Console.WriteLine("Entere Population Size : ");
            int size = Convert.ToInt32(Console.ReadLine());
            int n = 8;
            Console.WriteLine("Now Entere Mutation Rate Please Enter between 0 to 10:");
            int mrate = Convert.ToInt32(Console.ReadLine());
            initialize_population(size, n);
            crossover();
            mutation(mrate,size,n);
            fitness(n);
            int flag = 0;
            foreach(member x in population_list)
            {
                if (x.get_member(n) == 0)
                {
                    flag = 1;
                    Console.WriteLine("found");
                    x.print_all_members();
                }
            }
            if (flag == 0)
                Console.WriteLine("Please Enter More Size To Reach A Answer"!);
        }

    }
}
