using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPooling : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<GameObject> coinPool = new List<GameObject>();
    public float counter = 0;

    void Start()
    {
        EventManager.OnSpawnCoin.AddListener(spawnCoin);
    }

    private void spawnCoin(Vector3 coinPos)
    { 
        GameObject coin = Instantiate(coinPrefab, coinPos - new Vector3(0, 0.25f, 0), Quaternion.identity);
        coin.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.15f, 0.15f), -0.25f, -0.1f), ForceMode.Impulse);
        coinPool.Add(coin);
        counter++;
        EventManager.OnGainMoney.Invoke();

    }

    private void checkCounter()
    {
        if (counter == 25)
        {
            //...
        }
    }
}
