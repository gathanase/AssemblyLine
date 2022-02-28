using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AxisListener {
    void OnVerticalAxis(int vAxis);

    void OnHorizontalAxis(int hAxis);
}


public class KeyController : MonoBehaviour
{
    float lastKeyTime = 0f;
    float keyDeltaTime = 0.4f;  // reduce to speed up auto-repeat input
    AxisListener axisListener = null;

    public void Start() {
    }

    public void SetListener(AxisListener axisListener) {
        this.axisListener = axisListener;
    }

    public void Update() {
        float vAxis = Input.GetAxisRaw("Vertical");
        float hAxis = Input.GetAxisRaw("Horizontal");
        bool modified = Input.GetButton("Modified");
        if (vAxis == 0 && hAxis == 0) {
            lastKeyTime = 0f;
        } else if (axisListener != null) {
            float now = Time.realtimeSinceStartup;
            if (now > lastKeyTime + keyDeltaTime) {
                int distance = modified ? 4 : 1;
                if (vAxis != 0) {
                    axisListener.OnVerticalAxis((int) vAxis * distance);
                }
                if (hAxis != 0) {
                    axisListener.OnHorizontalAxis((int) hAxis * distance);
                }
                lastKeyTime = now;
            }
        }
    }
}
