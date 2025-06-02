using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvaScrip : MonoBehaviour
{
    public TextMeshProUGUI municiontext;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI vidaText;

    public PlayerShoot playerRef;
    // Start is called before the first frame update
    void Start()
    {
        municiontext.text = playerRef.municion.ToString();
        scoreText.text = playerRef.score.ToString();
        vidaText.text = PlayerShoot.vida.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (municiontext.text != playerRef.municion.ToString())
        {
            municiontext.text = playerRef.municion.ToString();
            //animacion
        }
        if (scoreText.text != playerRef.score.ToString())
        {
            scoreText.text = playerRef.score.ToString();
            //animacion
        }
        if (vidaText.text != PlayerShoot.vida.ToString())
        {
            vidaText.text = PlayerShoot.vida.ToString();
            //anmacion
        }
    }
}
