using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected GameController gameController;

    public void Init(GameController gameController) {
        this.gameController = gameController;
    }

    protected void Close() {
        Destroy(gameObject);
        gameController.infoWindow = null;
    }
}