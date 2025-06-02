using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUi : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI recipesDeliveredText;

    private void Start()
    {
        Game_menager.Instance.OnStateChange += Game_menager_OnStateChange;
        Hide();
    }

    private void Game_menager_OnStateChange(object sender, System.EventArgs e)
    {
        if (Game_menager.Instance.IsGameOver())
        {
            Show();
            recipesDeliveredText.text = DeliveryManager.Instance.GetSuccesfulRecipesAmount().ToString();
        }
        else
        {
            Hide();
        }
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
