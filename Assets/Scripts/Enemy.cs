using Cysharp.Threading.Tasks;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour, IPoolable
{
    public event Action<int> OnHealthChanged;
    public GameObject GameObject => gameObject;
    [field: SerializeField] public int MaxHealth { get; private set; }
    [SerializeField] private int _health;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public int Health {
        get => _health;
        private set {
            if (value <= 0)
            {
                value = 0;
                Die();
            }
            _health = value;
            OnHealthChanged?.Invoke(value);
        }
    }

    /// <summary>
    /// Enemy의 데이터, 스프라이터 초기화
    /// </summary>
    /// <param name="enemyData"> 스프레드시트에서 가져온 enemy의 정보 </param>
    /// <param name="sprite"> Id에 맞는 스프라이트 </param>
    public void Init(EnemyData enemyData, Sprite sprite)
    {
        gameObject.name = enemyData.Name;
        MaxHealth = enemyData.Health;
        _health = MaxHealth;
        _spriteRenderer.sprite = sprite;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    } 

    private void Die()
    {
        Debug.Log($"{gameObject.name} died");
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        ObjectPool.Instance.Return(this);
    }
}
