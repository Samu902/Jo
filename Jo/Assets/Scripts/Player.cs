using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string playerName;
    public int id;

    [HideInInspector]
    public PlayerMovement movement;
    [HideInInspector]
    public PlayerUI ui;

    private void Awake()
    {
        
    }

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
        ui = GetComponent<PlayerUI>();

        //Register player in game manager's list
        //GameManager.instance.players.Add(this);
    }
}
