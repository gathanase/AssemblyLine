using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    protected static GameController gameController = null;
    protected static GameDatabase gameDatabase = null;

    public virtual void Init() {
        if (gameDatabase == null) {
            gameDatabase = FindObjectOfType<GameDatabase>();
        }
        if (gameController == null) {
            gameController = FindObjectOfType<GameController>();
        }
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}