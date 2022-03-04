using System.Collections;
using UnityEngine;

namespace BeeClicker
{
    public abstract class Level : MonoBehaviour, IClickable
    {
        public System.Action OnClick;
        public System.Action<bool> OnCompleted;
        public System.Action<int> OnValueChanged;
        public System.Action<int> OnLevelChanged;

        [SerializeField] private int _Level;
        [SerializeField] private AnimationCurve _ValueCurve;
        private int _CurrentValue;
        protected int _CurrentMaxValue;

        protected virtual void Start()
        {
            RestartLevel();
        }

        protected virtual void RestartLevel()
        {
            _CurrentMaxValue = Mathf.RoundToInt(_ValueCurve.Evaluate(_Level));
            _CurrentValue = _CurrentMaxValue;
            ChangeLevel();
        }

        protected virtual void ChangeLevel()
        {
            OnLevelChanged?.Invoke(_CurrentMaxValue);
        }

        protected virtual void UpdateValue()
        {
            OnValueChanged?.Invoke(_CurrentValue);
        }

        protected virtual void LooseValue(int amount)
        {
            if(_CurrentValue - amount <= 0)
            {
                Complete();
                return;
            }
            _CurrentValue -= amount;
            UpdateValue();
        }

        protected virtual void Complete(bool completed = true)
        {
            OnCompleted?.Invoke(completed);
        }

        protected virtual void LevelUp()
        {
            _Level++;
            _CurrentMaxValue = Mathf.RoundToInt(_ValueCurve.Evaluate(_Level));
            _CurrentValue = _CurrentMaxValue;
            ChangeLevel();
        }

        protected virtual void Disable(bool completed = true)
        {
            gameObject.SetActive(false);
        }

        protected virtual void Enable(bool completed = true)
        {
            gameObject.SetActive(true);
            StartDPS();
            if(completed) LevelUp();
            else RestartLevel();
        }

        protected virtual void StartDPS()
        {
            StartCoroutine(DPSRoutine());
        }


        protected virtual IEnumerator DPSRoutine()
        {
            while(enabled)
            {
                yield return new WaitForSeconds(1f);
                LooseValue(GameManager.Instance.StatsHandeler.DPS());
            }
        }

        public abstract void Click();
    }
}