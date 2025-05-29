using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]

public class AudioClipSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] woodenCrate;
    public AudioClip[] drop;
    public AudioClip[] deliveryFail;
    public AudioClip[] deliveryGood;
    public AudioClip[] plateClick;
    public AudioClip[] frySizzle;
    public AudioClip[] trashCan;
}
