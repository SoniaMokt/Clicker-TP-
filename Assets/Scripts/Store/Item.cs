#if UNITY_EDITOR
#endif
using UnityEngine;
using UnityEngine.UI;

namespace BeeClicker.Store
{
    public class Item : MonoBehaviour
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
        private UpgradeHandeler _UpgradeHandeler;

        [ContextMenu("Setup")]
        private void SetupItem()
        {
            _NameText.text = _Item.Name;
            _DescriptionText.text = _Item.Description;
            _LevelText.text = _Level.ToString();
            _PriceText.text = Mathf.RoundToInt(_Item.Prices.Evaluate(_Level)).ToString();
            _Icon.sprite = _Item.Icon;
            UpgradeHandeler upgradeHandeler = _UpgradeHandeler ?? GetComponentInChildren<UpgradeHandeler>();
            upgradeHandeler.Handle(_Item.Upgrades);
        }
    }
}
