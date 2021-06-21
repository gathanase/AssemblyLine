using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Toolbar : MonoBehaviour
{
    public Toggle exitButton;
    public Toggle infoButton;
    public Toggle buildButton;
    public Toggle deleteButton;
    public Toggle rotateButton;
    public Toggle moveButton;
    public GameController gameController;

    void Awake() {
        exitButton.onValueChanged.AddListener(value => Application.Quit());
        infoButton.onValueChanged.AddListener(value => SetTool(value, ToolType.INFO));
        buildButton.onValueChanged.AddListener(value => SetTool(value, ToolType.BUILD));
        deleteButton.onValueChanged.AddListener(value => SetTool(value, ToolType.DELETE));
        rotateButton.onValueChanged.AddListener(value => SetTool(value, ToolType.ROTATE));
        moveButton.onValueChanged.AddListener(value => SetTool(value, ToolType.MOVE));
    }

    private void SetTool(bool active, ToolType gameTool) {
        if (active) {
            gameController.SetTool(gameTool);
        }
    }
}
