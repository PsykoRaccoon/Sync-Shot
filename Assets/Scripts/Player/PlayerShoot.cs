using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public bool isLookingRight = true;

    Vector3 start;
    Vector3 direction;
    public float distance = 10f;

    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        direction = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        start = transform.position;
        direction = transform.right;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isLookingRight)
            {
                Recargar();
            }
            else
            {
                Disparar();
            }
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isLookingRight)
            {
                Recargar();
            }
            else
            {
                Disparar();
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ApuntarLeft();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ApuntadoRight();
        }
    }

    public void Disparar()
    {

    }
    public void Recargar()
    {

    }
    public void ApuntadoRight()
    {
        if (!isLookingRight)
        {
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            isLookingRight = true;
        }
        else
        {
            raycastoShoot();
            OnDrawGizmos();
        }
    }
    public void ApuntarLeft()
    {
        if (isLookingRight)
        {
            gameObject.transform.rotation = new Quaternion(0, 180, 0, 0);
            isLookingRight = false;
        }
        else
        {
            raycastoShoot();
            OnDrawGizmos();
        }
    }

    void raycastoShoot() {
        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit, distance))
        {
            if (hit.collider.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enBh))
            {
                enBh.marked = true;
            }
        }
        else
        {
            if (hit.collider.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enBh))
            {
                enBh.marked = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit, distance))
        {
            Debug.DrawLine(start, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(start, start + direction * distance, Color.green);
        }
    }
}
