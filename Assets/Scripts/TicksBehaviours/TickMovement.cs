using UnityEngine;

public class TickMovement : MonoBehaviour
{
    private TickManager tickManager; 
    private Transform targetPosition; 
    private int lastTick;
    private bool isMoving;

    void Start()
    {
        tickManager = FindObjectOfType<TickManager>();

        GameObject targetObject = GameObject.Find("Tempo 0");
        if (targetObject != null)
        {
            targetPosition = targetObject.transform;
        }
        if (tickManager != null)
        {
            lastTick = tickManager.tickCount;
        }
    }

    void Update()
    {
        if (tickManager.tickCount > lastTick && !isMoving)
        {
            lastTick = tickManager.tickCount;
            isMoving = true;
            MoveTowardsTarget();
        }

        if (isMoving && tickManager.tickCount > lastTick)
        {
            lastTick = tickManager.tickCount;
            MoveTowardsTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        transform.position += direction;

        if (Vector3.Distance(transform.position, targetPosition.position) < 0.1f)
        {
            transform.position = targetPosition.position;
            //Destroy(gameObject);
            PlayerShoot.vida -= 10;
            gameObject.SetActive(false);
        }
    }
}
