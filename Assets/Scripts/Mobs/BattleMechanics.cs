using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleMechanics : MonoBehaviour
{
    public List<Mob> minions;
    public List<Mob> troops;

    public Text textInsideBox;
    public GameObject gameController;
    private GameController gameControllerScript;
    public int bodyCount;
    public bool isBattle;

    private void Start()
    {
        gameControllerScript = gameController.GetComponent<GameController>();
        isBattle = true;
    }

    //Esta clase deberia inicializarse al comenzar el juego, ya que lleva el conteo de los cuerpos
    public void StartFirstRound()
    {
        bodyCount = 0;
    }
    #region Getters y Seters
    public void setMinions(List<Mob> listaMinions)
    {
        minions = listaMinions;
    }

    public void setTroops(List<Mob> listaTroops)
    {
        troops = listaTroops;
    }

    public List<Mob> getMinions()
    {
        return minions;
    }
    public List<Mob> getTroops()
    {
        return troops;
    }
    #endregion

    private void OnMouseDown()
    {
        Combat();
    }

    public void ExitCombat() //se lo llama desde ExitButton
    {    
            gameControllerScript.ExitBattlefield();
    }

    public void Combat()
    {
        if (isBattle)
        {
            int minionsAttack = 0;

            int troopsAttack = 0;
            foreach (Mob minion in minions)
            {
                minionsAttack += minion.rollDice();
            }
            foreach (Mob troop in troops)
            {
                troopsAttack += troop.rollDice();
            }
            textInsideBox.text = "Minion Attack " + minionsAttack + "\n";
            textInsideBox.text += "Troop Attack " + troopsAttack + "\n";
            if (minionsAttack >= troopsAttack)
            {
                //gameControllerScript.DestroyTroop();
                int rCount = Random.Range(0, troops.Count - 1);
                textInsideBox.text += "A " + troops[rCount].nombre + " is destroyed \n";
                troops.RemoveAt(rCount);
                bodyCount += 1;

                if (troops.Count == 0)
                {
                    textInsideBox.text += "You have win the battle!!!";
                    gameControllerScript.bodyCarcassCount += bodyCount;
                    isBattle = false;
                    
                }
            }
            else
            {
                //gameControllerScript.DestroyMinion();
                int rCount = Random.Range(0, minions.Count - 1);
                textInsideBox.text += "A " + minions[rCount].nombre + " is destroyed \n";
                minions.RemoveAt(rCount);
                if (minions.Count == 0)
                {
                    textInsideBox.text += "You have lost the battle...";
                    gameControllerScript.bodyCarcassCount += bodyCount;
                    isBattle = false;                  
                }
            }
        }
        

    }
}
