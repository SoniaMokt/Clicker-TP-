using BeeClicker.Store;
using System;
using UnityEngine;

namespace BeeClicker
{
    public class StatsHandeler : MonoBehaviour
    {
        public System.Action<int> OnCurrencyChange;
        public System.Action<int> OnDPSChange;
        public System.Action<int> OnClickDamageChange;
        public System.Action OnDPSFirstChange;

        [SerializeField] private int _Currency;
        [SerializeField] private int _DPS;
        [SerializeField] private int _ClickDamage;

        private void Start()
        {
            OnCurrencyChange?.Invoke(_Currency);
            OnDPSChange?.Invoke(_DPS);
            OnClickDamageChange?.Invoke(_ClickDamage);
        }

        public int DPS()
        {
            ChangeCurrency(_DPS);
            return _DPS;
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
            switch(item.DamageType)
            {
                case DamageType.ClickDamage:
                    _ClickDamage += amount;
                    OnClickDamageChange?.Invoke(_ClickDamage);
                    break;
                case DamageType.DPS:
                    if(_DPS == 0) OnDPSFirstChange?.Invoke();
                    _DPS += amount;
                    OnDPSChange?.Invoke(_DPS);
                    break;
            }
        }

        internal void GainCurrency(int amount)
        {
            ChangeCurrency(amount);
        }
    }
}
