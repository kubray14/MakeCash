using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    public float force = 1f;

    private void SpawnCoin()
    {
        EventManager.OnSpawnCoin.Invoke(transform.position - new Vector3(0, 0.1f, 0));
    }
}
