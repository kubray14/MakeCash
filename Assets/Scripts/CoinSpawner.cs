using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Mesh upgradeMesh;
    [SerializeField] private GameObject pipeCoin;
    [SerializeField] private float moneyMultiplier = 1f;
    [SerializeField] private TMP_Text multiplierText;
    [SerializeField] private TMP_Text gainedMoneyText;
    private float tweenTime = 1f;
    private PlayerController playerController;

    private void Start()
    {
        DisplayMoneyMultiplier();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void SpawnCoin()
    {
        EventManager.OnSpawnCoin.Invoke(transform.parent.position);
        EventManager.OnGainMoney.Invoke(moneyMultiplier * PlayerController.moneyIncrease);
        EventManager.OnGainMoneyUI.Invoke();
        gainedMoneyText.gameObject.SetActive(true);
        float moneyText = moneyMultiplier * PlayerController.moneyIncrease;
        gainedMoneyText.text = moneyText.ToString("0.0") + "$";
        tweenTime = 1 / playerController.GetAnimSpeed();
        gainedMoneyText.transform.DOMoveY(gainedMoneyText.transform.position.y + 0.1f, tweenTime);
        gainedMoneyText.DOColor(new Color(gainedMoneyText.color.r, gainedMoneyText.color.g, gainedMoneyText.color.b, 0), tweenTime).OnComplete(() =>
        {
            gainedMoneyText.transform.position = new Vector3(gainedMoneyText.transform.position.x, gainedMoneyText.transform.position.y - 0.1f, gainedMoneyText.transform.position.z);
            gainedMoneyText.alpha = 1f;
            gainedMoneyText.gameObject.SetActive(false);
        });

    }

    private void DisplayMoneyMultiplier()
    {
        multiplierText.text = ((int)moneyMultiplier).ToString() + "X";
    }

    public float GetMultiplier()
    {
        return moneyMultiplier;
    }

    public void Upgrade(Mesh newMesh)
    {
        moneyMultiplier *= 4;
        DisplayMoneyMultiplier();
        transform.parent.GetComponent<MeshFilter>().mesh = newMesh;
        // Color change.
        pipeCoin.GetComponent<MeshFilter>().mesh = upgradeMesh;
    }

    public void ResetUpgrade()
    {
        moneyMultiplier = 1;
        DisplayMoneyMultiplier();
    }
}
