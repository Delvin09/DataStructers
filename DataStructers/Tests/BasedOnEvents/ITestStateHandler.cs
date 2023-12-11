namespace DataStructers
{
    public interface ITestStateHandler
    {
        void TestStateChanged(string testName, TestState state);
    }
}
