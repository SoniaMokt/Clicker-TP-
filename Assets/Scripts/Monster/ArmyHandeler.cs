using System.Collections;
using UnityEditor;
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

        [ContextMenu("TINE")]
        private void TintAll()
        {
            _Bees = GetComponentsInChildren<Bee>(true);
            foreach(Bee bee in _Bees)
            {
                bee.Tint(_StateColors[0]);
            }
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

            foreach(Bee bee in _Bees)
            {
                bee.transform.localPosition = Random.insideUnitCircle * range;
            }
            SetActiveEnoughBees(_Butineuses, _Butineuse);
            SetActiveEnoughBees(_Darts, _Dart);
            SetActiveEnoughBees(_Shields, _Shield);
            StartCoroutine(BeeStateChange());
        }


        public IEnumerator BeeStateChange()
        {
            while(Application.isPlaying || enabled)
            {
                int random = Random.Range(0, _Bees.Length);
                Bee bee = _Bees[random];
                random = Random.Range(0, _StateColors.Length);
                bee.Tint(_StateColors[random]);
                yield return new WaitForSeconds(Random.Range(0, _ChangeStateInterval));
            }
        }


        private void SetActiveEnoughBees(Component[] bees, int count)
        {
            int max = count;
            if(count > bees.Length)
            {
                max = bees.Length;
            }

            for(int i = 0; i < max; i++)
            {
                bees[i].gameObject.SetActive(true);
            }
            for(int i = max; i < bees.Length; i++)
            {
                bees[i].gameObject.SetActive(false);
            }
            for(int i = 0; i < bees.Length; i++)
            {
                bees[i].transform.localScale = Vector3.one * .1f;
                bees[i].transform.localScale += Vector3.one * .01f * (count / bees.Length);
            }
            for(int i = 0; i < count % bees.Length; i++)
            {
                bees[i].transform.localScale += Vector3.one * .03f;
            }
        }


        private void OnDrawGizmosSelected()
        {
            foreach(Transform child in transform)
            {
                Handles.DrawWireArc(child.transform.position, Vector3.forward, Vector3.right, 360, range);
                //Gizmos.DrawWireSphere(child.transform.position, range);
            }
        }
    }
}
