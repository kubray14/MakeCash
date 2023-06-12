using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button speedButton;
    [SerializeField] private float speedCost;
    [SerializeField] private Button incomeButton;
    [SerializeField] private float incomeCost;
    [SerializeField] private Button pipeButton;
    [SerializeField] private float pipeCost;
    [SerializeField] private Button mergeButton;
    private PlayerController playerController;

    [SerializeField] private Text money;
    [SerializeField] private TextMeshProUGUI speedCost_Text;
    [SerializeField] private TextMeshProUGUI incomeCost_Text;
    [SerializeField] private TextMeshProUGUI pipeCost_Text;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        speedButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(speedCost);
            speedCost += 3;
            speedCost_Text.text = speedCost.ToString();
            playerController.animSpeedIncrease();
            EventManager.OnSpeedUpgrade.Invoke();
        });

        incomeButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(incomeCost);
            incomeCost += 3;
            incomeCost_Text.text = incomeCost.ToString();
            playerController.moneyAmountIncrease();
        });

        pipeButton.onClick.AddListener(() =>
        {
            EventManager.OnAddPipe.Invoke();
            playerController.DecreaseMoney(pipeCost);
            pipeCost += 3;
            pipeCost_Text.text = pipeCost.ToString();
        });

        mergeButton.onClick.AddListener(() =>
        {
            EventManager.OnPipeMerge.Invoke();
        });

        EventManager.OnGainMoneyUI.AddListener(() =>
        {
            money.text = ((int)(playerController.GetMoney())).ToString();
            CheckCostActive();
        });

        EventManager.OnSpendMoney.AddListener(() =>
        {
            money.text = ((int)(playerController.GetMoney())).ToString();
            CheckCostInactive();
        });

        CheckCostInactive();
    }

    private void CheckCostInactive()
    {
        if (playerController.GetMoney() < speedCost)
        {
            speedButton.gameObject.SetActive(false);
        }

        if (playerController.GetMoney() < incomeCost)
        {
            incomeButton.gameObject.SetActive(false);
        }

        if (playerController.GetMoney() < pipeCost)
        {
            pipeButton.gameObject.SetActive(false);
        }
    }

    private void CheckCostActive()
    {
        if (playerController.GetMoney() >= speedCost)
        {
            speedButton.gameObject.SetActive(true);
        }

        if (playerController.GetMoney() >= incomeCost)
        {
            incomeButton.gameObject.SetActive(true);
        }

        if (playerController.GetMoney() >= pipeCost)
        {
            pipeButton.gameObject.SetActive(true);
        }
    }

    public void closePipeButton()
    {
        pipeButton.gameObject.SetActive(false);
        mergeButton.gameObject.SetActive(true);
    }
}
