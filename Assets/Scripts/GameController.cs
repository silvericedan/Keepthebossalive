using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum DayNight { Day, Night }
public class GameController : MonoBehaviour
{
    #region Variables 
    public static GameController instance = null;
    public int score;
    private Vector3 battlePosition;
    public Transform battlePlaceTransform;
    public int minionsCount;
    public int bodyCarcassCount;
    public Vector3 screenbounds;
    public DayNight dayNight = DayNight.Day;
    public GameObject uiController;
    public UiController uiControllerInstance;
    public GameObject fightButton;
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
        Debug.Log(uiControllerInstance);
        
        minionsCount = 0;
        score = 0;
        screenbounds = GetScreenBounds();
        battlePosition = battlePlaceTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (fightButton.GetComponent<FightButton>().goToFight)
        {
            GoToBattlefield();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            // FOR DEBUG only
            uiControllerInstance.ChangeDayNight();
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            // FOR DEBUG only
            GoToBattlefield();
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
    // Cuando un minion se meuere
    // Cuando matas un heroe se suma un body
}
