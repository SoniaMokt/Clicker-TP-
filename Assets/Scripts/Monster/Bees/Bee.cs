using UnityEngine;

namespace BeeClicker.Monster
{
    public class Bee : MonoBehaviour
    {
        [SerializeField, Etienne.ReadOnly] private BeeState _State;
        [SerializeField] private SpriteRenderer[] _TintableRenderers;
        [SerializeField] private GameObject[] _Normal;
        [SerializeField] private GameObject[] _Happy;
        [SerializeField] private GameObject[] _Grumpy;

        public void Tint(int state, Color color)
        {
            foreach(SpriteRenderer tintable in _TintableRenderers)
            {
                tintable.color = color;
            }
            _State = (BeeState)state;

            foreach(GameObject item in _Normal)
            {
                item.SetActive(_State == BeeState.Normal);
            }
            foreach(GameObject item in _Happy)
            {
                item.SetActive(_State == BeeState.Happy);
            }
            foreach(GameObject item in _Grumpy)
            {
                item.SetActive(_State == BeeState.Grumpy);
            }
        }
    }
}
