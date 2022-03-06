using System.Collections;
using UnityEngine;

namespace BeeClicker
{
    using Store;
    public abstract class Level : MonoBehaviour, IClickable
    {
        public System.Action OnClick;
        public System.Action<int, bool> OnCompleted;
        public System.Action<int> OnValueChanged;
        public System.Action<int> OnLevelChanged;
        public System.Action<bool> OnEnable;

        [SerializeField] protected int _Level;
        [SerializeField] private AnimationCurve _ValueCurve;
        [SerializeField] private DamageType _DamageType;
        [SerializeField] protected Transform _Visual;
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
                Complete(_Level);
                return;
            }
            _CurrentValue -= amount;
            UpdateValue();
        }

        protected virtual void Complete(int level, bool completed = true)
        {
            OnCompleted?.Invoke(level, completed);
        }

        protected virtual void LevelUp(int level)
        {
            _Level = level;
            _CurrentMaxValue = Mathf.RoundToInt(_ValueCurve.Evaluate(_Level));
            _CurrentValue = _CurrentMaxValue;
            ChangeLevel();
        }

        protected virtual void Disable(int level, bool completed = true)
        {
            gameObject.SetActive(false);
        }

        protected virtual void Enable(int level, bool completed = true)
        {
            gameObject.SetActive(true);
            StartDPS();
            if(completed) LevelUp(level + 1);
            else RestartLevel();
            OnEnable?.Invoke(completed);
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
                int amount = 0;
                if(_DamageType == DamageType.HPS)
                {
                    amount = GameManager.Instance.StatsHandeler.HPS();
                }
                if(_DamageType == DamageType.DPS)
                {
                    amount = GameManager.Instance.StatsHandeler.DPS();
                }
                LooseValue(amount);
            }
        }

        public abstract void Click();

        public void ChangeLevel(int index)
        {
            Complete(index, false);
            LevelUp(index);
        }
    }
}