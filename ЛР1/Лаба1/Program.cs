#define StopLog
using System.ComponentModel.Design;
using System.Diagnostics;

namespace Laba1
{
    public class Program
    {
        static void Main(string[] args)
        {
            Start();
            //int[] someArray = new int[] { 1, 2, 4, 3, 8, 5, 7, 6, 9, 0 };
            //PrintArray(someArray);


            ////BubbleSort(someArray);
            ////MergeSort(someArray);
            ////TreeSort(ref someArray);
            ////ShakerSort(someArray);

            //PrintArray(someArray);


            //double[] someArray2 = new double[] { 1, 2, 3, 4.5, 6, 0, 1.1 };
            //PrintArray(someArray2);


            ////BubbleSort(someArray2);
            ////MergeSort(someArray2);


            //PrintArray(someArray2);

        }


        public static void BubbleSort<T>(T[] array) where T : IComparable<T>
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (array.Length == 0) return;
            Log($"Сортировка пузырьком {array.Length} начата \n");
            Log($"Сортировка пузырьком {array.Length} начата ", "log_time.txt");
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0) // >=1 значит исходное следует за сравниваемым
                    {
                        Log($"Сравниваются два элемента: {array[j]} и {array[j + 1]}");
                        Swap(ref array[j], ref array[j + 1]);

                    }
                }
                Log($"Результат итерации {GetArrayAsAString(array)}");
            }
            sw.Stop();
            Log($"\nСортировка пузырьком окончена. Время: {sw.ElapsedTicks} тиков\n");
            Log($"Сортировка пузырьком окончена. Время: {sw.ElapsedTicks} тиков или {sw.ElapsedMilliseconds} миллисекунд", "log_time.txt");
        }
        public static void ShakerSort<T>(T[] array) where T : IComparable<T>
        {
            for (var i = 0; i < array.Length / 2; i++)
            {
                var swapFlag = false;
                //проход слева направо
                for (var j = i; j < array.Length - i - 1; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0) //array[j].CompareTo(array[j+1]) > 0 array[j] > array[j + 1]
                    {
                        Swap(ref array[j], ref array[j + 1]);
                        swapFlag = true;
                    }
                }

                //проход справа налево
                for (var j = array.Length - 2 - i; j > i; j--)
                {
                    if (array[j - 1].CompareTo(array[j]) > 0) //array[j - 1] > array[j]
                    {
                        Swap(ref array[j - 1], ref array[j]);
                        swapFlag = true;
                    }
                }

                //если обменов не было выходим
                if (!swapFlag)
                {
                    break;
                }
            }
        }
        public static void Swap<T>(ref T t1, ref T t2) where T : IComparable<T>
        {
            T buff = t1;
            t1 = t2;
            t2 = buff;
        }



        public static void MergeSort<T>(T[] array) where T : IComparable<T> //https://education.yandex.ru/journal/osnovnye-vidy-sortirovok-i-primery-ikh-realizatsii
        {
            if (array.Length == 0) return;

            Stopwatch sw = Stopwatch.StartNew();
            Log($"Сортировка слиянием {array.Length} начата \n");
            Log($"Сортировка слиянием {array.Length} начата", "log_time.txt");

            T[] buffer = new T[array.Length];
            MergeSortImp(array, buffer, 0, array.Length - 1);
            sw.Stop();
            Log($"\nСортировка слиянием окончена. Время: {sw.ElapsedTicks} тиков\n");
            Log($"Сортировка слиянием окончена. Время: {sw.ElapsedTicks} тиков или {sw.ElapsedMilliseconds} миллисекунд", "log_time.txt");
        }
        public static void MergeSortImp<T>(T[] array, T[] buffer, int l, int r) where T : IComparable<T>
        {
            if (l < r)
            {
                int m = (l + r) / 2; // делю массив на 2 части
                Log($"Произвожу деление на блоки: {GetPartOfAnArrayAsAString(array, l, m)} и {GetPartOfAnArrayAsAString(array, m + 1, r)} ");
                MergeSortImp(array, buffer, l, m); // отправляю сортироваться правую часть
                //Log($"Результат работы над блоком {GetPartOfAnArrayAsAString(buffer, l, m)}");
                MergeSortImp(array, buffer, m + 1, r); // отправляю сортироваться левую часть
                //Log($"Результат работы над блоком {GetPartOfAnArrayAsAString(buffer, m+1, r)}");
                int k = l;
                for (int i = l, j = m + 1; i <= m || j <= r;) // сращиваю левую и правую часть
                {
                    //Log($"Сравниваются два элемента: {array[i]} и {array[j]}");
                    if (j > r || (i <= m && array[i].CompareTo(array[j]) < 0))
                    {
                        buffer[k] = array[i];
                        ++i;
                    }
                    else
                    {
                        buffer[k] = array[j];
                        ++j;
                    }
                    ++k;
                }
                for (int i = l; i <= r; ++i)
                {
                    array[i] = buffer[i];
                }
                Log($"Результат работы над блоком {GetPartOfAnArrayAsAString(buffer, l, r)}");
                //Log($"Результат итерации {GetArrayAsAString(array)}");
            }

        }



        public static void TreeSort<T>(ref T[] array) where T : IComparable<T>
        {
            var treeNode = new TreeNode<T>(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode<T>(array[i]));
            }

            T[] a = treeNode.Transform();
            array = a;
            //return treeNode.Transform();
        }



        public static void Log(string text, string path = "log.txt")
        {
#if !StopLog
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine(text);
                sw.Close();
            } 
#endif
        }
        public static string GetArrayAsAString<T>(T[] array)
        {
            string s = "";
            for (int i = 0; i < array.Length; i++)
            {
                s += array[i] + " ";
            }
            return s;
        }
        public static string GetPartOfAnArrayAsAString<T>(T[] array, int l, int r)
        {
            string s = "";
            for (int i = l; i <= r; i++)
            {
                s += array[i];
            }
            return s;
        }
        public static void PrintArray<T>(T[] array)
        {
            Console.WriteLine(GetArrayAsAString(array));
        }

        
        static Queue<string> queueZapis = new Queue<string>();
        static Queue<string> queueChenie = new Queue<string>();
        static Thread a;
        static Thread b;
        static Random rnd = new Random(6284);
        static void Start()
        {
            Init();

            b.Join();
            a.Join();

        }


        static void Test()
        {
            while (true)
            {
                if (queueChenie.Count == 0)
                {
                    lock (queueZapis)
                    {
                        
                        Queue<string> temp = queueChenie;
                        queueChenie = queueZapis;
                        queueZapis = temp;      
                    }
                }
                if (queueChenie.Count != 0)
                {
                    Console.WriteLine("Типо запись: " + queueChenie.Dequeue());
                }
                if (!b.IsAlive & queueChenie.Count == 0)
                {
                    return;
                }
            }
        }
        static void Bred()
        {
            for (int i = 0; i < 1000; i++)
            {
                lock (queueZapis)
                {
                    queueZapis.Enqueue($"НЯНЯНЯ: {i}");
                    Console.WriteLine("Типо внесена на запись");
                }
                if (true) //i % 10 == 0
                {
                    Thread.Sleep(rnd.Next(0,10));
                }
            }
        }
        static void Init()
        {
            //Task.Run(() => { Bred(); });
            //Task.Run(() => { Test(); });

            b = new Thread(() => { Bred(); });
            a = new Thread(() => { Test(); });
            b.Priority = ThreadPriority.Highest;
            a.Priority = ThreadPriority.Lowest;
            b.Start();
            a.Start();
        }
    }

}
