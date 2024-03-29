﻿//#define StopLog
using System.Diagnostics;

namespace Laba1
{
    public class Program
    {
        static List<string> queueSort = new List<string>();
        static List<string> queueLog = new List<string>();
        static Task sort;
        static Task write;
        static Random rnd = new Random(6284);
        static void Main(string[] args)
        {
            for (int j = 10; j < 10001; j*=10)
            {
                int[] array1 = new int[j];
                int[] array2 = new int[j];
                int[] array3 = new int[j];
                int[] array4 = new int[j];
                for (int i = 0; i < j; i++)
                {
                    var a = rnd.Next(1,10000);
                    array1[i] = a;
                    array2[i] = a;
                    array3[i] = a;
                    array4[i] = a;
                }

                //sort = new Task(() => BubbleSort(array1));
                //write = new Task(() => Log());
                //sort.Start();
                //write.Start();
                //sort.Wait();
                //write.Wait();

                sort = new Task(() => MergeSort(array2));
                write = new Task(() => Log());
                sort.Start();
                write.Start();
                sort.Wait();
                write.Wait();

                sort = new Task(() => TreeSort(ref array3));
                write = new Task(() => Log());
                sort.Start();
                write.Start();
                sort.Wait();
                write.Wait();


                //sort = new Task(() => ShakerSort(array4));
                //write = new Task(() => Log());
                //sort.Start();
                //write.Start();
                //sort.Wait();
                //write.Wait();


            }
        }


        public static void BubbleSort<T>(T[] array) where T : IComparable<T>
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (array.Length == 0) return;
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка пузырьком {array.Length} начата\n");
            }
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j].CompareTo(array[j + 1]) > 0) // >=1 значит исходное следует за сравниваемым
                    {
                        lock (queueSort)
                        {
                            queueSort.Add($"Сравниваются два элемента: {array[j]} и {array[j + 1]}");
                        }
                        Swap(ref array[j], ref array[j + 1]);

                    }
                }
                lock (queueSort)
                {
                    queueSort.Add($"Результат итерации {GetArrayAsAString(array)}");
                }
            }
            sw.Stop();
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка пузырьком окончена. Время: {sw.ElapsedTicks} тиков или {sw.ElapsedMilliseconds} миллисекунд\n");
            }
            
        }
        public static void ShakerSort<T>(T[] array) where T : IComparable<T>
        {
            Stopwatch sw = Stopwatch.StartNew();
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка шейкером {array.Length} начата\n");
            }
            

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
            sw.Stop();
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка шейкером окончена. Время: {sw.ElapsedTicks} тиков или {sw.ElapsedMilliseconds} миллисекунд\n");
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
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка слиянием {array.Length} начата\n");
            }
            T[] buffer = new T[array.Length];
            MergeSortImp(array, buffer, 0, array.Length - 1);
            sw.Stop();
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка слиянием окончена. Время: {sw.ElapsedTicks} тиков или {sw.ElapsedMilliseconds} миллисекунд\n");
            }           
        }
        public static void MergeSortImp<T>(T[] array, T[] buffer, int l, int r) where T : IComparable<T>
        {
            if (l < r)
            {
                int m = (l + r) / 2; // делю массив на 2 части
                lock (queueSort)
                {
                    queueSort.Add($"Произвожу деление на блоки: {GetPartOfAnArrayAsAString(array, l, m)} и {GetPartOfAnArrayAsAString(array, m + 1, r)} ");
                }
                MergeSortImp(array, buffer, l, m); // отправляю сортироваться правую часть
                //Log($"Результат работы над блоком {GetPartOfAnArrayAsAString(buffer, l, m)}");
                MergeSortImp(array, buffer, m + 1, r); // отправляю сортироваться левую часть
                //Log($"Результат работы над блоком {GetPartOfAnArrayAsAString(buffer, m+1, r)}");
                int k = l;
                for (int i = l, j = m + 1; i <= m || j <= r;) // сращиваю левую и правую часть
                {
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
                lock (queueSort)
                {
                    queueSort.Add($"Результат работы над блоком {GetPartOfAnArrayAsAString(buffer, l, r)}");
                }
                //Log($"Результат итерации {GetArrayAsAString(array)}");
            }

        }



        public static void TreeSort<T>(ref T[] array) where T : IComparable<T>
        {
            Stopwatch sw = Stopwatch.StartNew();
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка деревом {array.Length} начата\n");
            }

            var treeNode = new TreeNode<T>(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode<T>(array[i]));
            }

            T[] a = treeNode.GetArray();
            array = a;

            sw.Stop();
            lock (queueSort)
            {
                queueSort.Add($"|Сортировка деревом окончена. Время: {sw.ElapsedTicks} тиков или {sw.ElapsedMilliseconds} миллисекунд\n");
            }
            //return treeNode.Transform();
        }



        public static void Log()
        {
#if !StopLog
            using (StreamWriter sw2 = new StreamWriter("log2.txt", true))
            {
                using (StreamWriter sw = new StreamWriter("log.txt", true))
                {
                    while (true)
                    {
                        if (queueLog.Count == 0)
                        {
                            lock (queueSort)
                            {

                                List<string> temp = queueLog;
                                queueLog = queueSort;
                                queueSort = temp;
                            }
                        }
                        if (queueLog.Count != 0)
                        {
                            string a = queueLog.FirstOrDefault();
                            queueLog.Remove(a);
                            sw.WriteLine(a);
                            if (a!=null && a[0] == '|')
                                sw2.WriteLine(a);
                        }
                        if (sort.IsCompleted & queueLog.Count == 0)
                        {
                            sw.Close();
                            sw2.Close();
                            return;
                        } 
                    }
                }
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


        #region testThreadsTasks
        //static void Start()
        //{
        //    Init();

        //    b.Join();
        //    a.Join();

        //}


        //static void Test()
        //{
        //    while (true)
        //    {
        //        if (queueChenie.Count == 0)
        //        {
        //            lock (queueZapis)
        //            {

        //                Queue<string> temp = queueChenie;
        //                queueChenie = queueZapis;
        //                queueZapis = temp;      
        //            }
        //        }
        //        if (queueChenie.Count != 0)
        //        {
        //            Console.WriteLine("Типо запись: " + queueChenie.Dequeue());
        //        }
        //        if (!b.IsAlive & queueChenie.Count == 0)
        //        {
        //            return;
        //        }
        //    }
        //}
        //static void Bred()
        //{
        //    for (int i = 0; i < 1000; i++)
        //    {
        //        lock (queueZapis)
        //        {
        //            queueZapis.Enqueue($"НЯНЯНЯ: {i}");
        //            Console.WriteLine("Типо внесена на запись");
        //        }
        //        if (true) //i % 10 == 0
        //        {
        //            Thread.Sleep(rnd.Next(0,10));
        //        }
        //    }
        //}
        //static void Init()
        //{
        //    //Task.Run(() => { Bred(); });
        //    //Task.Run(() => { Test(); });

        //    b = new Thread(() => { Bred(); });
        //    a = new Thread(() => { Test(); });
        //    b.Priority = ThreadPriority.Highest;
        //    a.Priority = ThreadPriority.Lowest;
        //    b.Start();
        //    a.Start();
        //}
        #endregion old
    }

}
