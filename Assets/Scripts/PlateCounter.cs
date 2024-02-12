using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float plateTimer;
    private float plateMaxSpawnTime = 4f;
    private int plateCount;
    private int plateMaxCount = 4;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;
    private void Update()
    {
        plateTimer += Time.deltaTime;
        if (plateTimer > plateMaxSpawnTime)
        {
            plateTimer = 0;
            if (GameManager.Instance.IsGamePlaying() && plateCount < plateMaxCount)
            {
                plateCount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
            }


        }
    }
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            if (plateCount > 0)
            {
                plateCount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
