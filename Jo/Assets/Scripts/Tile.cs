using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public Renderer rend;
    [HideInInspector]
    public Color oldColor;

    private void Awake()
    {
        oldColor = Color.white;

        rend = GetComponentInChildren<Renderer>();
    }
}
