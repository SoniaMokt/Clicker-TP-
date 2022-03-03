using BeeClicker.Store;
using UnityEngine;

namespace BeeClicker
{
    public class StatsHandeler : MonoBehaviour, IClickable
    {
        public System.Action<int> OnCurrencyChange;
        public System.Action<int> OnDPSChange;
        public System.Action<int> OnClickDamageChange;

        [SerializeField] private int _Currency;
        [SerializeField] private int _DPS;
        [SerializeField] private int _ClickDamage;

        private void Start()
        {
            OnCurrencyChange?.Invoke(_Currency);
            OnDPSChange?.Invoke(_DPS);
            OnClickDamageChange?.Invoke(_ClickDamage);
        }

        public void Click()
        {
            _Currency += _ClickDamage;
            OnCurrencyChange?.Invoke(_Currency);
        }

        public bool Pay(int price)
        {
            if(price > _Currency) return false;
            _Currency -= price;
            OnCurrencyChange?.Invoke(_Currency);
            return true;
        }

        public void IncreaseDamage(ItemScriptableObject item, int amount)
        {
            switch(item.DamageType)
            {
                case DamageType.ClickDamage:
                    _ClickDamage += amount;
                    OnClickDamageChange?.Invoke(_ClickDamage);
                    break;
                case DamageType.DPS:
                    _DPS += amount;
                    OnDPSChange?.Invoke(_DPS);
                    break;
            }
        }
    }
}
