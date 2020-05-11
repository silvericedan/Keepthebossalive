using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFightState : GameBaseState
{
    public override void EnterState(GameController game)
    {
        Debug.Log("Fight state");
        game.battleMusic.Play();
        if (game.hunger < 50)
        {
            game.Hunger();
            game.GoToBattlefield();
            game.StartCoroutine(game.Battle());
        }
        else
        {
            game.TransitionToState(game.GameOver);
        }
    }

    public override void Update(GameController game)
    {
        
    }
}
