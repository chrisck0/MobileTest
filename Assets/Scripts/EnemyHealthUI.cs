using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthUI : MonoBehaviour
{
    [SerializeField] private Image _healthImage;
    [SerializeField] private Enemy _enemy;

    //---------------
    private void OnEnable() => BindHealthEvents();
    private void OnDisable() => UnbindHealthEvents();
    //---------------

    private void UpdateHealthUI(int health)
    {
        _healthImage.transform.localScale = new Vector3((float)health / _enemy.MaxHealth, 1f, 1f);
    }

    private void BindHealthEvents()
    {
        _enemy.OnHealthChanged += UpdateHealthUI;
    }

    private void UnbindHealthEvents()
    {
        _enemy.OnHealthChanged -= UpdateHealthUI;
    }
}
