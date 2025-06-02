using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDisableExplode : MonoBehaviour
{
    void OnDisable()
    {
        Invoke(nameof(EsperaPaMorir),6);
    }
    void EsperaPaMorir()
    {
        
        Destroy(gameObject);
    }
}
