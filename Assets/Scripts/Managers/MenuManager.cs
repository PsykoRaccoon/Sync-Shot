using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public MusicManager musicManager; 

    public void OnButtonPressed(string action)
    {
        switch (action)
        {
            case "Play":
                cameraMovement.MoveTo("Play");
                musicManager.SetVolumesForMenu("Play");
                break;
            case "Options":
                cameraMovement.MoveTo("Options");
                musicManager.SetVolumesForMenu("Options");
                break;
            case "Credits":
                cameraMovement.MoveTo("Credits");
                musicManager.SetVolumesForMenu("Credits"); 
                break;
            case "Origin":
                cameraMovement.MoveTo("Origin");
                musicManager.SetVolumesForMenu("Main"); 
                break;
            case "Exit":
                cameraMovement.MoveTo("Exit");
                break;
        }
    }
}
