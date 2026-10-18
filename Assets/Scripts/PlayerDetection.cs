using UnityEngine;
using System.Collections.Generic;

public class PlayerDetection : MonoBehaviour
{
    [field: SerializeField] private List<GameObject> _detectedTargets;
    [SerializeField] private PlayerStat _playerStat;
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private SphereCollider _sphereCollider;
    private float _range => _sphereCollider.radius;

    /// <summary>
    /// 플레이어가 감지한 target 리스트
    /// </summary>
    public List<GameObject> DetectedTargets => _detectedTargets;

    //---------------
    private void OnEnable()
    {
        BindPlayerStatEvents();
        ChangeDetectionRange(_playerStat.DetectionRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_targetLayerMask.Contains(other))
        {
            _detectedTargets.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (_targetLayerMask.Contains(other) && _detectedTargets.Contains(other.gameObject))
        {
            _detectedTargets.Remove(other.gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
    private void OnDisable() => UnbindPlayerStatEvents();
    //---------------

    /// <summary>
    /// DetectedTargets 리스트에서 target을 제거
    /// </summary>
    /// <param name="target"> target 게임 오브젝트 </param>
    public void RemoveTarget(GameObject target)
    {
        if (_detectedTargets.Contains(target))
        {
            _detectedTargets.Remove(target);
        }
    }

    private void ChangeDetectionRange(float range)
    {
        _sphereCollider.radius = range;
    }

    private void BindPlayerStatEvents()
    {
        _playerStat.OnDetectionRangeChanged += ChangeDetectionRange;
    }

    private void UnbindPlayerStatEvents()
    {
        _playerStat.OnDetectionRangeChanged -= ChangeDetectionRange;
    }
}
