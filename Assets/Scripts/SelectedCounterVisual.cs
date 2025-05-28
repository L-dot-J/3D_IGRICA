using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualSelectedObjectArray;

    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += PlayerController_OnSelectedCounterChanged;
        //UnityEngine.Debug.Log("it got to here");
    }

    private void PlayerController_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        { 
            Hide(); 
        }
    }
    private void Show()
    {
        foreach (GameObject visualSelectedObject in visualSelectedObjectArray)
        {
            visualSelectedObject.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject visualSelectedObject in visualSelectedObjectArray)
        {
            visualSelectedObject.SetActive(false);
        }
    }
}
