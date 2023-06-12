using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinPooling : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private List<GameObject> coinPool = new List<GameObject>();
    public float counter = 0;
    public int maxCoin;
    public int index = 0;
    [SerializeField]  private float coinDieTime = 0.025f;
    void Start()
    {
        fullingList();
        EventManager.OnSpawnCoin.AddListener(spawnCoin);
        
    }

    private void spawnCoin(Vector3 coinPos)
    {
        coinPool[index].transform.DOKill(true);
        coinPool[index].GetComponent<Rigidbody>().velocity = Vector3.zero;
        coinPool[index].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        coinPool[index].transform.rotation = Quaternion.Euler(0, 0, 0);
        coinPool[index].transform.localScale = new Vector3(0.15f, 0.15f, 0.031f);
        coinPool[index].transform.position = coinPos - new Vector3(0, 0.25f, 0);
        coinPool[index].SetActive(true);
        coinPool[index].GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-0.15f, 0.15f), -0.25f, -0.1f), ForceMode.Impulse);
        counter++;
        index++;
        if (index >= coinPool.Count)
        {
            index = 0;
        }
        checkCounter();
        EventManager.OnGainMoney.Invoke();
    }

    private void checkCounter()
    {
        if (counter == maxCoin)
        {
            counter = 0;
            int i = 0;
            float tempTime = 0f;
            while(i < (maxCoin-4))
            { 
                coinPool[i].transform.DOScale(Vector3.zero, tempTime);
                i++;
                tempTime += coinDieTime;
            }
        }
    }

    private void fullingList()
    {
        int temp = 0;
        while (temp < transform.childCount)
        {
            coinPool.Add(transform.GetChild(temp).gameObject);
            temp++;
        }
    }
}
