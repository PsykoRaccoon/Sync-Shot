using System.Collections;
using UnityEngine;

public class TickManager : MonoBehaviour
{
    public float ticksPerSecond;
    private float tickInterval;
    public int tickCount;

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
            Debug.Log($"Tick {tickCount} - Intervalo: {tickInterval} segundos");
        }
    }

    public void SetTicksPerSecond(float newTicksPerSecond)
    {
        ticksPerSecond = newTicksPerSecond;
        UpdateTickInterval();
    }
}
