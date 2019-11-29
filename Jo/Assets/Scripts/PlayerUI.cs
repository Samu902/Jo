using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Button moveButton, shootButton, restButton, confirmButton;

    private PlayerMovement playerMovement;

    private void OnEnable()
    {
        moveButton.onClick.AddListener(OnMove);
        shootButton.onClick.AddListener(OnShoot);
        restButton.onClick.AddListener(OnRest);
        confirmButton.onClick.AddListener(OnConfirm);
    }

    private void OnDisable()
    {
        moveButton.onClick.RemoveListener(OnMove);
        shootButton.onClick.RemoveListener(OnShoot);
        restButton.onClick.RemoveListener(OnRest);
        confirmButton.onClick.RemoveListener(OnConfirm);
    }

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            OnMove();

        if (Input.GetKeyDown(KeyCode.S))
            OnShoot();

        if (Input.GetKeyDown(KeyCode.R))
            OnRest();

        if (Input.GetKeyDown(KeyCode.Return))
            OnConfirm();
    }

    public void OnMove()
    {
        playerMovement.ToggleAvailableMoves();
    }

    private void OnShoot()
    {
        Debug.Log("Hai attivato la modalità sparo");
    }

    private void OnRest()
    {
        Debug.Log("Hai attivato la modalità riposo");
    }

    private void OnConfirm()
    {
        Debug.Log("Hai appena confermato");
    }
}
