using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private Button moveButton, shootButton, restButton, confirmButton;
    public Toggle[] bulletToggles;

    private Player player;

    private void OnEnable()
    {
        moveButton.onClick.AddListener(OnMove);
        shootButton.onClick.AddListener(OnShoot);
        restButton.onClick.AddListener(OnRest);
        confirmButton.onClick.AddListener(OnConfirm);

        foreach (Toggle t in bulletToggles)
            t.onValueChanged.AddListener(OnBulletChanged);
    }

    private void OnDisable()
    {
        moveButton.onClick.RemoveListener(OnMove);
        shootButton.onClick.RemoveListener(OnShoot);
        restButton.onClick.RemoveListener(OnRest);
        confirmButton.onClick.RemoveListener(OnConfirm);

        foreach (Toggle t in bulletToggles)
            t.onValueChanged.RemoveListener(OnBulletChanged);
    }

    private void Start()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        if (player.isReady)
            return;

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
        player.movement.ToggleAvailableMoves(MoveType.Move);
    }

    private void OnShoot()
    {
        player.movement.ToggleAvailableMoves(MoveType.Shoot);
    }

    public void OnBulletChanged(bool isOn)
    {
        //This avoids calling the method on the deactivated toggle too, this way it is only called in the activated one
        if (!isOn)
            return;

        //if (!player.movement.movesAreVisible)
        //    return;

        //Update available moves (even visually)
        player.movement.ToggleAvailableMoves(MoveType.BulletChange);
        //player.movement.ToggleAvailableMoves(MoveType.Shoot);
    }

    private void OnRest()
    {
        player.movement.Rest();
    }

    private void OnConfirm()
    {
        player.movement.SendMove();
    }
}
