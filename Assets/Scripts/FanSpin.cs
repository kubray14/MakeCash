using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 50f;
    [SerializeField] private float minRotationSpeed = 50f;
    [SerializeField] private float maxRotationSpeed = 150f;
    private Vector3 turnDirection = -Vector3.forward;

    private void Start()
    {
        EventManager.onSpinChange.AddListener(spinChange);
    }

    private void Update()
    {
        transform.Rotate(turnDirection, rotationSpeed * Time.deltaTime, Space.World);
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
}
