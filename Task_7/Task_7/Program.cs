using System;
using System.Collections.Generic;

namespace Task_7
{

    /// <summary>
    /// класс для работы с деревом
    /// </summary>
    class PointTree : IComparable
    {
        public double data;
        public string alf;
        public PointTree left, right;

        //конструктор без параметров
        public PointTree()
        {
            data = 0;
            alf = "";
            left = null;
            right = null;
        }
        //конструктор с параметрами
        public PointTree(double d)
        {
            data = d;
        }
        //перегрузка 
        public int CompareTo(object obj)
        {
            PointTree p = obj as PointTree;
            if (data < p.data)
                return -1;
            if (data > p.data)
                return 1;
            else
                return 0;
        }
        //перегрузка
        public override string ToString()
        {
            return "tree: " + data;
        }
    }
    /// <summary>
    /// класс для работы с кодами
    /// </summary>
    class Codes
    {
        private static List<string> listW;
        //получение кодовых слов
        private static void Key(PointTree tree, string end = "")
        {
            if (tree.right == null && tree.left == null)
            {
                tree.alf = end;
                listW.Add(end);
            }
            if (tree.right != null)
                Key(tree.right, end + "0");
            if (tree.left != null)
                Key(tree.left, end + "1");
        }
        //нахождение кодов
        public static List<string> Words(PointTree tree)
        {
            listW = new List<string>();
            Key(tree);
            return listW;
        }
    }

    class Program
    {
        /// <summary>
        /// проверка на ввод частоты
        /// </summary>
        /// <returns></returns>
        static double InputDouble()
        {
            double a = 0;
            bool ok;
            do
            {
                try
                {
                    a = Convert.ToDouble(Console.ReadLine());
                    ok = true;

                }
                catch (FormatException)
                {
                    Console.WriteLine("Ошибка! Введите число типа double");
                    ok = false;
                }
            } while (!ok);
            return a;
        }
        /// <summary>
        /// подсчет суммы
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        static double Sum(List<double> list)
        {
            double sum = 0;
            foreach (var t in list)
                sum += t;
            return sum;
        }
    
        /// <summary>
        /// выволнение задания
        /// </summary>
        static void Create()
        {
            List<double> freq = new List<double>();// для частот
            List<PointTree> tree = new List<PointTree>();//для дерева

            //ввод частот
            Console.WriteLine("Введите частоты символов (их общая сумма равна 1)");
            while (true)
            {
                double cur = InputDouble();//проверка
                freq.Add(cur);//добавление
                double sum = Sum(freq);//подсчет общей суммы
                if (sum > 1)
                {
                    Console.WriteLine("Сумма введенных частот > 1. Повторите ввод сначала");
                    freq = new List<double>(); // если сумма>1
                }
                if (sum == 1)
                {
                    Console.WriteLine("Сумма введенных частот = 1. Ввод закончен");
                    break;
                }
            }
            //вывод полученных частот
            Console.WriteLine("\nВведенные частоты:");
            freq.Sort();
            freq.Reverse();
            foreach (var t in freq)
                Console.Write(t + " ");
            Console.WriteLine();
            //добавление частот в лист для дерева
            for (int i = 0; i < freq.Count; i++)
            {
                tree.Add(new PointTree(freq[i]));
            }

            while (tree.Count > 1)
            {
                PointTree tr1 = tree[0];
                PointTree tr2 = tree[1];

                tree[1] = new PointTree(tr1.data + tr2.data);
                tree[1].right = tr1;
                tree[1].left = tr2;
                tree.RemoveAt(0);
                tree.Sort();
                tree.Reverse();
            }
            List<string> codes = Codes.Words(tree[0]);

            //вывод кодовых слов
            Console.WriteLine("\nКодовые слова частот в лексикографическом порядке: ");
            foreach (var t in codes)
                Console.Write(t + " ");
            Console.WriteLine();

        }
        static void Main(string[] args)
        {
            Create();//главный метод
            Console.ReadKey();
        }  
         
    }
}
