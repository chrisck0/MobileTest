using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Tooltip("Center position for random spawn")]
    [SerializeField] private Transform _targetTransform;
    [SerializeField] private float _spawnRange;

    [field: SerializeField] private List<EnemySprite> _enemySprites;

    [Tooltip("Rate Sum must be 1")]
    [field: SerializeField] private List<SpawnData> _spawnDatas;
    [Tooltip("Total number of enemies to spawn")]
    [SerializeField] private int _spawnCount;

    private Dictionary<int, EnemyData> _enemyDataDict = new();
    private Dictionary<int, Sprite> _enemySpriteDict = new();

    private void Start()
    {
        Init();
        SpawnEnemies();
    }

    private void Init()
    {
        GoogleSheetSO googleSheetSO = GoogleSheetManager.SO<GoogleSheetSO>();

        foreach (EnemyData enemyData in googleSheetSO.EnemyDataList)
        {
            _enemyDataDict.Add(enemyData.Id, enemyData);
        }

        foreach (EnemySprite sprite in _enemySprites)
        {
            _enemySpriteDict.Add(sprite.Id, sprite.Sprite);
        }
    }

    private void SpawnEnemies()
    {
        float cumulative = 0f;
        int count = 1;
        foreach (SpawnData spawnData in _spawnDatas)
        {
            if (!_enemyDataDict.ContainsKey(spawnData.Id)
                || !_enemySpriteDict.ContainsKey(spawnData.Id))
                continue;

            cumulative += spawnData.Rate;

            while (count <= _spawnCount * cumulative)
            {
                IPoolable poolable = ObjectPool.Instance.Take();
                Enemy enemy = poolable as Enemy;

                if (enemy == null)
                {
                    poolable.ReturnToPool();
                    continue;
                }

                poolable.GameObject.SetActive(true);
                poolable.GameObject.transform.position = RandomPosition();
                count++;

                enemy.Init(_enemyDataDict[spawnData.Id], _enemySpriteDict[spawnData.Id]);
            }
        }
    }


    /// <summary>
    /// Id에 존재하면 EnemySprite를 반환, 없으면 null 반환
    /// </summary>
    /// <param name="id"> Enemy Id </param>
    /// <returns></returns>
    public Sprite GetEnemySprite(int id)
    {
        if (!_enemySpriteDict.ContainsKey(id)) return null;

        return _enemySpriteDict[id];
    }

    private Vector3 RandomPosition()
    {
        float randX = Random.Range(-_spawnRange, _spawnRange);
        float randZ = Random.Range(-_spawnRange, _spawnRange);
        Vector3 RandomPostion = new Vector3(randX, 0f, randZ);

        return _targetTransform.position + RandomPostion;
    }
}
