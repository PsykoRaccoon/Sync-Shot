using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public string actionName; 
    private MenuManager manager;
    private Renderer rend;
    private Color originalColor;

    void Start()
    {
        manager = FindObjectOfType<MenuManager>();
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    void OnMouseEnter()
    {
        rend.material.color = Color.yellow; 
    }

    void OnMouseExit()
    {
        rend.material.color = originalColor;
    }

    void OnMouseDown()
    {
        manager.OnButtonPressed(actionName);
    }
}
