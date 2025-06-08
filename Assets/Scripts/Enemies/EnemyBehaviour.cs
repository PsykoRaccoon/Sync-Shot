using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private float timeBetweenAttacks;
    private float lastAttackTime;
    private Color originalColor;
    private Renderer enemyRenderer;
    public bool marked = false;
    private TickManager tickManager;
    private int lastTick;
    public GameObject crosshair;
    public Material highlightMaterial;
    private Material originalMaterial;
    public GameObject[] deathParticlesPrefabs; // asignar en el editor
    public bool isDead = false;
    public enum EnemyState
    {
        Early,
        Perfect,
        Late,
        Bad
    }
    public EnemyState enemyState;

    private void Start()
    {
        tickManager = FindObjectOfType<TickManager>();
        
        enemyRenderer = GetComponent<Renderer>();
        originalMaterial = enemyRenderer.material;
        if (enemyRenderer != null)
        {
            originalMaterial = enemyRenderer.material;
            originalColor = enemyRenderer.material.color;
        }
        lastAttackTime = 0f;

        if (enemyRenderer != null)
        {
            originalColor = enemyRenderer.material.color;
        }
        //StartCoroutine(selfCountTicks());
        if (tickManager != null)
        {
            lastTick = tickManager.tickCount;
        }
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            inputDetection();
        }*/
        if (tickManager.tickCount > lastTick)
        {
            lastTick = tickManager.tickCount;
            enemyState = EnemyState.Bad;
            //Invoke(nameof(EarlyInput), ticksSeconds);
            //Invoke(nameof(LateInput), ticksSeconds / 3);
            Invoke(nameof(StateReset), tickManager.tickInterval / tickManager.ElCuarentaPorcientoDelTicksPerSecond);//tickManager.ticksToCalibrate);//2.5f);
        }
        if (marked && !isDead)
        {
            crosshair.SetActive(true);
            if (enemyRenderer != null && enemyRenderer.material != highlightMaterial)
            {
                enemyRenderer.material = highlightMaterial;
            }

        }
        else
        {
            crosshair.SetActive(false);
            if (enemyRenderer != null && enemyRenderer.material != originalMaterial)
            {
                enemyRenderer.material = originalMaterial;
            }
        }
    }

   /* public IEnumerator selfCountTicks()
    {
        while (true)
        {
            enemyState = EnemyState.Perfect;
            yield return new WaitForSeconds(ticksSeconds);
            Invoke(nameof(StateReset), ticksSeconds/2);
        }
        
    }*/
    public void LateInput()
    {
        enemyState = EnemyState.Late;
    }
    public void EarlyInput()
    {
        enemyState = EnemyState.Early;
    }
    public void StateReset()
    {
        enemyState = EnemyState.Perfect;
    }
    /*
    public void inputDetection()
    {
        switch (enemyState)
        {
            case EnemyState.Early:
                Debug.Log("inputEarly");
                break;
            case EnemyState.Perfect:
                Debug.Log("Perfect");
                break;
            case EnemyState.Late:
                Debug.Log("Late");
                break;
            case EnemyState.Bad:
                Debug.Log("InputBad");
                break;
        }
    }*/
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

    public bool canGetAttacked()
    {
        /*
        float currentTime = Time.time;

        float timeSinceLastAttack = currentTime - lastAttackTime;

        if (timeSinceLastAttack >= timeBetweenAttacks * 0.9f && timeSinceLastAttack <= timeBetweenAttacks * 1.1f)
        {
            //ChangeColor(Color.green);
            return true;
        }
        else
        {
            ChangeColor(Color.red);
            return false;
        }

        lastAttackTime = currentTime;*/
        if (enemyState != EnemyState.Bad)
        {
            ChangeColor(Color.green);
            return true;
        }
        else
        {
            marked = false;
            ChangeColor(Color.red);
            return false;
        }

    }

    void ChangeColor(Color newColor)
    {
        if (enemyRenderer != null)
        {
            //enemyRenderer.GetComponent<Renderer>().material.color = newColor;
            //Invoke("ResetColor", 0.2f);
            Material instanceMaterial = new Material(enemyRenderer.material);
            instanceMaterial.color = newColor;
            enemyRenderer.material = instanceMaterial;

            CancelInvoke(nameof(ResetColor));
            Invoke(nameof(ResetColor), 0.2f);
        }
    }

    private void ResetColor()
    {
        if (enemyRenderer != null)
        {
            // enemyRenderer.GetComponent<Renderer>().material.color = originalColor;
            // enemyRenderer.material = marked ? highlightMaterial : originalMaterial;
            if (marked && highlightMaterial != null)
            {
                //    enemyRenderer.material = highlightMaterial;
                //    enemyRenderer.material.color = Color.white;
                Material highlightInstance = new Material(highlightMaterial);
                highlightInstance.color = Color.white;
                enemyRenderer.material = highlightInstance;
            }
            else
            {
                Material originalInstance = new Material(originalMaterial);
                originalInstance.color = originalColor;
                enemyRenderer.material = originalInstance;
                //    enemyRenderer.material = originalMaterial;
                //    enemyRenderer.material.color = originalColor;
            }
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        marked = false;
        if (crosshair != null)
            crosshair.SetActive(false);

        // Instanciar el efecto de partículas
        if (deathParticlesPrefabs != null && deathParticlesPrefabs.Length > 0)
        {
            int randomIndex = Random.Range(0, deathParticlesPrefabs.Length);
            GameObject selectedPrefab = deathParticlesPrefabs[randomIndex];
            GameObject effect = Instantiate(selectedPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f); // limpiar efecto
        }

        // Desactivar o destruir el enemigo
        gameObject.SetActive(false); // o bien:
        
    }
}
