using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public float money = 0;
    public static float speedIncreaseValue = 0.15f;
    public static float moneyIncrease = 1f; //her butona bastýðýnda paranýn deðerinin artmasý
    [SerializeField] public int pipeSize = 1;
    [SerializeField] private List<CoinSpawner> pipeList;
    [SerializeField] public List<Animator> animList;
    [SerializeField] private bool canPlay = true;
    [SerializeField] private int mergingPipeIndex = 0;
    [SerializeField] private ParticleSystem upgradeParticle;
    [SerializeField] private ParticleSystem cardChangeParticle;
    [SerializeField] private Mesh greenPipeMesh;
     [SerializeField] private Mesh greenShortPipeMesh;
    [SerializeField] private Mesh yellowShortPipeMesh;
    [SerializeField] private GameObject card1;
    [SerializeField] private GameObject card2;
    public bool isTouch = false;


    private void Start()
    {
        EventManager.OnSpeedUpgrade.AddListener(IncreasePipeSpeed);
        EventManager.OnGainMoney.AddListener(IncreaseMoney);
        EventManager.OnAddPipe.AddListener(addPipe);
        EventManager.OnMachineMaxHeat.AddListener(MaxHeat);
        EventManager.OnCoolingComplete.AddListener(MinHeat);
        EventManager.OnPipeMerge.AddListener(PipeMerge);
        EventManager.OnCoinValueUpgrade.AddListener(moneyAmountIncrease);
        EventManager.OnNewCardUpgrade.AddListener(UpgradeCard);

        card2.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (!canPlay)
        {
            PipeEnd();
            return;
        }
        if (!(Input.touchCount != 0))
        {
            return;
        }
        if (!isTouch)
        {
            return;
        }
        EventManager.onSpinChange.Invoke(true);
        EventManager.onHeatAdd.Invoke();
        PipeStart();
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

    public void PipeEnd()
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
        float mergeTime = .75f;
        canPlay = false;
        List<Vector3> firsPositions = new List<Vector3>();

        for (int i = 0; i < pipeList.Count; i++)
        {
            firsPositions.Add(pipeList[i].transform.parent.position);
        }

        for (int i = mergingPipeIndex; i < pipeList.Count; i++) // merge movement.
        {
            pipeList[i].transform.parent.DOMove(pipeList[mergingPipeIndex].transform.parent.position, mergeTime);
        }

        yield return new WaitForSeconds(mergeTime);
        for (int i = mergingPipeIndex; i < pipeList.Count; i++)
        {
            if (i != mergingPipeIndex)
            {
                pipeList[i].transform.parent.position = firsPositions[i];
                pipeList[i].transform.parent.gameObject.SetActive(false);
            }
        }
        canPlay = true;

        if (mergingPipeIndex == 0)
        {
            pipeList[mergingPipeIndex].Upgrade(greenPipeMesh);
        }
        else
        {
            pipeList[mergingPipeIndex].Upgrade(greenShortPipeMesh);
        }

        for (int i = mergingPipeIndex + 1; i < pipeList.Count; i++)
        {
            pipeList[i].ResetUpgrade();
        }
        pipeSize = mergingPipeIndex + 1;
        mergingPipeIndex++;
        if (mergingPipeIndex > 3)
        {
            mergingPipeIndex = 0;
        }
        yield break;
    }

    private void IncreasePipeSpeed()
    {
        upgradeParticle.Play();
        foreach (Animator anim in animList)
        {
            anim.speed += speedIncreaseValue;
        }
    }

    private void IncreaseMoney(float moneyAmount)
    {
        money += moneyAmount;
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
            pipeList[pipeSize].transform.parent.gameObject.GetComponent<MeshFilter>().mesh = yellowShortPipeMesh;
            pipeList[pipeSize].transform.parent.gameObject.SetActive(true);
            pipeList[pipeSize].transform.parent.localScale = Vector3.zero;
            pipeList[pipeSize].transform.parent.DOScale(Vector3.one, 1f).SetEase(Ease.OutBounce);
            pipeSize++;
            upgradeParticle.Play();
        }
    }

    public void moneyAmountIncrease()
    {
        float increaseAmount = 0.1f;
        moneyIncrease += increaseAmount;
        upgradeParticle.Play();
    }

    public float GetAnimSpeed()
    {
        return animList[0].speed;
    }

    private void UpgradeCard()
    {
        canPlay = false;
        cardChangeParticle.Play();
        card1.SetActive(false);
        card2.SetActive(true);
        card2.transform.localScale = Vector3.one / 2;
        float tweenTime = 0.4f;
        card2.transform.DOScale(Vector3.one, tweenTime).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            canPlay = true;
        });
    }
}
