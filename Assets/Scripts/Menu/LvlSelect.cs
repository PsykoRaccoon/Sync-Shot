using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class LvlSelect : MonoBehaviour
{
    public AudioSource audioSource; 
    public AudioClip sonidoCarga;   
    public float delayAntesDeCargar;
    public Image pantallaNegra;
    public float duracionFade;
    public GameObject musicManager;
    public GameObject canvasBack;


    public void CargarEscena(string nombreEscena)
    {
        StartCoroutine(TransicionEscena(nombreEscena));
    }

    private IEnumerator TransicionEscena(string nombreEscena)
    {
        if (musicManager != null)
        {
            musicManager.SetActive(false);
            canvasBack.SetActive(false);
        }

        if (audioSource != null && sonidoCarga != null)
        {
            audioSource.PlayOneShot(sonidoCarga);
        }

        if (pantallaNegra != null)
        {
            Color color = pantallaNegra.color;
            for (float t = 0; t < duracionFade; t += Time.deltaTime)
            {
                float alpha = Mathf.Lerp(0f, 1f, t / duracionFade);
                pantallaNegra.color = new Color(color.r, color.g, color.b, alpha);
                yield return null;
            }
            pantallaNegra.color = new Color(color.r, color.g, color.b, 1f);
        }

        yield return new WaitForSeconds(delayAntesDeCargar);

        SceneManager.LoadScene(nombreEscena);
    }
}
