using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class GameTool : MonoBehaviour, IPointerDownHandler, IScrollHandler, IDragHandler
{
    protected GameController gameController;
    protected GameDatabase gameDatabase;

    public abstract ToolType GetToolType();

    public virtual void Awake() {
        gameController = FindObjectOfType<GameController>();
        gameDatabase = FindObjectOfType<GameDatabase>(true);
    }

    public virtual void OnActivate() {}

    public virtual void SetActive(bool active) {
        this.gameObject.SetActive(active);
    }

    public void OnPointerDown(PointerEventData eventData) {
        Vector3 pos3 = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector2Int pos = new Vector2Int(Mathf.RoundToInt(pos3.x), Mathf.RoundToInt(pos3.y));
        Machine machine;
        if (GetFactoryFloor().machines.TryGetValue(pos, out machine)) {
            OnClickMachine(machine);
        } else {
            OnClickEmpty(pos);
        }
    }

    public void OnScroll(PointerEventData eventData) {
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize + eventData.scrollDelta.y, 5, 50);
    }

    public void OnDrag(PointerEventData eventData) {
        Camera.main.transform.position = Camera.main.transform.position - new Vector3(eventData.delta.x, eventData.delta.y, 0) * 0.1f;
    }

    protected FactoryFloor GetFactoryFloor() {
        return gameController.GetFactoryFloor();
    }

    protected virtual void OnClickEmpty(Vector2Int pos) {
    }

    protected virtual void OnClickMachine(Machine machine) {
    }
}
