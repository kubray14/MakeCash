using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    private void SpawnCoin()
    {
        EventManager.OnSpawnCoin.Invoke(transform.parent.position);
    }
}
