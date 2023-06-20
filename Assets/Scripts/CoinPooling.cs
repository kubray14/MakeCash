using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinPooling : MonoBehaviour
{
    [SerializeField] private List<Mesh> coinMeshList;
    [SerializeField] private int coinTypeIndex = 0;
    [SerializeField] private List<GameObject> coinPool = new List<GameObject>();
    public float counter = 0;
    public int maxCoin;
    public int index = 0;
    [SerializeField] private float coinDieTime = 0.025f;
    void Start()
    {
        fullingList();
        EventManager.OnSpawnCoin.AddListener(spawnCoin);
        EventManager.OnCoinTypeUpgrade.AddListener(CoinTypeUpgrade);

    }

    private void spawnCoin(Vector3 coinPos)
    {
        coinPool[index].GetComponent<MeshFilter>().mesh = coinMeshList[coinTypeIndex];
        coinPool[index].transform.DOKill(true);
        coinPool[index].GetComponent<Rigidbody>().velocity = Vector3.zero;
        coinPool[index].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        coinPool[index].transform.rotation = Quaternion.Euler(0, 0, 0);
        coinPool[index].transform.localScale = Vector3.one;
        coinPool[index].transform.position = coinPos - new Vector3(0, 0.25f, 0);
        coinPool[index].SetActive(true);
        coinPool[index].transform.rotation = Quaternion.Euler(Random.Range(0,0.5f),0,0);
        coinPool[index].GetComponent<Rigidbody>().AddForce(new Vector3(0, -0.30f,0), ForceMode.Impulse);
        counter++;
        index++;
        if (index >= coinPool.Count)
        {
            index = 0;
        }
        checkCounter();
    }

    private void checkCounter()
    {
        if (counter == maxCoin)
        {
            counter = 0;
            int i = 0;
            float tempTime = 0f;
            while (i < (maxCoin - 4))
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

    private void CoinTypeUpgrade()
    {
        coinTypeIndex++;
        if (coinTypeIndex > coinMeshList.Count - 1)
        {
            EventManager.OnCoinTypeUpgrade.RemoveListener(CoinTypeUpgrade);
        }
    }
}
