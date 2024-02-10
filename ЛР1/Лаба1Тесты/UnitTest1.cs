namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        Random rnd;


        [TestInitialize]
        public void TestInit()
        {
            rnd = new Random();
        }


        [TestMethod]
        public void TestBubble()
        {
            for (int j = 10; j < 10000; j *= 10)
            {
                int[] array = new int[j];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rnd.Next();
                }
                Laba1.Program.BubbleSort(array);

            }
        }
        [TestMethod]
        public void TestMerge()
        {
            for (int j = 10; j < 10000; j *= 10)
            {
                int[] array = new int[j];
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = rnd.Next();
                }
                Laba1.Program.MergeSort(array);

            }
        }

        [TestMethod]
        public void TestBubble1()
        {

            //int j = 100000;
            int j = 10000;
            int[] array = new int[j];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next();
            }
            Laba1.Program.BubbleSort(array);


        }
        [TestMethod]
        public void TestMerge1()
        {
            int j = 100000;
            int[] array = new int[j];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = rnd.Next();
            }
            Laba1.Program.MergeSort(array);


        }

    }
}