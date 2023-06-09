using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button speedButton;
    [SerializeField] private float speedCost;
    [SerializeField] private Button incomeButton;
    [SerializeField] private float incomeCost;
    [SerializeField] private Button pipeButton;
    [SerializeField] private float pipeCost;
    private PlayerController playerController;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        speedButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(speedCost);
            EventManager.OnSpeedUpgrade.Invoke();
        });

        incomeButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(incomeCost);
            EventManager.OnSpeedUpgrade.Invoke();
        });

        pipeButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(pipeCost);
            EventManager.OnSpeedUpgrade.Invoke();
        });

        EventManager.OnGainMoney.AddListener(() =>
        {
            CheckCostActive();
        });

        EventManager.OnSpendMoney.AddListener(() =>
        {
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
}
