using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    public Slider healthSlider;
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();

        if (playerHealth != null)
        {
            // Đăng ký sự kiện cập nhật UI khi HP thay đổi
            playerHealth.OnHealthChangedEvent += UpdateHealthUI;
            UpdateHealthUI(playerHealth.currentHealth, playerHealth.maxHealth);
        }
    }

    public void UpdateHealthUI(int currentHealth, int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }


}





