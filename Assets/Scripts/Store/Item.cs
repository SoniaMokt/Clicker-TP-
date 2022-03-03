#if UNITY_EDITOR
#endif
using UnityEngine;
using UnityEngine.UI;

namespace BeeClicker.Store
{
    public class Item : MonoBehaviour, ISetupable
    {
        [SerializeField] private ItemScriptableObject _Item;
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

        public void Setup()
        {
            _NameText.text = _Item.Name;
            _DescriptionText.text = _Item.Description;
            UpdateValues();
            _Icon.sprite = _Item.Icon;

            _LevelUpButton = GetComponentInChildren<Button>();
            _LevelUpButton.onClick.AddListener(LevelUp);

            _UpgradeHandeler = GetComponentInChildren<UpgradeHandeler>();
            _UpgradeHandeler.Handle(_Item.Upgrades);
        }

        private void UpdateValues()
        {
            _LevelText.text = _Level.ToString();
            _Price = Mathf.RoundToInt(_Item.Prices.Evaluate(_Level));
            _PriceText.text = _Price.ToString();
        }

        private void LevelUp()
        {
            if(!GameManager.Instance.Buy(_Price)) return;
            GameManager.Instance.IncreaseDamage(_Item, Mathf.RoundToInt(_Item.Gain.Evaluate(_Level)));
            _Level++;
            UpdateValues();
        }
    }
}
