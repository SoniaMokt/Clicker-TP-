using UnityEngine;

namespace BeeClicker.Store
{
    public class UpgradeHandeler : MonoBehaviour, IHandelable<int>
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _Prefab;

        public void Handle(int[] h)
        {
            Upgrade[] childUpgrades = GetComponentsInChildren<Upgrade>();
            if(childUpgrades.Length == h.Length) return;
            if(childUpgrades.Length > h.Length)
            {
                for(int i = childUpgrades.Length - 1; i >= h.Length; i--)
                {
                    GameObject.DestroyImmediate(childUpgrades[i].gameObject);
                }
            }
            if(childUpgrades.Length < h.Length)
            {
                for(int i = childUpgrades.Length; i < h.Length; i++)
                {
                    GameObject go = GameObject.Instantiate(_Prefab, transform);
                    go.GetComponentInChildren<ISetupable>()?.Setup();
                }
            }
        }
    }
}
