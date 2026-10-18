using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    /// <summary>
    /// 오브젝트 풀 싱글톤
    /// </summary>
    public static ObjectPool Instance;

    [SerializeField] private GameObject _enemyPrefab;
    private Stack<IPoolable> _objectStack = new();
    private GameObject _poolParent;
    private const int INITIAL_POOL_SIZE = 5;

    private void Awake() => Init();

    /// <summary>
    /// 오브젝트 풀에서 꺼내기
    /// </summary>
    /// <returns></returns>
    public IPoolable Take()
    {
        if (_objectStack.Count != 0)
        {
            return _objectStack.Pop();
        }
        else
        {
            GameObject gameObject = Instantiate(_enemyPrefab);
            gameObject.transform.SetParent(_poolParent.transform);
            return gameObject.GetComponent<IPoolable>();
        }
    }

    /// <summary>
    /// 오브젝트 풀에 넣기
    /// </summary>
    /// <param name="poolable"></param>
    public void Return(IPoolable poolable)
    {
        _objectStack.Push(poolable);
        poolable.GameObject.SetActive(false);
        poolable.GameObject.transform.SetParent(_poolParent.transform);
    }

    private void CreatePool()
    {
        _poolParent = new GameObject();
        _poolParent.transform.SetParent(transform);
        _poolParent.name = $"{_enemyPrefab.name} Parent";

        for (int i = 0; i < INITIAL_POOL_SIZE; i++)
        {
            GameObject gameObject = Instantiate(_enemyPrefab, _poolParent.transform);
            _objectStack.Push(gameObject.GetComponent<IPoolable>());
            gameObject.SetActive(false);
        }
    }

    private void Init()
    {
        SetSingleton();
        CreatePool();
    }

    private void SetSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}
