using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum DayNight { Day, Night }
public class GameController : MonoBehaviour
{
    #region Public Variables 
    [HideInInspector]
    public static GameController instance = null;
    [HideInInspector]
    public int score;
    [HideInInspector]
    public int minionsCount;
    [HideInInspector]
    public int bodyCarcassCount;
    [HideInInspector]
    public Vector3 screenbounds;
    [HideInInspector]
    public DayNight dayNight = DayNight.Day;
    #endregion

    #region Inspector Variables 
    [Header("Fight Settings")]
    [Space]
    [Tooltip("A donde se va a mover la camara cuando haya una pelea.")]
    public Transform battlePlaceTransform;

    public Transform mapPlaceTransform;
    [Space]
    [Tooltip("Para disparar la pelea.")]
    public GameObject fightButton;
    [Header("UI Settings")]
    [Space]
    [Tooltip("Para sincronizar con la interfase.")]
    public GameObject uiController;
    [Space]
    [Tooltip("Para sincronizar con la interfase.")]
    public UiController uiControllerInstance;
    [Space]
    [Tooltip("Lista de habitaciones.")]
    public List<Transform> rooms;
    [Space]
    public Exclamation exclamation;
    public List<GameObject> heroes;
    public List<GameObject> minions;

    public AudioSource mapMusic;
    public AudioSource battleMusic;
    #endregion

    #region Private variables
    private int randomNumber;
    private Vector3 battlePosition;
    private Vector3 mapPosition;
    private bool exclamationSpawned;
    private Coroutine heroesAdvance;
    private int roomHeroesAt;
    public Exclamation exclamationInstance;
    private bool inBattle;
    private GameObject[] heroesPosition;
    private GameObject[] minionsPosition;
    #endregion

    public List<Mob> rosterMinions;
    public List<Mob> rosterTroops;
    public List<GameObject> minionInFigth;
    public List<GameObject> heroInFight;
    public GameObject botonCombate;
    private BattleMechanics scriptBotonCombate;
    public Text textLairBox;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        if(instance != this)
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        heroInFight = new List<GameObject>();
        minionInFigth = new List<GameObject>();
        uiControllerInstance = uiController.GetComponent<UiController>();
        minionsCount = 0;
        score = 0;
        screenbounds = GetScreenBounds();
        battlePosition = battlePlaceTransform.position;
        mapPosition = mapPlaceTransform.position;
        randomNumber = 0;
        exclamationSpawned = false;
        roomHeroesAt = 0;
        inBattle = false;
        heroesPosition = GameObject.FindGameObjectsWithTag("HeroesPosition");
        minionsPosition = GameObject.FindGameObjectsWithTag("MinionsPosition");

        rosterMinions.Add(new Zombie());
        rosterMinions.Add(new Zombie());
        rosterMinions.Add(new Zombie());

        rosterTroops.Add(new Farmer());
        rosterTroops.Add(new Farmer());
        rosterTroops.Add(new Farmer());

        TextLairUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = UnityEngine.Random.Range(0, 100);
        
        if (!inBattle && exclamationSpawned && fightButton.GetComponent<FightButton>().goToFight)
        {
            mapMusic.Stop();
            battleMusic.Play();
            GoToBattlefield();
            inBattle = true;
            StartCoroutine(Battle());
            StopCoroutine(heroesAdvance);
        }
        
