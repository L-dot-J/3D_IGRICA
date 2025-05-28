using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private GameObject cfxrSmoke;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }

    private void CuttingCounter_OnCut(object sender, System.EventArgs e)
    {
        GameObject effect = Instantiate(cfxrSmoke, spawnPoint.position, Quaternion.identity);

        ParticleSystem particlesystem = effect.GetComponentInChildren<ParticleSystem>();
        if (particlesystem != null)
        {
            particlesystem.Play(); 
        }

        Destroy(effect, 2f); 
    }

}
