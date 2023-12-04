namespace DataStructers.Tests
{
    class LinkedListTests : Tests
    {
        public override string Title => "LinkedList tests run:";

        protected override void RunTests()
        {
            SimpleTest();
        }

        private void SimpleTest()
        {
            var list = new DataStructures.Lib.LinkedList<int>();
            list.Add(1);
            list.Add(2);
            list.Add(3);

            ShowTestResult("Simple Test", list.Count == 3);
        }
    }
}
