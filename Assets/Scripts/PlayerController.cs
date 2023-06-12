using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float money = 0;
    [SerializeField] private float moneyAmount = 1f;
    [SerializeField] private float pipeSpeed = 1.0f;
    [SerializeField] private float speedIncreaseValue = 0.15f;
    [SerializeField] private float pipeSpeedIncreaseAmount = 0.2f;
    [SerializeField] private float moneyIncrease = 1f; //her butona bastýðýnda paranýn deðerinin artmasý
    [SerializeField] public int pipeSize = 1;
    [SerializeField] private List<GameObject> pipeList;
    [SerializeField] public List<Animator> animList;

    private void Start()
    {
        EventManager.OnSpeedUpgrade.AddListener(IncreasePipeSpeed);
        EventManager.OnGainMoney.AddListener(IncreaseMoney);
        EventManager.OnAddPipe.AddListener(addPipe);
    }
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            EventManager.onSpinChange.Invoke(true);
            Touch theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Stationary ||theTouch.phase == TouchPhase.Began)
            {
                for (int i = 0; i < pipeSize; i++)
                {
                    animList[i].SetBool("coinMove", true);
                }
            }
            else if (theTouch.phase ==  TouchPhase.Ended)
            {
                EventManager.onSpinChange.Invoke(false);
                for (int i = 0; i < pipeSize; i++)
                {
                    animList[i].SetBool("coinMove", false);
                }
            }
        }   
    }
    private void IncreasePipeSpeed()
    {
        pipeSpeed += pipeSpeedIncreaseAmount;
    }

    private void IncreaseMoney()
    {
        money += moneyAmount;
        EventManager.OnGainMoneyUI.Invoke();
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

    public void addPipe()
    {
        if (pipeSize < 4)
        {
            pipeList[pipeSize].SetActive(true);
            //event pipe buton false merge true
            pipeSize++;
        }
    }

    public void moneyAmountIncrease()
    {
        moneyAmount += moneyIncrease;
    }

    public void animSpeedIncrease()
    {
        foreach (Animator anim in animList)
        {
            anim.speed += speedIncreaseValue; 
        }
    }
}
