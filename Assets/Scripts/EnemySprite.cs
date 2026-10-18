using UnityEngine;

[System.Serializable]
public struct EnemySprite
{
    public int Id;
    public Sprite Sprite;

    public EnemySprite(int id, Sprite sprite)
    {
        Id = id;
        Sprite = sprite;
    }
}
