using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
    public int bonus;

    public int rollDice()
    {
        int result;
        result = Random.Range(1, 7); //se rollea un numero entre 1 y 6, el 7 esta excluido.
        result += this.bonus;
        return result;
    }

}
