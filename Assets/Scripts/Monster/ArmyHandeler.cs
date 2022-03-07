using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeClicker.Monster
{
    public enum BeeState { Normal, Happy, Grumpy }
    public class ArmyHandeler : MonoBehaviour
    {

        [SerializeField] private Color[] _StateColors;
        [SerializeField] private float _ChangeStateInterval = .5f;
        [SerializeField] private float range;
        private int _Butineuse;
        private int _Dart;
        private int _Shield;
        private Bee[] _Bees;
        private List<Bee> _ActiveBees = new List<Bee>();
        private Butineuse[] _Butineuses;
        private Dart[] _Darts;
        private Shield[] _Shields;

        private void Awake()
        {
            _Bees = GetComponentsInChildren<Bee>(true);
            _Butineuses = GetComponentsInChildren<Butineuse>(true);
            _Darts = GetComponentsInChildren<Dart>(true);
            _Shields = GetComponentsInChildren<Shield>(true);

            GameManager.Instance.OnBeePurchased += PurchaseBee;
        }

        private void PurchaseBee(string bee, int amount)
        {
            if(bee == nameof(Butineuse))
            {
                _Butineuse = amount;
            } else if(bee == nameof(Dart))
            {
                _Dart = amount;
            } else if(bee == nameof(Shield))
            {
                _Shield = amount;
            }
        }

        private void OnEnable()
        {
            SetActiveEnoughBees(_Butineuses, _Butineuse);
            SetActiveEnoughBees(_Darts, _Dart);
            SetActiveEnoughBees(_Shields, _Shield);
            if(_ActiveBees.Count <= 0) return;
            foreach(Bee bee in _ActiveBees)
            {
                bee.Tint(0, _StateColors[0]);
                bee.transform.localPosition = Random.insideUnitCircle * range;
            }
            StartCoroutine(BeeStateChange());
            StartCoroutine(BeePathChange());
        }

        private void OnDisable()
        {
            StopAllCoroutines();
        }


        public IEnumerator BeeStateChange()
        {
            while(Application.isPlaying || enabled)
            {
                int random = Random.Range(0, _ActiveBees.Count);
                Bee randomBee = _ActiveBees[random];
                random = Random.Range(0, _StateColors.Length);
                randomBee.Tint(random, _StateColors[random]);
                yield return new WaitForSeconds(Random.Range(0, _ChangeStateInterval));
            }
        }
        public IEnumerator BeePathChange()
        {
            while(Application.isPlaying || enabled)
            {
                foreach(Bee bee in _Bees)
                {
                    bee.transform.DOKill();
                    bee.transform.DOLocalMove(Random.insideUnitCircle * range, _ChangeStateInterval).SetEase(Ease.InOutQuad);
                }
                yield return new WaitForSeconds(Random.Range(_ChangeStateInterval * .5f, _ChangeStateInterval * 2));
            }
        }


        private void SetActiveEnoughBees(Component[] bees, int count)
        {
            int max = count;
            if(count > bees.Length)
            {
                max = bees.Length;
            }
            _ActiveBees.Clear();
            for(int i = 0; i < max; i++)
            {
                bees[i].gameObject.SetActive(true);
                _ActiveBees.Add(bees[i] as Bee);
            }
            for(int i = max; i < bees.Length; i++)
            {
                bees[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < bees.Length; i++)
            {
                bees[i].transform.localScale = Vector3.one * .1f;
                bees[i].transform.localScale += (count / bees.Length) * .01f * Vector3.one;
            }
            for(int i = 0; i < count % bees.Length; i++)
            {
                bees[i].transform.localScale += Vector3.one * .03f;
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            foreach(Transform child in transform)
            {
                UnityEditor.Handles.DrawWireArc(child.transform.position, Vector3.forward, Vector3.right, 360, range);
            }
        }
#endif
    }
}
