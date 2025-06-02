using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStaticdataManager : MonoBehaviour
{
    private void Awake()
    {
        CuttingCounter.ResetStaticData();
        BaseCounter.ResetStaticData();
        TrashBinCounter.ResetStaticData();
    }
}
