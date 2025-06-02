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
    }

    // Update is called once per frame
    void Update()
    {
        if (ticks.tickCount > lastTick)
        {
            lastTick = ticks.tickCount;
            for (int i = 0; i < pasos.Length; i += 2)
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
