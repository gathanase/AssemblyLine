using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuildTool : GameTool
{
    override protected void OnClickEmpty(Vector2Int pos) {
        RollerMachine rollerMachine = Instantiate(gameController.rollerMachineModel);
        rollerMachine.Init(pos, Direction.SOUTH);
        gameController.Add(rollerMachine);
    }
}
