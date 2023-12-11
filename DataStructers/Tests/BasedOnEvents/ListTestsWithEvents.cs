using DataStructers.Tests.Interfaces;

namespace DataStructers
{
    class ListTestsWithEvents : ITestsGroup, ITestStateEmitter
    {
        class TestResult
        {
            public string Name { get; init; } = string.Empty;

            public TestState State { get; init; }
        }

        private readonly System.Collections.Generic.List<ITestStateHandler> _testStateHandlers = new System.Collections.Generic.List<ITestStateHandler>();

        public string Title => "List tests";

        public IEnumerable<string> GetTestList()
        {
            yield return nameof(IndexOfIntsInListTest);
            yield return nameof(NotIndexOfIntsInListTest);
            yield return nameof(ContainsIntsInListTest);
        }

        private Func<TestResult>[] GetTests()
        {
            return new Func<TestResult>[] { IndexOfIntsInListTest, NotIndexOfIntsInListTest, ContainsIntsInListTest };
        }

        public void Run()
        {
            foreach (var test in GetTests())
            {
                var result = test();
                OnTestCompleted(result);
            }
        }

        private TestResult IndexOfIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.IndexOf(1) == 0 && list.IndexOf(2) == 1 && list.IndexOf(3) == 2;
            return new TestResult { Name = nameof(IndexOfIntsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult NotIndexOfIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.IndexOf(5) < 0;
            return new TestResult { Name = nameof(NotIndexOfIntsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private TestResult ContainsIntsInListTest()
        {
            var list = new DataStructures.Lib.List<int>(2);
            list.Add(1);
            list.Add(2);
            list.Add(3);

            bool isSuccess = list.Contains(1) && list.Contains(2) && list.Contains(3);
            return new TestResult { Name = nameof(ContainsIntsInListTest), State = isSuccess ? TestState.Success : TestState.Failed };
        }

        private void OnTestCompleted(TestResult result)
        {
            foreach (var handler in _testStateHandlers)
            {
                handler.TestStateChanged(result.Name, result.State);
            }
        }

        public void Subscribe(ITestStateHandler handler)
        {
            _testStateHandlers.Add(handler);
        }

        public void Unsubscribe(ITestStateHandler handler)
        {
            _testStateHandlers.Remove(handler);
        }
    }
}
