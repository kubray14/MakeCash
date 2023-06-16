using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private float moneyMultiplier = 1f;
    [SerializeField] private TMP_Text multiplierText;

    private void Start()
    {
        DisplayMoneyMultiplier();
    }

    private void SpawnCoin()
    {
        EventManager.OnSpawnCoin.Invoke(transform.parent.position);
    }

    private void DisplayMoneyMultiplier()
    {
        multiplierText.text = ((int)moneyMultiplier).ToString() + "X";
    }

    public float GetMultiplier()
    {
        return moneyMultiplier;
    }
}
