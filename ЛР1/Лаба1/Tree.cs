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

        //левая ветка дерева
        public TreeNode<T> Left { get; set; }

        //правая ветка дерева
        public TreeNode<T> Right { get; set; }

        //рекурсивное добавление узла в дерево
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

        //преобразование дерева в отсортированный массив
        public T[] Transform(List<T> elements = null)
        {
            if (elements == null)
            {
                elements = new List<T>();
            }

            if (Left != null)
            {
                Left.Transform(elements);
            }

            elements.Add(Data);

            if (Right != null)
            {
                Right.Transform(elements);
            }

            return elements.ToArray();
        }
    }

    class TestTreeNode
    {
        //метод для сортировки с помощью бинарного дерева (мб это бинарная сортировка)
        public static T[] TreeSort<T>(T[] array) where T:IComparable<T>
        {
            var treeNode = new TreeNode<T>(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode<T>(array[i]));
            }

            return treeNode.Transform();
        }

        static void Start()
        {
            Console.Write("n = ");
            var n = int.Parse(Console.ReadLine());

            var a = new int[n];
            var random = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(0, 100);
            }

            Console.WriteLine("Random Array: {0}", string.Join(" ", a));

            Console.WriteLine("Sorted Array: {0}", string.Join(" ", TreeSort(a)));
        }
    }
}

