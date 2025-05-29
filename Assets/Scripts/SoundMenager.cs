using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMenager : MonoBehaviour
{

    [SerializeField] private AudioClipSO audioClipSO;

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSucces += DeliveryManager_OnRecipeSucces;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerController.Instance.OnPickSomething += Player_OnPickSomething;
        BaseCounter.OnAnyObjectPlacedHere += BaseCounter_OnAnyObjectPlacedHere;
        TrashBinCounter.OnAnyObjectTrashed += TrashBinCounter_OnAnyObjectTrashed;
        PlatesCounter.OnAnyPlatePickedUp += PlatesCounter_OnAnyPlatePickedUp;
       
    }

    private void PlatesCounter_OnAnyPlatePickedUp(object sender, System.EventArgs e)
    {
        PlatesCounter plate = sender as PlatesCounter;
        PlaySound(audioClipSO.plateClick, plate.transform.position);
    }

    private void TrashBinCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashBinCounter trashBin = sender as TrashBinCounter;
        PlaySound(audioClipSO.trashCan, trashBin.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipSO.drop, baseCounter.transform.position);
    }

    private void Player_OnPickSomething(object sender, System.EventArgs e)
    {
        PlaySound(audioClipSO.woodenCrate, PlayerController.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipSO.deliveryFail, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeSucces(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipSO.deliveryGood, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 2f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)],position, volume);
        
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 2f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
