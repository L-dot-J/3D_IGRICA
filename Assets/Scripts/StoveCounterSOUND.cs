using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSOUND : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        bool shouldPlay = stoveCounter.GetState() == StoveCounter.State.Frying || stoveCounter.GetState() == StoveCounter.State.Fried;

        if (shouldPlay && !audioSource.isPlaying)
        {
            //Debug.Log("doslo je do play");
            audioSource.Play();
        }
        else if (!shouldPlay && audioSource.isPlaying)
        {
            //Debug.Log("doslo je do pause");
            audioSource.Pause();
        }
    }
}
