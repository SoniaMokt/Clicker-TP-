using UnityEngine;
using UnityEngine.UI;

namespace BeeClicker
{
    public class HiveUIHandeler : MonoBehaviour
    {
        [SerializeField] private Slider _HoneyBar;
        [SerializeField] private TMPro.TextMeshProUGUI _HoneyText;

        private void Awake()
        {
            GameManager.Instance.Hive.OnLevelChanged += UpdateLevelVisuals;
            GameManager.Instance.Hive.OnValueChanged += UpdateVisuals;
        }

        private void UpdateLevelVisuals(int value)
        {
            _HoneyBar.maxValue = value;
            UpdateVisuals(value);
        }

        private void UpdateVisuals(int value)
        {
            _HoneyBar.value = value;
            _HoneyText.text = $"{value.KiloFormat()} / {_HoneyBar.maxValue.KiloFormat()}";
        }
    }
}
