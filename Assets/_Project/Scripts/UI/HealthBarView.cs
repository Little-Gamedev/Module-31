using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour, IInitializable
{
    [SerializeField] private Image _fillImage;

    private Character _character;

    public void Initialize()
    {
        _character = GetComponentInParent<Character>();

        _character.CurrentHealth.Changed += OnHealthChanged;

        Show(_character.CurrentHealth.Value);
    }

    private void OnDestroy()
    {
        if (_character == null)
            return;

        _character.CurrentHealth.Changed -= OnHealthChanged;
    }

    private void OnHealthChanged(int oldValue, int newValue) => Show(newValue);

    private void Show(int value) => _fillImage.fillAmount = (float)value / _character.MaxHealth;
}