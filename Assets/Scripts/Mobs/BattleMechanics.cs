using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMechanics : MonoBehaviour
{
    public List<Mob> minions;
    public List<Mob> troops;

    public GameObject gameController;
    private GameController gameControllerScript;

    private void Start()
    {
        gameControllerScript = gameController.GetComponent<GameController>();
    }

    //Esta clase deberia inicializarse al comenzar el juego, ya que lleva el conteo de los cuerpos
    public BattleMechanics(List<Mob> listaMinions, List<Mob> listaTroops)
    {
        setMinions(listaMinions);
        setTroops(listaTroops);
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

    public void Combat()
    {
        int minionsAttack = 0;

        int troopsAttack = 0;
        foreach(Mob minion in minions)
        {
           minionsAttack += minion.rollDice();
        }
        foreach(Mob troop in troops)
        {
            troopsAttack += troop.rollDice();
        }

        if (minionsAttack >= troopsAttack)
        {
            troops.RemoveAt(Random.Range(0, troops.Count-1));
            Debug.Log("una tropa menos");
            if(troops.Count == 0)
            {
                Debug.Log("tropas enemigas eliminadas");
                gameControllerScript.ExitBattlefield();
            }
        }
        else
        {
            minions.RemoveAt(Random.Range(0, minions.Count-1));
            Debug.Log("un minion destruido");
            if (minions.Count == 0)
            {
                Debug.Log("tus minions han sido eliminados");
                gameControllerScript.ExitBattlefield();
            }
        }

    }
}
