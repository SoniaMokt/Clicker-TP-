using DG.Tweening;
using System.Collections;
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
            Enable(false);
        }


        public override void Click()
        {
            LooseValue(GameManager.Instance.StatsHandeler.Click());
            transform.DOComplete();
            transform.DOPunchScale(Vector3.one * .1f, .4f);
            OnClick?.Invoke();
        }
    }
}