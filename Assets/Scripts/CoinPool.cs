using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    [SerializeField] private int maxCoinCount = 25;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<GameObject> coinPool = new List<GameObject>();
    [SerializeField] private int coinIndex = 0;

    private void Start()
    {
        CreateCoins();
        EventManager.OnSpawnCoin.AddListener(SpawnCoin);
    }

    private void CreateCoins()
    {
        for (int i = 0; i < maxCoinCount; i++)
        {
            GameObject createdCoin = Instantiate(coinPrefab, Vector3.zero, coinPrefab.transform.rotation);
            coinPool.Add(createdCoin);
            createdCoin.SetActive(false);
        }
    }

    private void SpawnCoin(Vector3 spawnPos)
    {
        coinPool[coinIndex].gameObject.SetActive(true);
        coinPool[coinIndex].gameObject.transform.position = spawnPos;
        coinPool[coinIndex].gameObject.transform.localScale = coinPrefab.transform.localScale;
        coinPool[coinIndex].gameObject.GetComponent<Rigidbody>().AddForce(-(Vector3.up * 3 + Vector3.forward).normalized * .5f, ForceMode.Impulse);
        coinPool[coinIndex].gameObject.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere.normalized * 0.01f, ForceMode.Impulse);
        coinIndex++;
    }
}
