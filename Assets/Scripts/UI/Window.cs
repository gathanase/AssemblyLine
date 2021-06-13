using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected GameController gameController;

    public void Init(GameController gameController) {
        this.gameController = gameController;
        gameController.infoWindow = this;
        gameObject.SetActive(true);
    }

    public void Close() {
        gameController.infoWindow = null;
        gameObject.SetActive(false);
    }
}