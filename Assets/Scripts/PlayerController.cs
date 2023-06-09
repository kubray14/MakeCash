using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float money = 0;
    [SerializeField] private float moneyAmount = 1f;
    [SerializeField] private float pipeSpeed = 1.0f;
    [SerializeField] private float pipeSpeedIncreaseAmount = 0.2f;

    private void Start()
    {
        EventManager.OnSpeedUpgrade.AddListener(IncreasePipeSpeed);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            IncreaseMoney();
        }
    }

    private void IncreasePipeSpeed()
    {
        pipeSpeed += pipeSpeedIncreaseAmount;
    }

    private void IncreaseMoney()
    {
        money += moneyAmount;
        EventManager.OnGainMoney.Invoke();
    }

    public void DecreaseMoney(float cost)
    {
        money -= cost;
        EventManager.OnSpendMoney.Invoke();
    }

    public float GetMoney()
    {
        return money;
    }
}
