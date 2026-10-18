using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using System;

public class PlayerAction : MonoBehaviour
{
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private PlayerDetection _playerDetection;
    private List<GameObject> _detectedTargets => _playerDetection.DetectedTargets;
    private GameObject _target;
    private bool _isAttacking;

    private void Update()
    {
        FindTarget();
        AttackTarget().Forget();
    }

    private void FindTarget()
    {
        if (_detectedTargets.Count == 0)
        {
            _target = null;
            return;
        }

        GameObject newTarget = _detectedTargets[0];
        float minDistance = Vector3.Distance(_detectedTargets[0].transform.position, transform.position);

        foreach (GameObject gameObject in _playerDetection.DetectedTargets) {
            if (gameObject.activeSelf == false)
            {
                _playerDetection.RemoveTarget(gameObject);
                break;
            }
            
            if (gameObject == _target) break;

            float distance = Vector3.Distance(gameObject.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                newTarget = gameObject;
            }
        }

        _target = newTarget;
    }

    private async UniTaskVoid AttackTarget()
    {
        if (_target == null || _isAttacking == true) return;

        _isAttacking = true;
        Enemy enemy = _target.GetComponent<Enemy>();
        enemy.TakeDamage(_playerStat.Attack);
        await UniTask.Delay(TimeSpan.FromSeconds(1 / _playerStat.AttackSpeed));

        _isAttacking = false;
    }
}
