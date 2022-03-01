using UnityEngine;

[System.Serializable]
public struct MonsterData {
    public string Name { get => name; set => name = value; }
    public int Life { get => life; set => life = value; }
    public Sprite Sprite { get => sprite; set => sprite = value; }

    [SerializeField] private string name;
    [SerializeField] private int life;
    [SerializeField] private Sprite sprite;
}
