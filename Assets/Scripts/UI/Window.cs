using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected GameController gameController;

    public void Init(GameController gameController) {
        this.gameController = gameController;
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
        gameController.infoWindow = null;
    }
}