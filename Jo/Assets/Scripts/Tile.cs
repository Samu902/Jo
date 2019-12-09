using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [HideInInspector]
    public Vector3 pos;
    [HideInInspector]
    public Renderer rend;
    [HideInInspector]
    public Color oldColor;

    private void Awake()
    {
        pos = transform.position;
        oldColor = Color.cyan;
        rend = GetComponentInChildren<Renderer>();
    }
}
