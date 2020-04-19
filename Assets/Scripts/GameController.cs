using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    #endregion

    #region Private variables
    private int randomNumber;
    private Vector3 battlePosition;
    private bool exclamationSpawned;
    private Coroutine heroesAdvance;
    private int roomHeroesAt;
    private Exclamation exclamationInstance;
    private bool inBattle;
    private GameObject[] heroesPosition;
    private GameObject[] minionsPosition;
    #endregion
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
        uiControllerInstance = uiController.GetComponent<UiController>();
        minionsCount = 0;
        score = 0;
        screenbounds = GetScreenBounds();
        battlePosition = battlePlaceTransform.position;
        randomNumber = 0;
        exclamationSpawned = false;
        roomHeroesAt = 0;
        inBattle = false;
        heroesPosition = GameObject.FindGameObjectsWithTag("HeroesPosition");
        minionsPosition = GameObject.FindGameObjectsWithTag("MinionsPosition");
        
    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = UnityEngine.Random.Range(0, 100000);

        if (!inBattle && exclamationSpawned && fightButton.GetComponent<FightButton>().goToFight)
        {
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
    
    public void GoToBattlefield()
    {
        
        SetCameraPosition(battlePosition);
        uiControllerInstance.DisableLairUI();
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
    GameObject minionInFigth;
    GameObject heroInFight;
    public IEnumerator Battle()
    {
         minionInFigth = Instantiate(minions[0], minionsPosition[0].transform.position, Quaternion.identity);
         heroInFight = Instantiate(heroes[0], heroesPosition[0].transform.position, Quaternion.identity);
        minionInFigth.GetComponent<MinionController>().SetHitpoint(100);
        heroInFight.GetComponent<HeroesController>().SetHitpoint(100);
        yield return new WaitForSeconds(2);
        StartCoroutine(TheBattle());
    }

    private IEnumerator TheBattle()
    {
        Debug.Log(minionInFigth.GetComponent<MinionController>().GetHitpoint());
        yield return null;
    }
    // Cuando un minion se meuere
    // Cuando matas un heroe se suma un body
}
