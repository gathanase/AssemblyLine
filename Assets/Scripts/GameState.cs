using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    private GameController gameController;
    public long money = 0;
    public FactoryFloor factoryFloor;

    public GameState(GameController gameController) {
        factoryFloor = new FactoryFloor(gameController);
    }

    public void OnTick() {
        factoryFloor.OnTick();
    }
}
