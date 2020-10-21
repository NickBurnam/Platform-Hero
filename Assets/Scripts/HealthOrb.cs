using UnityEngine;

public class HealthOrb : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int healthBonus = 25;

    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerHealth.GetHP() < playerHealth.maxHP)
        {
            Destroy(gameObject);
            playerHealth.SetHP(playerHealth.maxHP);
        }
    }
}
