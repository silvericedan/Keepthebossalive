using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallButton : MonoBehaviour
{
    public GameObject gameController;
    private GameController gameControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameControllerScript = gameController.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        gameControllerScript.SummonMinion();
    }
    
}
