#if UNITY_EDITOR
#endif
using UnityEngine;
using UnityEngine.UI;

namespace BeeClicker.Store
{
    public class Item : MonoBehaviour, ISetupable<ItemScriptableObject>
    {
        public System.Action OnLevelUp;

        [SerializeField] private int _Level = 0;
        [Header("Texts")]
        [SerializeField] private TMPro.TextMeshProUGUI _NameText;
        [SerializeField] private TMPro.TextMeshProUGUI _DescriptionText;
        [SerializeField] private TMPro.TextMeshProUGUI _LevelText;
        [SerializeField] private TMPro.TextMeshProUGUI _PriceText;
        [Header("Images")]
        [SerializeField] private Image _Icon;

        private Button _LevelUpButton;
        private int _Price;
        private UpgradeHandeler _UpgradeHandeler;
        private ItemScriptableObject _Item;

        public void Setup(ItemScriptableObject item)
        {
            _Item = item;
            _NameText.text = item.Name;
            _DescriptionText.text = item.Description;
            UpdateValues();
            _Icon.sprite = item.Icon;

            _LevelUpButton = GetComponentInChildren<Button>();
            _LevelUpButton.onClick.AddListener(LevelUp);

            _UpgradeHandeler = GetComponentInChildren<UpgradeHandeler>();
            _UpgradeHandeler.Handle(item.Upgrades);
        }

        private void UpdateValues()
        {
            _LevelText.text = _Level.KiloFormat();
            _Price = Mathf.RoundToInt(_Item.Prices.Evaluate(_Level));
            _PriceText.text = _Price.KiloFormat();
        }

        private void LevelUp()
        {
            GameManager instance = GameManager.Instance;
            if(!instance.TryToBuy(_Price)) return;
            instance.IncreaseDamage(_Item, Mathf.RoundToInt(_Item.Gain.Evaluate(_Level)));
            _Level++;
            OnLevelUp?.Invoke();
            DamageType damageType = _Item.DamageType;
            if(!damageType.HasFlag(DamageType.ClickDamage))
            {
                if(damageType == DamageType.HPS)
                {
                    instance.BuyBee(nameof(Monster.Butineuse), _Level);

                } else if(damageType == DamageType.DPS)
                {
                    instance.BuyBee(nameof(Monster.Dart), _Level);
                } else
                {
                    instance.BuyBee(nameof(Monster.Shield), _Level);
                }
            }
            UpdateValues();
        }
    }
}
