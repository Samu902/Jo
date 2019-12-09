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

    public List<Player> players;

    private void Awake()
    {
        instance = this;

        players = new List<Player>();

        //SIMULAZIONE DEBUG
        for (int i = 0; i <= 6; i++)
        {
            players.Add(new Player() { id = i });
        }
    }

    private void Start()
    {
        remainingTime = cronoTime;

        //DEBUG
        //StartCoroutine(GameLoop());
    }

    private void Update()
    {
        
    }

    private IEnumerator GameLoop()
    {
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
                int s = Mathf.RoundToInt(remainingTime);
                crono.text = s < 10 ? "0: 0" + s : "0: " + s;
                yield return new WaitForEndOfFrame();
            }
            Debug.Log("Tutti hanno segnato la mossa/è finito il tempo");

            //Tutti fanno la mossa contemporaneamente
            yield return new WaitForSeconds(0.2f);
            Debug.Log("Tutti hanno fatto la loro mossa");
            yield return StartCoroutine(ShowPlayerCameras());

            //Simulazione morte--Prova
            if (Random.value <= 0.75f)
            {
                int r = Random.Range(0, players.Count - 1);
                //DA RIGUARDARE E SCRIVERE MEGLIO
                FindObjectOfType<CameraManager>().TurnOffCam(players[r].id);
                Debug.Log("Giocatore " + (players[r].id + 1) + " eliminato");
                players.RemoveAt(r);

                //Da lasciare
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

    private IEnumerator ShowPlayerCameras()
    {
        CameraManager c = FindObjectOfType<CameraManager>();
        c.playerCamerasParent.gameObject.SetActive(true);
        c.SwitchCameras(players.Count);

        yield return new WaitForSeconds(2f);

        c.playerCamerasParent.gameObject.SetActive(false);
    }
}
