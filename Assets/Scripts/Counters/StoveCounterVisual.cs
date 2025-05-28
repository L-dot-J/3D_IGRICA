using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject cfxrFry;
    [SerializeField] private Transform spawnPoint;

    private GameObject currentEffect;


    private void Update()
    {
        if (!stoveCounter.HasKitchenObject() && currentEffect != null)
        {
            Destroy(currentEffect);
            currentEffect = null;
        }
    }

    private void Start()
    {
        stoveCounter.OnFry += StoveCounter_OnFry;
    }

    private void StoveCounter_OnFry(object sender, System.EventArgs e)
    {
        if (currentEffect != null)
        {
            Destroy(currentEffect); 
        }

        currentEffect = Instantiate(cfxrFry, spawnPoint.position, Quaternion.identity);

        ParticleSystem particleSystem = currentEffect.GetComponentInChildren<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
        }

    }

   
}
