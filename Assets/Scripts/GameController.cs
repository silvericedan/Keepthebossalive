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

    public AudioSource mapMusic;
    public AudioSource battleMusic;
    #endregion

    #region Private variables
    private int randomNumber;
    private Vector3 battlePosition;
    private Vector3 mapPosition;
   
    private bool inBattle;
    #endregion

    public List<Mob> rosterMinions;
    public List<Mob> rosterTroops;
    public GameObject botonCombate;
    private BattleMechanics scriptBotonCombate;
    public Text textLairBox;
    public int waveNumber = 0;
    public int hunger = 0;


    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else
        //if (instance != this)
        //{
        //    Destroy(gameObject);
        //    DontDestroyOnLoad(gameObject);

        //}
    }
    // Start is called before the first frame update
    void Start()
    {

        uiControllerInstance = uiController.GetComponent<UiController>();
        scriptBotonCombate = botonCombate.GetComponent<BattleMechanics>();
        minionsCount = 0;
        score = 0;
        screenbounds = GetScreenBounds();
        battlePosition = battlePlaceTransform.position;
        mapPosition = mapPlaceTransform.position;
        randomNumber = 0;   
        inBattle = false;
     

        rosterMinions.Add(new Undead());
        rosterMinions.Add(new Undead());
        rosterMinions.Add(new Undead());

        TextLairUpdate();
    }

    public void NextWave()
    {
        rosterTroops.Add(new Soldier());
        waveNumber += 1;
        for (int i = 0; i < waveNumber; i++)
        {
            rosterTroops.Add(new Soldier());
        }

    }

    public void Hunger()
    {
        hunger += waveNumber + UnityEngine.Random.Range(0, 1);
    }

    public void EndGame()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update()
    {
        randomNumber = UnityEngine.Random.Range(0, 100);
        
        if (!inBattle && fightButton.GetComponent<FightButton>().goToFight)
        {
            if(hunger < 5)
            {
                Hunger();
                mapMusic.Stop();
                battleMusic.Play();
                GoToBattlefield();
                inBattle = true;
                StartCoroutine(Battle());
                //StopCoroutine(heroesAdvance);
            }
            else
            {
                EndGame();
            }

            
        }
        
        //if (dayNight == DayNight.Day && !exclamationSpawned && randomNumber < 50)
        //{
        //    // generar party de heroes.
        //    exclamationInstance = Instantiate(exclamation, rooms[roomHeroesAt].position, Quaternion.identity);
        //    exclamationSpawned = true;
        //    roomHeroesAt++;
        //    heroesAdvance = StartCoroutine(HeroesAdvance());
        //}

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    
    public void TextLairUpdate()
    {
        textLairBox.text = "Minions: " + rosterMinions.Count +" units \n";
        textLairBox.text += "Corpses " + bodyCarcassCount + "\n";
        textLairBox.text += "Hunger "  + hunger + "\n"; //completar valor de hunger
    }

    public void TextLairClean()
    {
        textLairBox.text = "";
    }


    public void GoToBattlefield()
    {
        
        SetCameraPosition(battlePosition);
        uiControllerInstance.DisableLairUI();
        TextLairClean(); //limpiamos la pantalla del texto del dungeon
        NextWave(); //llenamos el roster de enemigos con nuevos soldados
        scriptBotonCombate.StartFirstRound();
    }

    public IEnumerator Battle()
    {
        scriptBotonCombate.setMinions(rosterMinions);
        scriptBotonCombate.setTroops(rosterTroops);

        yield return null;
    }

    public void ExitBattlefield()
    {
        SetCameraPosition(mapPosition);
        uiControllerInstance.EnableLairUI();
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
 
    public void SummonMinion()
    {
        if(bodyCarcassCount >=1)
        {
            rosterMinions.Add(new Undead());
            bodyCarcassCount -= 1;
            TextLairUpdate();
        }
    }

    public void CureBoss()
    {
        if (hunger >= 1)
        {
            hunger -= 1;
            bodyCarcassCount -= 1;
            TextLairUpdate();
        }
    }

}
