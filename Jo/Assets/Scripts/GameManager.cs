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

    void Start()
    {
        instance = this;

        remainingTime = cronoTime;

        initialPlayers = new GameObject[8];
        players = new List<GameObject>(initialPlayers);

        StartCoroutine(GameLoop());
    }

    void Update()
    {
        UpdateTime();
    }

    IEnumerator GameLoop()
    {
        //Ci si "paracaduta" nell'arena

        while (players.Count > 1)
        {
            while (turns % turnsToRestrict != 0 || turns == 0)
            {
                turns++;

                //15s per decidere la mossa, nel caso non si metta nulla si resta fermi
                remainingTime = cronoTime;
                while (remainingTime > 0)
                {
                    yield return new WaitForEndOfFrame();
                }
                Debug.Log("Tutti hanno segnato la mossa/è finito il tempo");
                //Tutti fanno la mossa contemporaneamente
                Debug.Log("Tutti hanno fatto la loro mossa e abbiamo visto cosa è successo all'ambiente");
                //Si muove l'ambiente    
            }
            //Se sono passati X turni si restringe l'area
            Debug.LogFormat("[Turno {0}] Si è ristretta l'area!", turns);
            break;
        }
        //Si riparte e si continua finché non ne rimane 1
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
