using UnityEngine;

[System.Serializable]
public struct SpawnData
{
    public int Id;
    public float Rate;

    public SpawnData(int id, float rate)
    {
        Id = id;
        Rate = rate;
    }
}
