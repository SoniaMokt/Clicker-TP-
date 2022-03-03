using UnityEngine;

namespace BeeClicker
{
    public class Hive : MonoBehaviour, IClickable
    {
        public System.Action OnClick;
        public void Click()
        {
            GameManager.Instance.StatsHandeler.Click();
            OnClick?.Invoke();
        }
    }
}