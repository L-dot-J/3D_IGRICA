using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainSlowdown : MonoBehaviour
{
    [SerializeField] private float slowDuration = 2.5f;
    [SerializeField] private float slowMultiplier = 0.5f; 

    private bool isPlayerInside = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!gameObject.activeSelf) return;

        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null && !isPlayerInside)
        {
            isPlayerInside = true;
            StartCoroutine(ApplySlowdown(player));
        }
    }

    private IEnumerator ApplySlowdown(PlayerController player)
    {
        player.ApplySpeedModifier(slowMultiplier);

        yield return new WaitForSeconds(slowDuration);

        player.ResetSpeedModifier();
        isPlayerInside = false;
    }
}
