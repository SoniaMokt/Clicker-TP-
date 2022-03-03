using UnityEngine;

namespace BeeClicker.Store
{
    public class StoreHandeler : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TMPro.TextMeshProUGUI _CurrencyText;
        [SerializeField] private TMPro.TextMeshProUGUI _DPSText;
        [SerializeField] private TMPro.TextMeshProUGUI _ClickDamageText;
        [Header("Items")]
        [SerializeField] private ItemScriptableObject[] _Items;

        private ItemHandeler _ItemContainer;

        private void Awake()
        {
            StatsHandeler statsHandeler = GameManager.Instance.StatsHandeler;
            statsHandeler.OnCurrencyChange += UpdateCurrency;
            statsHandeler.OnDPSChange += UpdateDPS;
            statsHandeler.OnClickDamageChange += UpdateClickDamage;

            _ItemContainer = GetComponentInChildren<ItemHandeler>();
            _ItemContainer.Handle(_Items);
        }

        private void UpdateCurrency(int amount)
        {
            _CurrencyText.text = amount.ToString();
        }

        private void UpdateDPS(int amount)
        {
            _DPSText.text = amount.ToString();
        }

        private void UpdateClickDamage(int amount)
        {
            _ClickDamageText.text = amount.ToString();
        }
    }
}
