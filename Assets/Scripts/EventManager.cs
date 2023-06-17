using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent OnMachineUpgrade = new UnityEvent();
    public static UnityEvent OnSpeedUpgrade = new UnityEvent();
    public static UnityEvent OnCoinValueUpgrade = new UnityEvent();
    public static UnityEvent OnCoinTypeUpgrade = new UnityEvent();
    public static UnityEvent OnAddPipe = new UnityEvent();
    public static UnityEvent OnPipeMerge = new UnityEvent();
    public static UnityEvent<float> OnGainMoney = new UnityEvent<float>();
    public static UnityEvent OnGainMoneyUI = new UnityEvent();
    public static UnityEvent OnSpendMoney = new UnityEvent();
    public static UnityEvent onHeatAdd = new UnityEvent();
    public static UnityEvent<bool> OnMachineMaxHeat = new UnityEvent<bool>();
    public static UnityEvent<bool> onCoolMachine = new UnityEvent<bool>();
    public static UnityEvent<bool> OnCoolingComplete = new UnityEvent<bool>();
    public static UnityEvent<bool> onSpinChange = new UnityEvent<bool>();

    public static UnityEvent<Vector3> OnSpawnCoin = new UnityEvent<Vector3>();
}
