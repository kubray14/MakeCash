using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinPooling : MonoBehaviour
{
    [SerializeField] private List<Mesh> coinMeshList;
    [SerializeField] private int coinTypeIndex = 0;
    [SerializeField] private List<Rigidbody> coinPool = new List<Rigidbody>();
    [SerializeField] private List<MeshCollider> coinPoolMeshColliders = new List<MeshCollider>();
    public float counter = 0;
    public int maxCoin;
    public int index = 0;
    [SerializeField] private float coinDieTime = 0.025f;
    [SerializeField] private Vector3 coinStartScale;
    [SerializeField] private Vector3 gravityValue;
    [SerializeField] private Vector3 coinStartForce;
    [SerializeField] private float coinSpawnRotationY;
    void Start()
    {
        fullingList();
        EventManager.OnSpawnCoin.AddListener(spawnCoin);
        EventManager.OnCoinTypeUpgrade.AddListener(CoinTypeUpgrade);
        Physics.gravity = gravityValue;

    }

    private void spawnCoin(Vector3 coinPos)
    {
        coinPool[index].GetComponent<MeshFilter>().mesh = coinMeshList[coinTypeIndex];
       // coinPoolMeshColliders[index].sharedMesh = coinMeshList[coinTypeIndex];
        coinPool[index].transform.DOKill(true);
        coinPool[index].velocity = Vector3.zero;
        coinPool[index].angularVelocity = Vector3.zero;
        coinPool[index].transform.rotation = Quaternion.Euler(0, 0, 0);
        coinPool[index].transform.localScale = coinStartScale;
        coinPool[index].transform.position = coinPos - new Vector3(0, 0.25f, 0);
        coinPool[index].gameObject.SetActive(true);
        coinPool[index].transform.rotation = Quaternion.Euler(0, Random.Range(-coinSpawnRotationY, coinSpawnRotationY), 0);
        coinPool[index].AddForce(coinStartForce, ForceMode.Impulse);
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
            StartCoroutine(CoinDie_Coroutine());
        }
    }

    private IEnumerator CoinDie_Coroutine()
    {
        yield return null;
        counter = 0;

        int i = 4;
        while (i < (maxCoin - 4))
        {
            //yield return new WaitForSeconds(0.1f);

            int k = Random.Range(1, 4);
            for (int j = 0; j < k; j++)
            {
                if (i + k < maxCoin - 4)
                {
                    coinPool[i + j].transform.DOScale(Vector3.zero, 0.5f + (j * 0.1f));
                }
            }
            i += k;
        }
    }

    private void fullingList()
    {
        int temp = 0;
        while (temp < transform.childCount)
        {
            coinPool.Add(transform.GetChild(temp).gameObject.GetComponent<Rigidbody>());
            coinPoolMeshColliders.Add(transform.GetChild(temp).gameObject.GetComponent<MeshCollider>());
            temp++;
        }
    }

    private void CoinTypeUpgrade()
    {
        coinTypeIndex++;
        if (coinTypeIndex >= coinMeshList.Count - 1)
        {
            EventManager.OnCoinTypeUpgrade.RemoveListener(CoinTypeUpgrade);
        }
    }
}
