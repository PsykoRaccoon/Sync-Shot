using UnityEngine;

public class InputManager : MonoBehaviour
{
    public TickManager tickManager; 
    private KeyCode inputKey = KeyCode.Mouse0; 
    public GameObject visualObject; 
    public Color successColor = Color.green;
    private Color originalColor;

    private bool canRegisterInput = false;
    private float tickInterval;
    private float tickTime;
    public float inputWindow; 

    void Start()
    {
        if (tickManager != null)
        {
            tickManager.OnTick += OnTickEvent; 
            tickInterval = 1f / tickManager.ticksPerSecond;
        }

        if (visualObject != null)
        {
            originalColor = visualObject.GetComponent<Renderer>().material.color;
        }
    }

    void Update()
    {
        if (canRegisterInput && Input.GetKeyDown(inputKey))
        {
            Debug.Log("Input registrado en el tick!");
            canRegisterInput = false;

            if (visualObject != null)
            {
                visualObject.GetComponent<Renderer>().material.color = successColor;
                Invoke("ResetColor", 0.1f);
            }
        }
    }

    void OnTickEvent(int tickCount)
    {
        tickTime = Time.time;
        canRegisterInput = true;

        Invoke("CloseInputWindow", inputWindow * 2);
    }

    void CloseInputWindow()
    {
        canRegisterInput = false;
    }

    void ResetColor()
    {
        if (visualObject != null)
        {
            visualObject.GetComponent<Renderer>().material.color = originalColor;
        }
    }
}
