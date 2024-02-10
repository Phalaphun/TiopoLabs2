namespace Laba1
{

    public class TreeNode
    {
        public TreeNode(int data)
        {
            Data = data;
        }

        //данные
        public int Data { get; set; }

        //левая ветка дерева
        public TreeNode Left { get; set; }

        //правая ветка дерева
        public TreeNode Right { get; set; }

        //рекурсивное добавление узла в дерево
        public void Insert(TreeNode node)
        {
            if (node.Data < Data)
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
        public int[] Transform(List<int> elements = null)
        {
            if (elements == null)
            {
                elements = new List<int>();
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
        private static int[] TreeSort(int[] array)
        {
            var treeNode = new TreeNode(array[0]);
            for (int i = 1; i < array.Length; i++)
            {
                treeNode.Insert(new TreeNode(array[i]));
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

