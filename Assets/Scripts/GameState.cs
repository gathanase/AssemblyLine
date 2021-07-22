using System.Collections;
using System.Collections.Generic;

public class GameState
{
    public long money = 0;
    public FactoryFloor factoryFloor;

    public class Save {
        public FactoryFloor.Save floor;
        public long money;
    }

    public GameState(GameController gameController) {
        factoryFloor = new FactoryFloor(gameController);
    }

    public void OnTick() {
        factoryFloor.OnTick();
    }

    public Save ToSave() {
        Save save = new Save();
        save.money = this.money;
        save.floor = factoryFloor.ToSave();
        return save;
    }

    public void FromSave(Save save) {
        this.money = save.money;
        this.factoryFloor.FromSave(save.floor);
    }
}