namespace DataStructers
{
    public interface ITestStateEmitter
    {
        void Subscribe(ITestStateHandler handler);

        void Unsubscribe(ITestStateHandler handler);
    }
}
