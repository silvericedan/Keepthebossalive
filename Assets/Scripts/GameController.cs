using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Public Variables 
    [HideInInspector]
    public static GameController instance = null;
    [HideInInspector]
    public int bodyCarcassCount;
    [HideInInspector]
    public Vector3 mapPosition;
    #endregion

    #region Inspector Variables 
    [Header("Fight Settings")]
    [Space]
    [Tooltip("A donde se va a mover la camara cuando haya una pelea.")]
    public Transform battlePlaceTransform;
    public Transform mapPlaceTransform;
    [Tooltip("Para disparar la pelea.")]
    public GameObject fightButton;
    [Space]
    [Header("UI Settings")]
    [Space]
    [Tooltip("Para sincronizar con la interfase.")]
    public GameObject uiController;
  
    [Space]
    [Header("Music")]
    public AudioSource mapMusic;
    public AudioSource battleMusic;
    #endregion

    #region Private variables
    private Vector3 battlePosition;
    #endregion

    public GameObject Tutorial;
    public List<Mob> rosterMinions;
    public List<Mob> rosterTroops;
    public GameObject botonCombate;
    private BattleMechanics scriptBotonCombate;
    public Text textLairBox;
    public int waveNumber = 0;
    [HideInInspector]
    public int hunger = 0;

    /*CORE*/
    #region CORE
    private GameBaseState currentState;

    public readonly GameTutorialState TutorialState = new GameTutorialState();
    public readonly GameLairState LairState = new GameLairState();
    public readonly GameFightState FightState = new GameFightState();
    public readonly GameOverState GameOver = new GameOverState();
    public GameBaseState CurrentState
    {
        get { return currentState; }
    }
    #endregion

    #region MONOBEHAVIOUR CYCLES
    void Start()
    {
        TransitionToState(TutorialState);

        scriptBotonCombate = botonCombate.GetComponent<BattleMechanics>();

        battlePosition = battlePlaceTransform.position;
        mapPosition = mapPlaceTransform.position;
     

        rosterMinions.Add(new Undead());
        rosterMinions.Add(new Undead());
        rosterMinions.Add(new Undead());

        
    }
    void Update()
    {
        currentState.Update(this);
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

    }
    #endregion

    #region CONTEXT
    public void TransitionToState(GameBaseState gameState)
    {
        currentState = gameState;
        currentState.EnterState(this);
    }
    #endregion

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
