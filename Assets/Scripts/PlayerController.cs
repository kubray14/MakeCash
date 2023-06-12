using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float money = 0;
    [SerializeField] private float moneyAmount = 1f;
    [SerializeField] private float pipeSpeed = 1.0f;
    [SerializeField] private float pipeSpeedIncreaseAmount = 0.2f;
    [SerializeField] private int pipeSize = 1;
    [SerializeField] private List<GameObject> pipeList;
    [SerializeField] private List<Animator> animList;

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
            pipeSize++;
        }
        else
        {
           // close pipe button and open merge button 
        }
    }
}
