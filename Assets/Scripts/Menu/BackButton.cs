using UnityEngine;

public class BackButton : MonoBehaviour
{
    public MenuManager menuManager;    
    public GameObject canvasMenu;
    public string actionName;

    public void OnBackButtonPressed()
    {
        menuManager.OnButtonPressed(actionName); 
        canvasMenu.SetActive(false);             
    }
}