        if (dayNight == DayNight.Day && !exclamationSpawned && randomNumber < 50)
        {
            // generar party de heroes.
            exclamationInstance = Instantiate(exclamation, rooms[roomHeroesAt].position, Quaternion.identity);
            exclamationSpawned = true;
            roomHeroesAt++;
            heroesAdvance = StartCoroutine(HeroesAdvance());
        }
        
    }
    
    public void TextLairUpdate()
    {
        textLairBox.text = "Minions: " + rosterMinions.Count +" units \n";
        textLairBox.text += "Corpses " + bodyCarcassCount + "\n";
        textLairBox.text += "Hunger "  + "\n"; //completar valor de hunger
    }

    public void TextLairClean()
    {
        textLairBox.text = "";
    }

    public void GoToBattlefield()
    {
        
        SetCameraPosition(battlePosition);
        uiControllerInstance.DisableLairUI();
        TextLairClean();
    }

    public void ExitBattlefield()
    {
        SetCameraPosition(mapPosition);
        uiControllerInstance.EnableLairUI();
        heroesAdvance = StartCoroutine(HeroesAdvance());
        inBattle = false;
        battleMusic.Stop();
        mapMusic.Play();

        TextLairUpdate();


    }
    public void SetCameraPosition(Vector3 position)
    {
        Camera mainCamera = Camera.main;
        mainCamera.gameObject.transform.position = new Vector3(
                position.x,
                position.y,
                mainCamera.transform.position.z);
    }

    private Vector3 GetScreenBounds()
    {
        Camera mainCamera = Camera.main;
        Vector3 screenVector = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);
        return mainCamera.ScreenToWorldPoint(screenVector);
    }
    public void GenerateMinions()
    {
        minionsCount++;

    }

    private IEnumerator HeroesAdvance()
    {
        while(roomHeroesAt< rooms.Count)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(4, 10));
            exclamationInstance.transform.position = rooms[roomHeroesAt].position;
            roomHeroesAt++;
        }
        //exclamationInstance = Instantiate(exclamation, rooms[roomHeroesAt].position, Quaternion.identity);
    }
    private void ChangeDayPeriod()
    {
        uiControllerInstance.ChangeDayNight(dayNight);
        if(dayNight == DayNight.Day)
        {
            dayNight = DayNight.Night;
        }
        else
        {
            dayNight = DayNight.Day;
        }
    }
    
    public IEnumerator Battle()
    {
        
        Debug.Log(rosterMinions[0]);

        scriptBotonCombate = botonCombate.GetComponent<BattleMechanics>();
        scriptBotonCombate.setMinions(rosterMinions);
        scriptBotonCombate.setTroops(rosterTroops);
        //for (int i = 0; i < rosterMinions.Count; i++)
        //{
        //    chooseMinion = UnityEngine.Random.Range(0, 2);
        //    minionInFigth.Add(Instantiate(minions[chooseMinion], minionsPosition[i].transform.position, Quaternion.identity));
        //}
        //for (int i = 0; i < rosterTroops.Count; i++)
        //{
        //    heroInFight.Add(Instantiate(heroes[0], heroesPosition[i].transform.position, Quaternion.identity));
        //}

        yield return null;
        //minionInFigth = Instantiate(minions[0], minionsPosition[0].transform.position, Quaternion.identity);
        //heroInFight = Instantiate(heroes[0], heroesPosition[0].transform.position, Quaternion.identity);
        //minionInFigth.GetComponent<MinionController>().SetHitpoint(100);
        //heroInFight.GetComponent<HeroesController>().SetHitpoint(100);
        // yield return new WaitForSeconds(2);
        /// StartCoroutine(TheBattle());
    }
    int chooseMinion;
    private IEnumerator TheBattle()
    {
        Debug.Log(rosterMinions[0]);

        scriptBotonCombate = botonCombate.GetComponent<BattleMechanics>();
        scriptBotonCombate.setMinions(rosterMinions);
        scriptBotonCombate.setTroops(rosterTroops);
        scriptBotonCombate.StartFirstRound();
       
        yield return null;
    }
    // Cuando un minion se meuere
    // Cuando matas un heroe se suma un body
    public void DestroyMinion()
    {
        Destroy(minionInFigth[minionInFigth.Count - 1]);
        minionInFigth.RemoveAt(minionInFigth.Count - 1);
    }
    public void DestroyTroop()
    {
        Destroy(heroInFight[heroInFight.Count - 1]);
        heroInFight.RemoveAt(heroInFight.Count - 1);
    }
    public void SummonMinion()
    {
        if(bodyCarcassCount >=1)
        {
            rosterMinions.Add(new Zombie());
            bodyCarcassCount -= 1;
            TextLairUpdate();
        }
    }


}
