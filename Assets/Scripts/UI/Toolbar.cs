using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Toolbar : MonoBehaviour
{
    public Toggle infoButton;
    public Toggle buildButton;
    public Toggle deleteButton;
    public Toggle rotateButton;
    public GameController gameController;

    void Awake() {
        infoButton.onValueChanged.AddListener(value => SetTool(value, GameTool.INFO));
        buildButton.onValueChanged.AddListener(value => SetTool(value, GameTool.BUILD));
        deleteButton.onValueChanged.AddListener(value => SetTool(value, GameTool.DELETE));
        rotateButton.onValueChanged.AddListener(value => SetTool(value, GameTool.ROTATE));
    }

    private void SetTool(bool active, GameTool gameTool) {
        if (active) {
            gameController.SetTool(gameTool);
        }
    }
}
