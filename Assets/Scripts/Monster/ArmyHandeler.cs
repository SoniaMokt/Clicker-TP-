using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BeeClicker.Monster
{
    public class ArmyHandeler : MonoBehaviour
    {
        [SerializeField] private float range;
        [SerializeField] private int butineuse;
        [SerializeField] private int dart;
        [SerializeField] private int shield;

        [ContextMenu("TEST")]
        void test()
        {
            var bees = GetComponentsInChildren<Bee>();
            foreach(var bee in bees)
            {
                bee.transform.localPosition =Random.insideUnitCircle * range;
            }
        }

        private void OnDrawGizmosSelected()
        {
            foreach(Transform child in transform)
            {
                Gizmos.DrawWireSphere(child.transform.position, range);
            }
        }
    }
}
