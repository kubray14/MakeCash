using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private Button speedButton;
    [SerializeField] private Image disableSpeed;
    [SerializeField] private float speedCost = 7;
    [SerializeField] private Button incomeButton;
    [SerializeField] private Image disableIncome;
    [SerializeField] private float incomeCost = 7;
    [SerializeField] private Button pipeButton;
    [SerializeField] private Image disablePipe;
    [SerializeField] private float pipeCost = 50;
    [SerializeField] private Button mergeButton;
    private PlayerController playerController;

    [SerializeField] private Text money;
    [SerializeField] private float earnedMoney;
    [SerializeField] private float targetMoney;
    [SerializeField] private Image presentGreenImage;
    [SerializeField] private TextMeshProUGUI speedCost_Text;
    [SerializeField] private TextMeshProUGUI incomeCost_Text;
    [SerializeField] private TextMeshProUGUI pipeCost_Text;

    private float speed_value = 0.3f;
    private float pipe_value = 7f;
    private float income_value = 0.3f;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        speedCost_Text.text = "$" + speedCost.ToString();
        incomeCost_Text.text = "$" + incomeCost.ToString();
        pipeCost_Text.text = "$" + pipeCost.ToString();

        speedButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(speedCost);
            speedCost += speed_value;
            speed_value += 0.1f;
            speedCost_Text.text = "$" + speedCost.ToString();
            playerController.animSpeedIncrease();
            EventManager.OnSpeedUpgrade.Invoke();
        });

        incomeButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(incomeCost);
            incomeCost += income_value;
            income_value += 0.1f;
            incomeCost_Text.text = "$" + incomeCost.ToString();
            playerController.moneyAmountIncrease();
        });

        pipeButton.onClick.AddListener(() =>
        {
            EventManager.OnAddPipe.Invoke();
            playerController.DecreaseMoney(pipeCost);
            pipeCost += pipe_value;
            pipe_value += (pipe_value / 2);
            pipeCost_Text.text = "$" + pipeCost.ToString();
        });

        mergeButton.onClick.AddListener(() =>
        {
            EventManager.OnPipeMerge.Invoke();
        });

        EventManager.OnGainMoneyUI.AddListener(() =>
        {
            money.text = playerController.GetMoney().ToString();
            earnedMoney += playerController.moneyAmount;
            presentGreenImage.fillAmount = earnedMoney / targetMoney;
            if (earnedMoney == targetMoney)
            {
                // HEDÝYE UI;
            }
            CheckCostActive();
        });

        EventManager.OnSpendMoney.AddListener(() =>
        {
            money.text = playerController.GetMoney().ToString();
            CheckCostInactive();
        });

        CheckCostInactive();
    }

    private void CheckCostInactive()
    {
        if (playerController.GetMoney() < speedCost)
        {
            disableSpeed.gameObject.SetActive(true);
        }

        if (playerController.GetMoney() < incomeCost)
        {
             disableIncome.gameObject.SetActive(true);
        }

        if (playerController.GetMoney() < pipeCost)
        {
            disablePipe.gameObject.SetActive(true);
        }
        if (playerController.pipeSize == 4)
        {
            disablePipe.gameObject.SetActive(true);
            mergeButton.gameObject.SetActive(true);
        }
    }

    private void CheckCostActive()
    {
        if (playerController.GetMoney() >= speedCost)
        {
            disableSpeed.gameObject.SetActive(false);
        }

        if (playerController.GetMoney() >= incomeCost)
        {
            disableIncome.gameObject.SetActive(false);
        }

        if (playerController.GetMoney() >= pipeCost)
        {
            disablePipe.gameObject.SetActive(false);
        }
        if (playerController.pipeSize == 4)
        {
            disablePipe.gameObject.SetActive(true);
            mergeButton.gameObject.SetActive(true);
        }
    }

    public void closePipeButton()
    {
        pipeButton.gameObject.SetActive(false);
        mergeButton.gameObject.SetActive(true);
    }
}
