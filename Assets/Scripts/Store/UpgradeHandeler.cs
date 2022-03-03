using UnityEngine;

namespace BeeClicker.Store
{
    public class UpgradeHandeler : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject _UpgradePrefab;


        public void Handle(int[] upgrades)
        {
            Upgrade[] childUpgrades = GetComponentsInChildren<Upgrade>();
            Debug.Log(childUpgrades.Length);
            if(childUpgrades.Length == upgrades.Length) return;
            if(childUpgrades.Length > upgrades.Length)
            {
                for(int i = childUpgrades.Length - 1; i >= upgrades.Length; i--)
                {
                    GameObject.DestroyImmediate(childUpgrades[i].gameObject);
                }
            }
            if(childUpgrades.Length < upgrades.Length)
            {
                for(int i = childUpgrades.Length; i < upgrades.Length; i++)
                {
                    GameObject.Instantiate(_UpgradePrefab, transform);
                }
            }
        }
    }
}
