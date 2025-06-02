using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    private void Start()
    {

        Game_menager.Instance.OnStateChange += GameMenager_OnStateChange;
        Show();

    }

    private void GameMenager_OnStateChange(object sender, System.EventArgs e)
    {
        if (Game_menager.Instance.IsCountdownToStartActive())
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
