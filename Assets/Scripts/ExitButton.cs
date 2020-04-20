using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public GameObject FightButton;
    private BattleMechanics FightButtonScript;

    private void Start()
    {
        FightButtonScript = FightButton.GetComponent<BattleMechanics>();
    }
    private void OnMouseDown()
    {
        if (!FightButtonScript.isBattle)
        {
            FightButtonScript.textInsideBox.text = "";
            FightButtonScript.ExitCombat();
        }
        else
        {
            FightButtonScript.textInsideBox.text = "Your forces can't escape!";
        }
        
    }


}
