using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] public int maxHealth = 10;
    [SerializeField] public int currentHealth;

    public HealthBar healthBar;

    [SerializeField] private Animator playerAnim;

    [SerializeField] private Animator anim; //game over

    private void Awake()
    {
        playerAnim = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Damage(1);
        }
    }

    public void Damage(int damageAmount)
    {
        playerAnim.SetTrigger("hurt");
        currentHealth -= damageAmount;
        healthBar.SetHealth(currentHealth);
        Debug.Log($"Player took {damageAmount} damage! Current health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player has died.");
        Time.timeScale = 0;
        anim.SetBool("isQuest", true);
    }
}
