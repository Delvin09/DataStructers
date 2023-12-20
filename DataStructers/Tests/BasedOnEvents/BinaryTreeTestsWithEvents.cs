using DataStructers.Tests.Interfaces;
using DataStructures.Lib;

namespace DataStructers
{
    class BinaryTreeTestsWithEvents : ITestsGroup
    {
        public string Title => "Binary tree tests";

        public IEnumerable<string> GetTestList()
        {
            yield return nameof(AddToTree);
            yield return nameof(ContainsInTree);
            yield return nameof(ClearTree);
            yield return nameof(TreeToArray);
        }

        private Func<TestResult>[] GetTests()
        {
            return new Func<TestResult>[]
            {
                AddToTree
                , ContainsInTree
                , ClearTree
                , TreeToArray
            };
        }

        public void Run()
        {
            foreach (var test in GetTests())
            {
                var result = test();
                OnTestCompleted(result);
            }
        }

        private void OnTestCompleted(TestResult result)
        {
            TestCompleted?.Invoke(result.Name, result.State);
        }

        private TestResult AddToTree()
        {
            var tree = new BinaryTree<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            var isSuccess = tree.Count == 4 && tree.Root?.Key == 1;
            return new TestResult { Name = nameof(AddToTree), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ContainsInTree()
        {
            var tree = new BinaryTree<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            var isSuccess = tree.Count == 4 && tree.Contains(2) && !tree.Contains(-2);
            return new TestResult { Name = nameof(ContainsInTree), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ClearTree()
        {
            var tree = new BinaryTree<int>();
            tree.Add(1);
            tree.Add(2);
            tree.Add(3);
            tree.Add(4);

            tree.Clear();

            var isSuccess = tree.Count == 0 && tree.Root == null;
            return new TestResult { Name = nameof(ClearTree), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult TreeToArray()
        {
            var tree = new BinaryTree<int>();
            tree.Add(3);
            tree.Add(1);
            tree.Add(4);
            tree.Add(2);

            var array = tree.ToArray();

            var isSuccess = tree.Count == 4 && array[0] == 1 && array[1] == 2 && array[2] == 3 && array[3] == 4;
            return new TestResult { Name = nameof(TreeToArray), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        public event Action<string, TestState>? TestCompleted;
    }
}
