using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 10f;
    private bool CanSpin;

    private void Start()
    {
        EventManager.onSpinChange.AddListener(spinChange);
    }

    private void Update()
    {
        if (CanSpin)
        {
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }

    private void spinChange(bool stat)
    {
        CanSpin = stat;
    }
}
