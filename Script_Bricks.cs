using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Bricks : MonoBehaviour
{
    public int hits = 2;
    private int points = 100;
    public Material hitMaterial;

    Material originalMaterial;
    Renderer renderers;

    void Start()
    {
        renderers = GetComponent<Renderer>();
        originalMaterial = renderers.sharedMaterial;
    }
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hits--;
        if (hits <= 0)
        {
            Script_Game_Manager.Instance.Score += points;
            Destroy(gameObject);
        }

        renderers.sharedMaterial = hitMaterial;
        Invoke("RestoreMaterial", 0.1f);
    }

    void RestoreMaterial()
    {
        renderers.sharedMaterial = originalMaterial;
    }
}
