namespace BeeClicker
{
    public interface IClickable
    {
        public void Click();
    }
    public interface IHandelable<T>
    {
        public void Handle(T[] h);
    }
    public interface ISetupable<T>
    {
        public void Setup(T s);
    }
}
