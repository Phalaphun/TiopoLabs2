namespace Laba1
{
    internal class Program
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
            //BubbleSort(someArray2);
            MergeSort(someArray2);
            PrintArray(someArray2);

        }


        public static void BubbleSort<T>(T[] array) where T: IComparable<T>
        {
            if(array.Length == 0) return;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - 1 - i; j++)
                {
                    if (array[j].CompareTo(array[j+1]) > 0) // >=1 значит исходное следует за сравниваемым
                    {
                        Swap(ref array[j], ref array[j + 1]);
                    }
                }

                
            }
        }

        public static void Swap<T>(ref T t1, ref T t2) where T : IComparable<T>
        {
            T buff = t1;
            t1 = t2;
            t2 = buff;
        }

        public static void PrintArray<T>(T[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
            Console.WriteLine("\n");
        }

        public static void MergeSort<T>(T[] array) where T: IComparable<T> //https://education.yandex.ru/journal/osnovnye-vidy-sortirovok-i-primery-ikh-realizatsii
        {
            if( array.Length == 0) return;

            T[] buffer = new T[array.Length];
            MergeSortImp(array,buffer,0,array.Length-1);
        }

        private static void MergeSortImp<T>(T[] array, T[] buffer, int l, int r) where T: IComparable<T>
        {
            if (l < r)
            {
                int m = (l + r) / 2;
                MergeSortImp(array, buffer, l, m);
                MergeSortImp(array, buffer, m + 1, r);

                int k = l;
                for (int i = l, j = m + 1; i <= m || j <= r;)
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
            }
        }
    }

}
