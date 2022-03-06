using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BeeClicker.Monster
{
    public class Monster : Level
    {
        public System.Action<float> OnTimerStart;

        [SerializeField, Tooltip("In Seconds")] private float _Timer = 30f;

        private void Awake()
        {
            GameManager.Instance.Hive.OnCompleted += Enable;
            OnCompleted += Disable;
            GetComponentInChildren<Button>().onClick.AddListener(Exit);
        }

        private void Exit()
        {
            Complete(_Level, false);
        }

        protected override void Start()
        {
            base.Start();
            Disable(0, false);
        }

        protected override void Enable(int level, bool completed = true)
        {
            base.Enable(level - 1, completed);
            StartCoroutine(Timer());
        }

        protected override void Disable(int level, bool completed = true)
        {
            base.Disable(level, completed);
            StopAllCoroutines();
        }

        public IEnumerator Timer()
        {
            OnTimerStart?.Invoke(_Timer);
            yield return new WaitForSeconds(_Timer);
            Complete(_Level, false);
        }

        protected override void Complete(int level, bool completed = true)
        {
            base.Complete(level, completed);
            if(!completed) return;
            GameManager.Instance.GainCurrency(Mathf.RoundToInt(_CurrentMaxValue * .5f));
        }

        public override void Click()
        {
            LooseValue(GameManager.Instance.StatsHandeler.Click(false));
            _Visual.DOComplete();
            _Visual.DOPunchScale(Vector3.one * .1f, .4f);
            OnClick?.Invoke();
        }
    }
}