using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{

    [SerializeField] private static float rotationSpeed = 50f;
    [SerializeField] private static float minRotationSpeed = 50f;
    [SerializeField] private static float maxRotationSpeed = 150f;
    private static float speedMultiplier = 1f;
    private Vector3 turnDirection = -Vector3.forward;

    private void Start()
    {
        EventManager.onSpinChange.AddListener(spinChange);
        EventManager.OnSpeedUpgrade.AddListener(IncreaseSpeedMultiplier);
        speedMultiplier = 1f;
    }

    private void Update()
    {
        transform.Rotate(turnDirection, rotationSpeed * speedMultiplier * Time.deltaTime, Space.World);
    }

    private void spinChange(bool stat)
    {
        if (stat)
        {
            rotationSpeed = maxRotationSpeed;
        }
        else
        {
            rotationSpeed = minRotationSpeed;
        }
    }

    private void IncreaseSpeedMultiplier()
    {
        speedMultiplier += 0.05f;
    }
}
