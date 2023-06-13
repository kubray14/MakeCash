using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;

    private void Start()
    {
        EventManager.onSpinChange.AddListener(spinChange);
    }

    private void Update()
    {
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
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
