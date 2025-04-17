using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float BPM;
    private float timeBetweenAttacks;
    private float lastAttackTime;
    private Color originalColor;
    private Renderer enemyRenderer;

    private void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        timeBetweenAttacks = 60f / BPM;
        lastAttackTime = 0f;

        if(enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            HandleAttack();
        }
    }

    private void HandleAttack()
    {
        float currentTime = Time.time;

        float timeSinceLastAttack = currentTime - lastAttackTime;

        if (timeSinceLastAttack >= timeBetweenAttacks * 0.9f && timeSinceLastAttack <= timeBetweenAttacks * 1.1f)
        {
            ChangeColor(Color.green);
        }
        else
        {
            ChangeColor(Color.red);
        }

        lastAttackTime = currentTime;
    }

    void ChangeColor(Color newColor)
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.GetComponent<Renderer>().material.color = newColor;
            Invoke("ResetColor", 0.2f);
        }
    }

    private void ResetColor()
    {
        if (enemyRenderer != null)
        {
            enemyRenderer.GetComponent<Renderer>().material.color = originalColor;
        }
    }
}
