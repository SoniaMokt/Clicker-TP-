using UnityEngine;

namespace BeeClicker.Monster
{
    public class Bee : MonoBehaviour
    {
        [SerializeField, Etienne.ReadOnly] private BeeState _State;
        [SerializeField] private SpriteRenderer[] _TintableRenderers;

        public void Tint(Color color)
        {
            foreach(SpriteRenderer tintable in _TintableRenderers)
            {
                tintable.color = color;
            }
        }
    }
}
