using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private Animator animator;
    private const string SHAKE = "Shake";

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedobject += ContainerCounter_OnPlayerGrabbedobject;
    }

    private void ContainerCounter_OnPlayerGrabbedobject(object sender, System.EventArgs e)
    {
        animator.SetTrigger(SHAKE);
    }
}
