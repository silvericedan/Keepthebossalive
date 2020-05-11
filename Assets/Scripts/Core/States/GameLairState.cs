using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLairState : GameBaseState
{
    public override void EnterState(GameController game)
    {
        game.mapMusic.Play();
        game.TextLairUpdate();
        game.SetCameraPosition(game.mapPosition);
    }

    public override void Update(GameController game)
    {
        if (game.fightButton.GetComponent<FightButton>().goToFight)
        {
            game.mapMusic.Stop();
            game.TransitionToState(game.FightState);
        }
    }
}
