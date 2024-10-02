using UnityEngine;
using UnityEngine.UI;

public class UI_HealthBar : MonoBehaviour
{
    private Character character;
    private CharacterStats stats;

    private RectTransform rectTransform;
    private Slider slider;

    private void Start()
    {
        character = GetComponentInParent<Character>();
        stats = GetComponentInParent<CharacterStats>();

        rectTransform = GetComponent<RectTransform>();
        slider = GetComponentInChildren<Slider>();

        character.onFlipped += FlipUI;
        stats.onHealthChanged += UpdateHealthUI;
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = stats.maxHealth.GetValue();
        slider.value = stats.currentHealth;

        if (slider.value <= 0) Destroy(this.gameObject);
    }

    private void FlipUI() => rectTransform.Rotate(0, 180, 0);
    private void OnDisable()
    {
        character.onFlipped -= FlipUI;
        stats.onHealthChanged -= UpdateHealthUI;
    }
}
