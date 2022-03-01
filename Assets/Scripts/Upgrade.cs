using UnityEngine;

[System.Serializable]
public struct Upgrade {
    public string Name => name;
    public Sprite Sprite => sprite;
    public int Price => cost;
    public int DPS => dps;


    [SerializeField] private string name;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int cost, dps;
}
