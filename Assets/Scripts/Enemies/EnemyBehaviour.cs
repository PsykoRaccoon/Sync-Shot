using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public float BPM;
    public float ticksSeconds;
    public float timeBetweenAttacks;
    private float lastAttackTime;
    private Color originalColor;
    private Renderer enemyRenderer;
    public bool marked = false;
    private TickManager tickManager;
    private int lastTick;
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
        timeBetweenAttacks = 60f / BPM;
        lastAttackTime = 0f;
        ticksSeconds = 1 / ticksSeconds;

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
            enemyState = EnemyState.Perfect;
            //Invoke(nameof(EarlyInput), ticksSeconds);
            //Invoke(nameof(LateInput), ticksSeconds / 3);
            Invoke(nameof(StateReset), ticksSeconds / 3);
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
        enemyState = EnemyState.Bad;
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
        if (enemyState == EnemyState.Perfect)
        {
            ChangeColor(Color.green);
            return true;
        }
        else
        {
            ChangeColor(Color.red);
            return false;
        }
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
