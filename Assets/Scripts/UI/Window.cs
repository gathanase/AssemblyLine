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

    float lastKeyTime = 0f;
    float keyDeltaTime = 0.4f;  // reduce to speed up auto-repeat input
    virtual public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Close();
        }
        float vAxis = Input.GetAxisRaw("Vertical");
        float hAxis = Input.GetAxisRaw("Horizontal");
        if (vAxis == 0 && hAxis == 0) {
            lastKeyTime = 0f;
        } else {
            float now = Time.realtimeSinceStartup;
            if (now > lastKeyTime + keyDeltaTime) {
                if (vAxis != 0) {
                    OnVerticalAxis((int) vAxis);
                }
                if (hAxis != 0) {
                    OnHorizontalAxis((int) hAxis);
                }
                lastKeyTime = now;
            }
        }
    }

    virtual public void OnVerticalAxis(int vAxis) {}

    virtual public void OnHorizontalAxis(int hAxis) {}
}