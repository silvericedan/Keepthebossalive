using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    public int score;

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
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
