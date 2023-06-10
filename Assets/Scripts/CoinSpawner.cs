using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject coinPrefab;
    public float force = 1f;

    private void SpawnCoin()
    {
        GameObject coinClone = Instantiate(coinPrefab, transform.position - new Vector3(0, 0.1f, 0), coinPrefab.transform.rotation);
        coinClone.GetComponent<Rigidbody>().AddForce(-(Vector3.up * 3 + Vector3.forward).normalized * force * .5f, ForceMode.Impulse);
        coinClone.GetComponent<Rigidbody>().AddTorque(Random.insideUnitSphere.normalized * force * 0.01f, ForceMode.Impulse);
    }
}
