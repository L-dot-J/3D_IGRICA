using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Update()
    {
      timerImage.fillAmount = Game_menager.Instance.GetPlayTimerNormalized();
    }
}
