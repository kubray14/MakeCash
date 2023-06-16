using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float money = 0;
    [SerializeField] public float moneyAmount = 1f;
    [SerializeField] private float pipeSpeed = 1.0f;
    [SerializeField] private float speedIncreaseValue = 0.15f;
    [SerializeField] private float pipeSpeedIncreaseAmount = 0.2f;
    [SerializeField] private float moneyIncrease = 0.1f; //her butona bastýðýnda paranýn deðerinin artmasý
    [SerializeField] public int pipeSize = 1;
    [SerializeField] private List<GameObject> pipeList;
    [SerializeField] public List<Animator> animList;
    [SerializeField] private bool canPlay = true;

    private void Start()
    {
        EventManager.OnSpeedUpgrade.AddListener(IncreasePipeSpeed);
        EventManager.OnGainMoney.AddListener(IncreaseMoney);
        EventManager.OnAddPipe.AddListener(addPipe);
        EventManager.OnMachineMaxHeat.AddListener(MaxHeat);
        EventManager.OnCoolingComplete.AddListener(MinHeat);
        EventManager.OnPipeMerge.AddListener(PipeMerge);
    }
    private void Update()
    {
        if (!canPlay)
        {
            PipeEnd();
            return;
        }

        if (Input.touchCount > 0)
        {
            EventManager.onSpinChange.Invoke(true);
            Touch theTouch = Input.GetTouch(0);
            if (theTouch.phase == TouchPhase.Stationary || theTouch.phase == TouchPhase.Began)
            {
                EventManager.onHeatAdd.Invoke();
                PipeStart();
            }
            else if (theTouch.phase == TouchPhase.Ended)
            {
                EventManager.onSpinChange.Invoke(false);
                EventManager.onCoolMachine.Invoke(false);
                PipeEnd();
            }
        }
    }

    private void MaxHeat(bool isHeat)
    {
        canPlay = isHeat;
        EventManager.onCoolMachine.Invoke(canPlay);
        PipeEnd();
    }

    private void MinHeat(bool isHeat)
    {
        canPlay = isHeat;
    }

    private void PipeStart()
    {
        for (int i = 0; i < pipeSize; i++)
        {
            animList[i].SetBool("coinMove", true);
        }
    }

    private void PipeEnd()
    {
        for (int i = 0; i < pipeSize; i++)
        {
            animList[i].SetBool("coinMove", false);
        }
    }

    private void PipeMerge()
    {
        StartCoroutine(PipeMerge_Coroutine());
    }

    private IEnumerator PipeMerge_Coroutine()
    {
        float mergeTime = 1f;
        canPlay = false;
        List<Vector3> firsPositions = new List<Vector3>();
        for (int j = 0; j < pipeList.Count; j++)
        {
            firsPositions.Add(pipeList[j].transform.position);
        }
        for (int i = 1; i < pipeList.Count; i++)
        {
            pipeList[i].transform.DOMove(pipeList[0].transform.position, mergeTime);
        }

        yield return new WaitForSeconds(mergeTime);
        for (int i = 1; i < pipeList.Count; i++)
        {
            pipeList[i].transform.position = firsPositions[i];
            pipeList[i].gameObject.SetActive(false);
        }
        pipeSize = 1;
        canPlay = true;
        EventManager.OnPipeUpgrade.Invoke(); // Pipe Upgrade Eksik !!!!!!!!!!!!!
        yield break;
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
