using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CrumblingPlatform : MonoBehaviour
{
    public float maxHealth = 5f;
    public float healthDecreaseRate = 1f;
    public float healthRegenRate = 2f;
    public float fallDistance = 50f;
    public float respawnDelay = 3f;

    private float currentHealth;
    private Vector3 originalPosition;
    private bool isFalling = false;
    private bool playerOnPlatform = false;

    public Slider healthBar; // Optional for visual UI

    void Start()
    {
        currentHealth = maxHealth;
        originalPosition = transform.position;

        if (healthBar)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        if (isFalling) return;

        if (playerOnPlatform)
        {
            currentHealth -= healthDecreaseRate * Time.deltaTime;
        }
        else
        {
            currentHealth += healthRegenRate * Time.deltaTime;
        }

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        if (healthBar)
        {
            healthBar.value = currentHealth;
        }

        if (currentHealth <= 0f)
        {
            StartCoroutine(FallAndRespawn());
        }
    }

    IEnumerator FallAndRespawn()
    {
        isFalling = true;

        // Move it downward instantly (can be animated if you want)
        transform.position -= new Vector3(0, fallDistance, 0);

        yield return new WaitForSeconds(respawnDelay);

        // Reset platform position
        transform.position = originalPosition;

        // Restore health
        currentHealth = maxHealth;
        if (healthBar) healthBar.value = currentHealth;

        isFalling = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}
