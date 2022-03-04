using UnityEngine;
using UnityEngine.UI;

namespace BeeClicker.Monster
{
    public class MonsterUIHandeler : MonoBehaviour
    {
        [SerializeField] private Slider _LifeBar;
        [SerializeField] private TMPro.TextMeshProUGUI _LifeText;
        [SerializeField] private Slider _TimerBar;
        [SerializeField] private TMPro.TextMeshProUGUI _TimerText;
        private float _Timer;

        private void Awake()
        {
            Monster monster = GameManager.Instance.Monster;
            monster.OnLevelChanged += UpdateLevelVisuals;
            monster.OnValueChanged += UpdateVisuals;
            monster.OnTimerStart += SetTimer;
        }

        private void Update()
        {
            _Timer -= Time.deltaTime;
            _TimerText.text = $"{_Timer:0.0s}";
            _TimerBar.value = _Timer;
        }

        private void SetTimer(float timer)
        {
            _Timer = timer;
            _TimerBar.maxValue = timer;
            _TimerBar.value = timer;
        }

        private void UpdateLevelVisuals(int value)
        {
            _LifeBar.maxValue = value;
            UpdateVisuals(value);
        }

        private void UpdateVisuals(int value)
        {
            _LifeBar.value = value;
            _LifeText.text = $"{value.KiloFormat()} / {_LifeBar.maxValue.KiloFormat()}";
        }
    }
}
