using System;
using UnityEngine;

public class PlayerStat : MonoBehaviour
{
    public event Action<float> OnDetectionRangeChanged;

    public int Attack {
        get => _attack;
        private set {
            _attack = value;
        }
    }

    public float AttackSpeed {
        get => _attackSpeed;
        private set {
            _attackSpeed = value;
        }
    }

    public int MovementSpeed {
        get => _movementSpeed;
        private set {
            _movementSpeed = value;
        }
    }

    public float DetectionRange {
        get => _detectionRange;
        private set {
            _detectionRange = value;
            OnDetectionRangeChanged?.Invoke(_detectionRange);
        }
    }

    [SerializeField] private int _attack;
    [SerializeField] private float _attackSpeed;
    [SerializeField] private int _movementSpeed;
    [SerializeField] private float _detectionRange;
}
