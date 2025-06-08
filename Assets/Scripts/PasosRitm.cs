using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasosRitm : MonoBehaviour
{
    public GameObject[] pasos;
    public TickManager ticks;
    private int lastTick;
    public Color color1;
    public Color color2;
    Color tempColor;
    void Start()
    {
        if (ticks != null)
        {
            lastTick = ticks.tickCount;
        }
        if (pasos == null || pasos.Length == 0)
        {
            Debug.LogWarning("El array 'pasos' no est� asignado o est� vac�o.", this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ticks.tickCount > lastTick)
        {
            lastTick = ticks.tickCount;
            for (int i = 0; i < pasos.Length - 1; i += 2)
            {
                pasos[i].GetComponent<Renderer>().material.color = color1;
                pasos[i + 1].GetComponent<Renderer>().material.color = color2;
            }
            tempColor = color1;
            color1 = color2;
            color2 = tempColor;
        }
    }
}
