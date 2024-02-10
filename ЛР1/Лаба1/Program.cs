using System.Diagnostics;

namespace Laba1
{
    public class Program
    {
        static void Main(string[] args)
        {
            int[] someArray = new int[] { 1, 2, 4, 3, 8, 5, 7, 6, 9, 0 };
            PrintArray(someArray);


            //BubbleSort(someArray);
            MergeSort(someArray);


            PrintArray(someArray);


            double[] someArray2 = new double[] { 1, 2, 3, 4.5, 6, 0, 1.1 };
            PrintArray(someArray2);


            BubbleSort(someArray2);
            //MergeSort(someArray2);


            PrintArray(someArray2);

        }


        public static void BubbleSort<T>(T[] array) where T : IComparable<T>
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (array.Length == 0) return;
            Log($"Сортировка пузырьком {array.Length} начата \n");
            Log($"Сортировка пузырьком {array.Length} начата ","log_time.txt");
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
            Log($"Сортировка слиянием {array.Length} начата","log_time.txt");

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

    }

}
