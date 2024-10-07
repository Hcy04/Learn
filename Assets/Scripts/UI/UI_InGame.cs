using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private Slider slider;

    [SerializeField] private Image swordImage;

    private void Start()
    {
        stats.onHealthChanged += UpdateHealthBar;
    }

    public void UpdateHealthBar()
    {
        slider.maxValue = stats.maxHealth.GetValue();
        slider.value = stats.currentHealth;
    }

    public void UpdateSwordImage(bool canUse)
    {
        if (canUse) swordImage.color = Color.white;
        else swordImage.color = Color.grey;
    }
}
