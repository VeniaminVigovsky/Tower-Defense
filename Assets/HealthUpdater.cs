using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUpdater : MonoBehaviour
{
    [SerializeField]
    private Tower tower;

    private Image healthBar;

    private int maxHealth;

    private void Awake()
    {
        healthBar = GetComponentInChildren<Image>();

    }

    private void Start()
    {
        if (tower != null)
            maxHealth = tower.MaxHealth;
    }

    // Update is called once per frame
    private void Update()
    {
        healthBar.fillAmount = (float)tower.Health / maxHealth;

        UpdateColor(healthBar.fillAmount);
    }

    private void UpdateColor(float fillAmount)
    {

        float r = Mathf.Clamp((1f - 0.72f * fillAmount), 0.27f, 0.72f);
        float g = Mathf.Clamp((0.37f + 0.57f * fillAmount), 0.37f, 0.72f);
        healthBar.color = new Color(r, g, healthBar.color.b);
        
    }
}
