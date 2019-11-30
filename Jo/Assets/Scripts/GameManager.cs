using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Cronometer zone")]
    public TMP_Text crono;
    public int cronoTime;
    private float remainingTime;

    private bool[] playersConfirm;
    private int turns;
    [SerializeField]
    private int turnsToRestrict;

    private GameObject[] initialPlayers;
    private List<GameObject> players;

    private void Start()
    {
        instance = this;

        remainingTime = cronoTime;

        initialPlayers = new GameObject[8];
        players = new List<GameObject>(initialPlayers);

        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        //UpdateTime();
    }

    private IEnumerator GameLoop()
    {
        ////Ci si "paracaduta" nell'arena

        //while (players.Count > 1)
        //{
        //    while (turns % turnsToRestrict != 0 || turns == 0)
        //    {
        //        turns++;

        //        //15s per decidere la mossa, nel caso non si metta nulla si resta fermi
        //        remainingTime = cronoTime;
        //        while (remainingTime > 0)
        //        {
        //            yield return new WaitForEndOfFrame();
        //        }
        //        Debug.Log("Tutti hanno segnato la mossa/è finito il tempo");

        //        //Tutti fanno la mossa contemporaneamente
        //        Debug.Log("Tutti hanno fatto la loro mossa");
        //        if (Random.value >= 0.75f)
        //        {
        //            int r = Random.Range(0, players.Count - 1);
        //            players.RemoveAt(r);
        //            Debug.Log("Giocatore " + (r + 1) + " eliminato");
        //        }

        //        //Si muove l'ambiente    
        //        Debug.Log("Abbiamo visto cosa è successo all'ambiente");
        //    }
        //    //Se sono passati X turni si restringe l'area
        //    Debug.LogFormat("[Turno {0}] Si è ristretta l'area!", turns);
        //}
        ////Si riparte e si continua finché non ne rimane 1

        ////Roba vittoria


        ////Ci si "paracaduta" nell'arena
        //do
        //{
        //    do
        //    {
        //        turns++;

        //        //15s per decidere la mossa, nel caso non si metta nulla si resta fermi
        //        remainingTime = cronoTime;
        //        while (remainingTime > 0)
        //        {
        //            yield return new WaitForEndOfFrame();
        //        }
        //        Debug.Log("Tutti hanno segnato la mossa/è finito il tempo");

        //        //Tutti fanno la mossa contemporaneamente
        //        Debug.Log("Tutti hanno fatto la loro mossa");
        //        if (Random.value >= 0.75f)
        //        {
        //            int r = Random.Range(0, players.Count - 1);
        //            players.RemoveAt(r);
        //            Debug.Log("Giocatore " + (r + 1) + " eliminato");

        //            if (players.Count <= 1)
        //                Debug.Log("E' rimasto solo 1 giocatore");
        //        }

        //        //Si muove l'ambiente    
        //        Debug.Log("Abbiamo visto cosa è successo all'ambiente");
        //    }
        //    while (turns % turnsToRestrict != 0);

        //    //Quando sono passati X turni si restringe l'area
        //    Debug.LogFormat("[Turno {0}] Si è ristretta l'area!", turns);
        //}
        //while (players.Count > 1);
        //Si riparte e si continua finché non ne rimane 1

        //Roba vittoria


        //Ci si "paracaduta" nell'arena
        do
        {
            turns++;
            Debug.Log("[Turno " + turns + "]");

            //15s per decidere la mossa, nel caso non si metta nulla si resta fermi
            remainingTime = cronoTime;
            while (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                crono.text = Mathf.Round(remainingTime) < 10 ? "0: 0" + Mathf.Round(remainingTime) : "0: " + Mathf.Round(remainingTime);
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Tutti hanno segnato la mossa/è finito il tempo");

            //Tutti fanno la mossa contemporaneamente
            yield return new WaitForSeconds(0.2f);
            Debug.Log("Tutti hanno fatto la loro mossa");
            if (Random.value <= 0.75f)
            {
                int r = Random.Range(0, players.Count - 1);
                players.RemoveAt(r);
                Debug.Log("Giocatore " + (r + 1) + " eliminato");

                if (players.Count <= 1)
                {
                    yield return new WaitForSeconds(0.2f);
                    Debug.Log("E' rimasto solo 1 giocatore");
                    break;
                }
            }

            //Si muove l'ambiente    
            yield return new WaitForSeconds(0.2f);
            Debug.Log("Abbiamo visto cosa è successo all'ambiente");

            if(turns % turnsToRestrict == 0)
            {
                //Quando sono passati X turni si restringe l'area
                yield return new WaitForSeconds(0.2f);
                Debug.Log("Si è ristretta l'area!");
            }
        }
        while (players.Count > 1);
        //Si riparte e si continua finché non ne rimane 1

        //Roba vittoria
        yield return new WaitForSeconds(0.2f);
        Debug.Log("Hai vinto!!!");
    }

    private void UpdateTime()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            remainingTime = cronoTime;
        }
        remainingTime -= Time.deltaTime;
        crono.text = Mathf.Round(remainingTime) < 10 ? "0: 0" + Mathf.Round(remainingTime) : "0: " + Mathf.Round(remainingTime);
    }
}
