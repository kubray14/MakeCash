using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class ButtonScript : MonoBehaviour, IPointerDownHandler
{
    private PlayerController controller;
    private Button myButton;

    private void Awake()
    {
        controller = FindObjectOfType<PlayerController>();
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        myButton.onClick.AddListener(() =>
        {
            EventManager.onSpinChange.Invoke(false);
            EventManager.onCoolMachine.Invoke(true);
            controller.PipeEnd();
            controller.isTouch = false;
        });
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        controller.isTouch = true;
    }
}
