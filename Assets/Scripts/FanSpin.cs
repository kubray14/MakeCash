using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{

    [SerializeField] private float rotationSpeed = 10f;
    private readonly bool CanSpin;

    private void Update()
    {
        if (CanSpin)
        {
            transform.Rotate(-Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}
