using UnityEngine;

namespace BeeClicker.Store
{
    public class ItemHandeler : MonoBehaviour, IHandelable<ItemScriptableObject>
    {
        [SerializeField] private GameObject _Prefab;

        public void Handle(ItemScriptableObject[] h)
        {
            Item[] childItems = GetComponentsInChildren<Item>();
            if(childItems.Length == h.Length) return;
            if(childItems.Length > h.Length)
            {
                for(int i = childItems.Length - 1; i >= h.Length; i--)
                {
                    if(!Application.isPlaying) GameObject.DestroyImmediate(childItems[i].gameObject);
                    else GameObject.Destroy(childItems[i].gameObject);
                }
            }
            if(childItems.Length < h.Length)
            {
                for(int i = childItems.Length; i < h.Length; i++)
                {
                    GameObject go = GameObject.Instantiate(_Prefab, transform);
                    go.GetComponentInChildren<ISetupable>()?.Setup();
                }
            }
        }
    }
}
