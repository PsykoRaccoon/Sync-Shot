using System;
using System.Collections;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public float ticksPerSecond;
    public float tickInterval;
    public float ElCuarentaPorcientoDelTicksPerSecond;
    public int tickCount;

    public event Action<int> OnTick;

    void Start()
    {
        UpdateTickInterval();
        StartCoroutine(TickLoop());
    }

    void UpdateTickInterval()
    {
        tickInterval = 1f / ticksPerSecond;
    }

    IEnumerator TickLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickInterval);
            tickCount++;
            //print($"Tick {tickCount} - Intervalo: {tickInterval} segundos");

            OnTick?.Invoke(tickCount); 
        }
    }

    public void SetTicksPerSecond(float newTicksPerSecond)
    {
        ticksPerSecond = newTicksPerSecond;
        UpdateTickInterval();
    }
}
