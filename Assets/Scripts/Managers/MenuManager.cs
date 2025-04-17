using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CameraMovement cameraMovement;

    public void OnButtonPressed(string action)
    {
        switch (action)
        {
            case "Play":
                cameraMovement.MoveTo("Play");
                break;
            case "Options":
                cameraMovement.MoveTo("Options");
                break;
            case "Credits":
                cameraMovement.MoveTo("Credits");
                break;
            case "Exit":
                cameraMovement.MoveTo("Exit");
                break;
        }
    }
}

