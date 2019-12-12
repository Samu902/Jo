using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public Renderer rend;
    [HideInInspector]
    public Color oldColor;

    public Vector3 Position { get => transform.position; set => transform.position = value; }

    private void Awake()
    {
        oldColor = Color.cyan;
        rend = GetComponentInChildren<Renderer>();
    }
}
