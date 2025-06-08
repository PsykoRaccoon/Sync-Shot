//using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    public bool isLookingRight = true;
    public GameObject canvaMuerte;
    public AudioSource music, effects;
    public AudioClip[] keyClips; // D, F, J, K
    public Image[] keysImages;// D, F, J, K
    public TextMeshProUGUI ammoText;
    private Coroutine ammoWarningCoroutine;
    private Color originalAmmoColor;
    public Color[] keyFlashColors; // D, F, J, K
    private bool[] isAnimating; // uno por tecla
    public Transform rig;
    private bool compensarRotacion = false;
    Vector3 start;
    Vector3 direction;
    public float distance = 10f;
    //public AnimatorController animController;
    public Animator anim;
    public Transform shootPoint;
    public int municion = 6;
    bool recargando;
    public float score = 0;
    public static int vida;
    private Quaternion rigOriginalRotation;
    public List<GameObject> markedEnemies = new List<GameObject>();
    private Coroutine rotacionCoroutine;
    void Awake()
    {
        vida = 100;
        Time.timeScale = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        vida = 100;
        start = transform.position;
        direction = transform.right;
        GetComponent<Animator>().applyRootMotion = false;
        isAnimating = new bool[keysImages.Length];
        rigOriginalRotation = rig.localRotation;

    }

    // Update is called once per frame
    void Update()
    {
        start = transform.position;
        direction = transform.right;
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isLookingRight)
            {
                Recargar();
            }
            else
            {
                Disparar();
            }
            // J
            PlayKeyFeedback(2);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (!isLookingRight)
            {
                Recargar();
            }
            else
            {
                Disparar();
            }
             // K
            PlayKeyFeedback(3);

        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            ApuntarLeft();
             // D
            PlayKeyFeedback(0);
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            ApuntadoRight();
          // F
            PlayKeyFeedback(1);
        }
        
        if (vida <= 0)
        {
            canvaMuerte.SetActive(true);
            transform.localEulerAngles = new Vector3(0, 0, 90);
            music.Stop();
            Time.timeScale = 0;
        }
    }
    void LateUpdate()
    {
        if (compensarRotacion)
        {
            // Este valor depende de cuánto se está girando mal el rig en la animación
            // Ajústalo manualmente hasta que quede visualmente correcto
            rig.localRotation = Quaternion.Euler(-90, -45f, 0);
        }
    }
    public void Disparar()
    {
        //gastar bala
        //animacion
        if (recargando) return;
        if (anim != null) anim.SetTrigger("Disparar");
        if (rotacionCoroutine != null)
        {
            StopCoroutine(rotacionCoroutine);
            rig.localRotation = rigOriginalRotation;
            compensarRotacion = false;
        }
        rotacionCoroutine = StartCoroutine(CompensarRotacionTemporal());
        if (municion > 0 && markedEnemies.Count > 0)
        {
            municion--;

            if (municion <= 0 && ammoWarningCoroutine == null)
            {
                ammoWarningCoroutine = StartCoroutine(ParpadeoSinMunicion());
            }
            foreach (GameObject enemie in markedEnemies)
            {
                EnemyBehaviour eb = enemie.GetComponent<EnemyBehaviour>();
                if (eb != null && !eb.isDead && eb.canGetAttacked())
                {
                    score += 100;
                    eb.Die();
                }
                else
                {
                    vida -= 2;
                }
            }
            markedEnemies.Clear();
        }
        else if (municion <= 0)
        {
            //avisar de no municion
            if (ammoWarningCoroutine == null && ammoText != null)
            {
                ammoWarningCoroutine = StartCoroutine(ParpadeoSinMunicion());
            }
        }
    }
    public void Recargar()
    {
        if (!recargando)
        {
            StartCoroutine(nameof(ProcesoDeRecarga));
        }
        //detener parpadeo de la recarga
        if (ammoWarningCoroutine != null)
        {
            StopCoroutine(ammoWarningCoroutine);
            ammoWarningCoroutine = null;
            if (ammoText != null) ammoText.color = originalAmmoColor;
        }

       
    }
    public IEnumerator ProcesoDeRecarga() {
        recargando = true;
        yield return new WaitForSeconds(1);
        municion = 6;
        recargando = false;
    }
    public void ApuntadoRight()
    {
        if (!isLookingRight)
        {
            gameObject.transform.rotation =  Quaternion.Euler(0, 90, 0);
            isLookingRight = true;
        }
        else
        {
            raycastoShoot();
            OnDrawGizmos();
        }
    }
    public void ApuntarLeft()
    {
        if (isLookingRight)
        {
            gameObject.transform.rotation =  Quaternion.Euler(0, -90, 0);
            isLookingRight = false;
        }
        else
        {
            raycastoShoot();
            OnDrawGizmos();
        }
    }

    void raycastoShoot() {

        //Vector3 start = transform.position + Vector3.up * 1f; // Ajusta altura si es necesario
        //Vector3 direction = isLookingRight ? transform.right : -transform.right;
        Vector3 start = shootPoint.position;
        Vector3 direction = shootPoint.forward;
        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit, distance))
        {
            if (hit.collider.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enBh))
            {
                if (enBh.isDead) return;
                enBh.marked = true;
                if(!markedEnemies.Contains(enBh.gameObject))
                    markedEnemies.Add(enBh.gameObject);
            }
        }
        else
        {

        }
    }

    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;

        Vector3 start = transform.position + Vector3.up * 1f;
        Vector3 direction = isLookingRight ? transform.right : -transform.right;

        RaycastHit hit;
        if (Physics.Raycast(start, direction, out hit, distance))
        {
            Debug.DrawLine(start, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(start, start + direction * distance, Color.green);
        }
    }
    //PARA EL FEEDBACK VISUAL y de sonido
    #region FEEDBACK VISUAL y SONORO
    IEnumerator CompensarRotacionTemporal()
    {
        compensarRotacion = true;
        yield return new WaitForSeconds(0.8f); // Duración exacta de la animación donde se ve mal
        rig.localRotation = rigOriginalRotation;
        compensarRotacion = false;
    }
    IEnumerator PlayKeyFeedback(Image img, Color flashColor, int index)
    {
        isAnimating[index] = true;
        Color originalColor = img.color;
        Vector3 originalScale = img.rectTransform.localScale;
        Vector3 originalPosition = img.rectTransform.localPosition;

        img.gameObject.SetActive(true);
        img.transform.localScale = Vector3.one;
        img.color = flashColor;

        float duration = 0.12f;
        float time = 0f;
        Vector3 targetScale = Vector3.one * 1.5f;


        while (time < duration)
        {
            time += Time.deltaTime;
            float progress = time / duration;

            img.rectTransform.localScale = Vector3.Lerp(Vector3.one, targetScale, progress);
            //float scale = Mathf.Lerp(0f, 1.2f, time / duration);
            //img.transform.localScale = new Vector3(scale, scale, scale);
            float shakeStrength = 5f; // puedes ajustar esto
            Vector3 shakeOffset = new Vector3(
                Random.Range(-shakeStrength, shakeStrength),
                Random.Range(-shakeStrength, shakeStrength),
                0f
            );
            img.rectTransform.localPosition = originalPosition + shakeOffset;

            yield return null;
        }

        // Esperar un poquito
        yield return new WaitForSeconds(0.1f);

        // Desvanecer y escalar hacia 0
        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = Mathf.Lerp(1.2f, 0f, time / duration);
            img.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }
        img.color = originalColor;
        img.rectTransform.localScale = originalScale;

        img.rectTransform.localPosition = originalPosition;

        img.gameObject.SetActive(false);

        isAnimating[index] = false;
    }
    void PlayKeyFeedback(int index)
    {
        if (index >= 0 && index < keysImages.Length && !isAnimating[index])
        {
            Color flashColor = (keyFlashColors.Length > index) ? keyFlashColors[index] : Color.white;
            StartCoroutine(PlayKeyFeedback(keysImages[index], flashColor, index));
            if (keyClips.Length > index && keyClips[index] != null)
            {
                effects.pitch = Random.Range(0.95f, 1.05f);
                StartCoroutine(ResetPitch());
                effects.PlayOneShot(keyClips[index]);
            }
        }
    }
    IEnumerator ResetPitch()
    {
        yield return new WaitForSeconds(0.2f); // espera a que termine el sonido
        effects.pitch = 1f;
    }
    IEnumerator ParpadeoSinMunicion()
    {
        if (ammoText == null) yield break;

        originalAmmoColor = ammoText.color;
        Color redColor = Color.red;

        while (true)
        {
            ammoText.color = redColor;
            yield return new WaitForSeconds(0.3f);
            ammoText.color = originalAmmoColor;
            yield return new WaitForSeconds(0.3f);
        }
    }
    #endregion
}
