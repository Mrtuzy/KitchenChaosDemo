using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{

    [SerializeField] PlateCounter plateCounter;
    [SerializeField] Transform topPoint;
    [SerializeField] Transform plateVisual;
    List<GameObject> plates;
    
    private void Awake()
    {
        plates = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpawned += PlateCounter_OnPlateSpawned;
        plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
    }

    private void PlateCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plateGamObject = plates[plates.Count-1];
        plates.Remove(plateGamObject);
        Destroy(plateGamObject);
    }

    private void PlateCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisual,topPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0,plateOffsetY * plates.Count,0);
        plates.Add(plateVisualTransform.gameObject);
    }
}
