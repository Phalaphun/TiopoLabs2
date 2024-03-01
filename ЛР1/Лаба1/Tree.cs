namespace Laba1
{

    public class TreeNode<T> where T:IComparable<T>
    {
        public TreeNode(T data)
        {
            Data = data;
        }

        //данные
        public T Data { get; set; }

        public TreeNode<T> Left { get; set; }

        public TreeNode<T> Right { get; set; }

        public void Insert(TreeNode<T> node)
        {
            if (node.Data.CompareTo(Data) < 0)// node.Data < Data
            {
                if (Left == null)
                {
                    Left = node;
                }
                else
                {
                    Left.Insert(node);
                }
            }
            else
            {
                if (Right == null)
                {
                    Right = node;
                }
                else
                {
                    Right.Insert(node);
                }
            }
        }

        public T[] GetArray(List<T> elements = null)
        {
            if (elements == null)
            {
                elements = new List<T>();
            }

            if (Left != null)
            {
                Left.GetArray(elements);
            }

            elements.Add(Data);

            if (Right != null)
            {
                Right.GetArray(elements);
            }

            return elements.ToArray();
        }
    }

    class TestTreeNode
    {
        public static T[] TreeSort<T>(T[] array) where T:IComparable<T>
        {
            var treeNode = new TreeNode<T>(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode<T>(array[i]));
            }

            return treeNode.GetArray();
        }

        static void Test()
        {
            Console.Write("n = ");
            var n = int.Parse(Console.ReadLine());

            var a = new int[n];
            var random = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(0, 100);
            }

            Console.WriteLine("Массивчик: {0}", String.Join(" ", a));

            Console.WriteLine("Великий Си Масивчик: {0}", String.Join(" ", TreeSort(a)));
        }
    }
}

