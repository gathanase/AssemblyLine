using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameDatabase gameDatabase;
    public Text moneyText;
    private long money = 0;
    public FactoryFloor factoryFloor;

    void Awake() {
        factoryFloor = new FactoryFloor();
        SetTool(ToolType.INFO);
    }

    void Start()
    {
        AddMoney(10000);
        InvokeRepeating("OnTick", 1, 1);
    }

    public void SetTool(ToolType toolType) {
        foreach (var gameTool in gameDatabase.GetTools()) {
            if (gameTool.GetToolType() == toolType) {
                gameTool.OnActivate();
                gameTool.gameObject.SetActive(true);
            } else {
                gameTool.gameObject.SetActive(false);
            }
        }
    }

    public FactoryFloor GetFactoryFloor() {
        return factoryFloor;
    }

    void OnTick() {
        factoryFloor.OnTick();
    }

    public void AddMoney(long amount) {
        money += amount;
        RefreshMoney();
    }

    public void RemoveMoney(long amount) {
        money -= amount;
        RefreshMoney();
    }

    private void RefreshMoney() {
        moneyText.text = money.ToString("C0");
    }
}
