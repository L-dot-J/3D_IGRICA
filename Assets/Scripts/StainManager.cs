using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainManager : MonoBehaviour
{
    [SerializeField] private float toggleInterval = 10f; 
    [SerializeField] private int maxActiveStains = 3; 
    [SerializeField] private List<GameObject> stainSpots = new List<GameObject>();

    private List<GameObject> activeStains = new List<GameObject>();

    void Start()
    {
        foreach (Transform child in transform)
        {
            stainSpots.Add(child.gameObject);
            child.gameObject.SetActive(false);
        }

        InvokeRepeating("ToggleRandomStains", 0f, toggleInterval);
    }

    void ToggleRandomStains()
    {
        
        foreach (var stain in activeStains)
        {
            if (stain != null) stain.SetActive(false);
        }
        activeStains.Clear();

        
        for (int i = 0; i < maxActiveStains; i++)
        {
            if (stainSpots.Count == 0) break;

            int randomIndex = Random.Range(0, stainSpots.Count);
            GameObject stain = stainSpots[randomIndex];
            stain.SetActive(true);
            activeStains.Add(stain);
        }
    }
}
