using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeleteTool : MonoBehaviour, IPointerDownHandler
{
    public GameController gameController;

    void Awake() {
        gameController = FindObjectOfType<GameController>();
    }

    public void OnPointerDown(PointerEventData eventData) {
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(pos3.x), Mathf.RoundToInt(pos3.y));
        Debug.Log("Delete clicked at " + pos);
        Machine machine;
        if (gameController.machines.TryGetValue(pos, out machine)) {
            gameController.Remove(machine);
        }
    }
}
