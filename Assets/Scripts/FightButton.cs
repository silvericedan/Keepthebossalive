using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightButton : MonoBehaviour
{
    public bool goToFight;
    // Start is called before the first frame update
    void Start()
    {
        goToFight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        goToFight = true;
    }
    private void OnMouseUp()
    {
        goToFight = true;
    }
}
