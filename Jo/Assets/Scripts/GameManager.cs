using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text crono;
    private float remainingTime;

    void Start()
    {
        StartCoroutine(GameLoop());
        remainingTime = 15;
    }

    void Update()
    {
        UpdateTime();
    }

    IEnumerator GameLoop()
    {
        //Ci si "paracaduta" nell'arena
        while (true)
        {
            if (true)
                break;
        }
        //15s per decidere la mossa, nel caso non si metta nulla si resta fermi

        //Tutti fanno la mossa contemporaneamente

        //Si muove l'ambiente

        //Se sono passati X turni si restringe l'area

        //Si riparte e si continua finché non ne rimane 1
        yield return null;
    }

    private void UpdateTime()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            remainingTime = 15;
        }
        remainingTime -= Time.deltaTime;
        crono.text = Mathf.Round(remainingTime) < 10 ? "0: 0" + Mathf.Round(remainingTime) : "0: " + Mathf.Round(remainingTime);
    }
}
