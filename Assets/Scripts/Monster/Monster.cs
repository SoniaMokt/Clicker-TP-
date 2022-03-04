using DG.Tweening;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

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
        }

        protected override void Start()
        {
            base.Start();
            Disable(false);
        }

        protected override void Enable(bool completed = true)
        {
            base.Enable(completed);
            StartCoroutine( Timer());
        }

        protected override void Disable(bool completed = true)
        {
            base.Disable(completed);
            StopAllCoroutines();
        }

        public IEnumerator Timer()
        {
            OnTimerStart?.Invoke(_Timer);
            yield return new    WaitForSeconds(_Timer);
            Complete(false);
        }

        protected override void Complete(bool completed = true)
        {
            base.Complete(completed);
            GameManager.Instance.GainCurrency(Mathf.RoundToInt(_CurrentMaxValue * .5f));
        }

        public override void Click()
        {
            LooseValue(GameManager.Instance.StatsHandeler.Click(false));
            transform.DOComplete();
            transform.DOPunchScale(Vector3.one * .1f, .4f);
            OnClick?.Invoke();
        }
    }
}