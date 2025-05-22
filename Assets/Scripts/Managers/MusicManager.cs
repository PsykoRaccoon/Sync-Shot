using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] tracks; //bajo drums elecdrums guitar

    [Header("Volúmenes por menú")]
    public float[] mainMenuVolumes = { 1f, 1f, 1f, 1f };

    public float[] optionsMenuVolumes = { 0.5f, 0.5f, 1f, 0.5f };

    public float[] playMenuVolumes = { 0.8f, 1f, 1f, 0.7f };

    public float[] creditsMenuVolumes = { 0.4f, 0.4f, 0.4f, 0.4f }; //pendiente

    public void SetVolumesForMenu(string menuName)
    {
        float[] volumes = null;

        switch (menuName)
        {
            case "Main":
                volumes = mainMenuVolumes;
                break;
            case "Options":
                volumes = optionsMenuVolumes;
                break;
            case "Play":
                volumes = playMenuVolumes;
                break;
            case "Credits":
                volumes = creditsMenuVolumes;
                break;
        }

        if (volumes != null)
            SetVolumes(volumes);
    }

    private void SetVolumes(float[] volumes)
    {
        for (int i = 0; i < tracks.Length && i < volumes.Length; i++)
        {
            tracks[i].volume = volumes[i];
        }
    }
}
