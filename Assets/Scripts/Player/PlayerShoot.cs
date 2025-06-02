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

    public int municion = 6;
    bool recargando;
    public float score = 0;
    public static int vida;

    public List<GameObject> markedEnemies = new List<GameObject>();

    void Awake()
    {
        vida = 100;
    }
    // Start is called before the first frame update
    void Start()
    {
        vida = 100;
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
        //gastar bala
        //animacion
        if (municion > 0 && markedEnemies.Count > 0)
        {
            municion--;
            foreach (GameObject enemie in markedEnemies)
            {
                if (enemie.GetComponent<EnemyBehaviour>().canGetAttacked())
                {
                    score += 100;
                    enemie.SetActive(false);
                }
                else
                {
                    vida -= 10;
                }
            }
            markedEnemies.Clear();
        }
        else
        {
            //avisar de no municion
        }
    }
    public void Recargar()
    {
        if (!recargando)
        {
            StartCoroutine(nameof(ProcesoDeRecarga));
        }
    }
    public IEnumerator ProcesoDeRecarga() {
        recargando = true;
        yield return new WaitForSeconds(1);
        municion = 6;
        recargando = false;
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
                if(!markedEnemies.Contains(enBh.gameObject))
                markedEnemies.Add(enBh.gameObject);
            }
        }
        else
        {

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
