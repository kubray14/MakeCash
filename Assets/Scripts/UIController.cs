using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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

    [SerializeField] private TextMeshProUGUI speedLevel_Text;
    [SerializeField] private TextMeshProUGUI incomeLevel_Text;
    [SerializeField] private TextMeshProUGUI pipeLevel_Text;

    [SerializeField] private Image heat;
    [SerializeField] private float heatSpeed = 0.1f;
    [SerializeField] private float colorConst = 0.0125f;
    private float speed_value = 0.3f;
    private float pipe_value = 7f;
    private float income_value = 0.3f;

    private int speedLevel = 1;
    private int incomeLevel = 1;
    private int pipeLevel = 1;

    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Start()
    {
        speedCost_Text.text = "$" + speedCost.ToString();
        incomeCost_Text.text = "$" + incomeCost.ToString();
        pipeCost_Text.text = "$" + pipeCost.ToString();

        speedLevel_Text.text = "Level " + speedLevel.ToString();
        incomeLevel_Text.text = "Level " + incomeLevel.ToString();
        pipeLevel_Text.text = "Level " + pipeLevel.ToString();

        speedButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(speedCost);
            speedCost += speed_value;
            speed_value += 0.1f;
            speedLevel++;
            speedLevel_Text.text = "Level " + speedLevel;
            speedCost_Text.text = "$" + speedCost.ToString();
            playerController.animSpeedIncrease();
            EventManager.OnSpeedUpgrade.Invoke();
        });

        incomeButton.onClick.AddListener(() =>
        {
            playerController.DecreaseMoney(incomeCost);
            incomeCost += income_value;
            income_value += 0.1f;
            incomeLevel++;
            incomeLevel_Text.text = "Level " + incomeLevel.ToString();
            incomeCost_Text.text = "$" + incomeCost.ToString();
            playerController.moneyAmountIncrease();
        });

        pipeButton.onClick.AddListener(() =>
        {
            EventManager.OnAddPipe.Invoke();
            playerController.DecreaseMoney(pipeCost);
            pipeCost += pipe_value;
            pipe_value += (pipe_value / 2);
            pipeLevel++;
            pipeLevel_Text.text = "Level " + pipeLevel;
            pipeCost_Text.text = "$" + pipeCost.ToString();
            if (playerController.pipeSize == 4)
            {
                disablePipe.gameObject.SetActive(true);
                mergeButton.gameObject.SetActive(true);
            }
        });

        mergeButton.onClick.AddListener(() =>
        {
            EventManager.OnPipeMerge.Invoke();
            mergeButton.gameObject.SetActive(false);
        });

        EventManager.OnGainMoneyUI.AddListener(() =>
        {
            money.text = playerController.GetMoney().ToString("0.0");
            earnedMoney += playerController.money;
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
        EventManager.onCoolMachine.AddListener(coolingMachine);
        EventManager.onHeatAdd.AddListener(addHeat);
        CheckCostInactive();
    }

    private void addHeat()
    {
        heat.DOPause();
        heat.fillAmount += heatSpeed * Time.deltaTime;
        if (heat.color.r < 1)
        {
            heat.color = new Color(heat.color.r + colorConst * Time.deltaTime, 1, 0, 1);
        }
        else
        {
            heat.color = new Color(1, heat.color.g - colorConst * Time.deltaTime, 0, 1);
        }

        if (heat.fillAmount == 0)
        {
            heat.DOKill();
        }

        if (heat.fillAmount >= 1)
        {
            EventManager.OnMachineMaxHeat.Invoke(false);
        }
    }

    private void coolingMachine(bool isHeat)
    {
        if (!isHeat)
        {
            heat.DOColor(new Color(0, 1, 0), 2f);
            heat.DOFillAmount(0, 2f).OnComplete(() =>
            {
                EventManager.OnCoolingComplete.Invoke(true);
            });
        }
        else
        {
            heat.DOColor(new Color(0, 1, 0), 2f);
            heat.DOFillAmount(0, 2f);
        }
    }
    private void CheckCostInactive()
    {
        if (playerController.GetMoney() < speedCost)
        {
            disableSpeed.gameObject.SetActive(true);
            speedButton.interactable = false;
        }

        if (playerController.GetMoney() < incomeCost)
        {
            disableIncome.gameObject.SetActive(true);
            incomeButton.interactable = false;
        }

        if (playerController.GetMoney() < pipeCost)
        {
            disablePipe.gameObject.SetActive(true);
            pipeButton.interactable = false;
        }
    }

    private void CheckCostActive()
    {
        if (playerController.GetMoney() >= speedCost)
        {
            disableSpeed.gameObject.SetActive(false);
            speedButton.interactable = true;
        }

        if (playerController.GetMoney() >= incomeCost)
        {
            disableIncome.gameObject.SetActive(false);
            incomeButton.interactable = true;
        }

        if (playerController.GetMoney() >= pipeCost)
        {
            disablePipe.gameObject.SetActive(false);
            pipeButton.interactable = true;
        }
        //if (playerController.pipeSize == 4)
        //{
        //    disablePipe.gameObject.SetActive(true);
        //    //mergeButton.gameObject.SetActive(true);
        //}
    }

    public void closePipeButton()
    {
        pipeButton.gameObject.SetActive(false);
        mergeButton.gameObject.SetActive(true);
    }
}
