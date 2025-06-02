using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ritmoEnEscena : MonoBehaviour
{
    public TickManager tickManager;
    private int lastTick;
    Vector3 velocity;
    Vector3 inicialEscala;
    // Start is called before the first frame update
    void Start()
    {
        tickManager = FindObjectOfType<TickManager>();

        if (tickManager != null)
        {
            lastTick = tickManager.tickCount;
        }

        inicialEscala = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (tickManager.tickCount > lastTick)
        {
            lastTick = tickManager.tickCount;
            transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.z + 0.1f);
        }
        transform.localScale = Vector3.SmoothDamp(transform.localScale, inicialEscala, ref velocity, 0.2f);
    }
}
