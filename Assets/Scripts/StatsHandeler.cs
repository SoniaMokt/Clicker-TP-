using BeeClicker.Store;
using UnityEngine;

namespace BeeClicker
{
    public class StatsHandeler : MonoBehaviour
    {
        public System.Action<int> OnCurrencyChange;
        public System.Action<int> OnDPSChange;
        public System.Action<int> OnHPSChange;
        public System.Action<int> OnClickDamageChange;

        [SerializeField] private int _Currency;
        [SerializeField] private int _DPS;
        [SerializeField] private int _HPS;
        [SerializeField] private int _ClickDamage;

        private void Start()
        {
            OnCurrencyChange?.Invoke(_Currency);
            OnDPSChange?.Invoke(_DPS);
            OnHPSChange?.Invoke(_HPS);
            OnClickDamageChange?.Invoke(_ClickDamage);
        }

        public int DPS()
        {
            ChangeCurrency(_DPS);
            return _DPS;
        }
        public int HPS()
        {
            ChangeCurrency(_HPS);
            return _HPS;
        }

        public int Click(bool gainCurrency = true)
        {
            if(gainCurrency) ChangeCurrency(_ClickDamage);
            return _ClickDamage;
        }

        private void ChangeCurrency(int value)
        {
            _Currency += value;
            OnCurrencyChange?.Invoke(_Currency);
        }

        public bool TryToPay(int price)
        {
            if(price > _Currency) return false;
            _Currency -= price;
            OnCurrencyChange?.Invoke(_Currency);
            return true;
        }

        public void IncreaseDamage(ItemScriptableObject item, int amount)
        {
            DamageType damageType = item.DamageType;
            if(damageType.HasFlag(DamageType.ClickDamage))
            {
                _ClickDamage += amount;
                OnClickDamageChange?.Invoke(_ClickDamage);
            }
            if(damageType.HasFlag(DamageType.DPS))
            {
                _DPS += amount;
                OnDPSChange?.Invoke(_DPS);
            }
            if(damageType.HasFlag(DamageType.HPS))
            {
                _HPS += amount;
                OnHPSChange?.Invoke(_HPS);
            }
        }

        internal void GainCurrency(int amount)
        {
            ChangeCurrency(amount);
        }
    }
}
