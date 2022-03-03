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
    public interface ISetupable
    {
        public void Setup();
    }
}
