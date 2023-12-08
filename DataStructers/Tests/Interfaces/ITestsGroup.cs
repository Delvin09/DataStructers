namespace DataStructers.Tests.Interfaces
{
    interface ITestsGroup
    {
        string Title { get; }

        void Run();

        //event Action<string, TestState> TestCompleted;
    }
}
