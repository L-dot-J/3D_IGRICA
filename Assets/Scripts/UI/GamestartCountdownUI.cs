using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamestartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start()
    {
        Game_menager.Instance.OnStateChange += Game_menager_OnStateChange;
        Hide();
    }

    private void Game_menager_OnStateChange(object sender, System.EventArgs e)
    {
        if (Game_menager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        countdownText.text = Mathf.Ceil(Game_menager.Instance.GetCountdownToStartTimer()).ToString();
    }
    private void Show()
    { 
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
