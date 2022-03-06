using DG.Tweening;
using UnityEngine;

namespace BeeClicker
{
    public class Hive : Level
    {

        private void Awake()
        {
            OnCompleted += Disable;
            GameManager.Instance.Monster.OnCompleted += Enable;
        }
        protected override void Start()
        {
            base.Start();
            Enable(0, false);
        }


        public override void Click()
        {
            LooseValue(GameManager.Instance.StatsHandeler.Click());
            _Visual.DOComplete();
            _Visual.DOPunchScale(Vector3.one * .1f, .4f);
            OnClick?.Invoke();
        }
    }
}