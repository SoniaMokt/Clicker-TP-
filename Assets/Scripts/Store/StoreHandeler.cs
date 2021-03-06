using UnityEngine;

namespace BeeClicker.Store
{
    public class StoreHandeler : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TMPro.TextMeshProUGUI _CurrencyText;
        [SerializeField] private TMPro.TextMeshProUGUI _DPSText;
        [SerializeField] private TMPro.TextMeshProUGUI _HPSText;
        [SerializeField] private TMPro.TextMeshProUGUI _ClickDamageText;
        [Header("Items")]
        [SerializeField] private ItemScriptableObject[] _Items;

        private ItemHandeler _ItemContainer;

        private void Awake()
        {
            StatsHandeler statsHandeler = GameManager.Instance.StatsHandeler;
            statsHandeler.OnCurrencyChange += UpdateCurrency;
            statsHandeler.OnDPSChange += UpdateDPS;
            statsHandeler.OnHPSChange += UpdateHPS;
            statsHandeler.OnClickDamageChange += UpdateClickDamage;

            GameManager.Instance.Hive.OnCompleted += Disable;
            GameManager.Instance.Monster.OnCompleted += Enable;

            _ItemContainer = GetComponentInChildren<ItemHandeler>();
            _ItemContainer.Handle(_Items);
        }

        private void Disable(int level, bool obj)
        {
            gameObject.SetActive(false);
        }

        private void Enable(int level, bool obj)
        {
            gameObject.SetActive(true);
        }

        private void UpdateCurrency(int amount)
        {
            _CurrencyText.text = amount.KiloFormat();
        }

        private void UpdateHPS(int amount)
        {
            _HPSText.text = amount.KiloFormat();
        }

        private void UpdateDPS(int amount)
        {
            _DPSText.text = amount.KiloFormat();
        }

        private void UpdateClickDamage(int amount)
        {
            _ClickDamageText.text = amount.KiloFormat();
        }
    }
}
