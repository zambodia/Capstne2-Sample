//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BattleMenuScript : MonoBehaviour {

    public Text label;
    public Slider slider;

    public GameObject Camera;

    public Canvas BattleMenu;
    public Canvas StatsCanvas;

    public int enemyCount;

    void Update() {
        enemyCount = (int) slider.value;
        label.text = enemyCount.ToString();
    }

    public void OpenMainMenu() {
        Camera.GetComponent<CombatManagerScript>().startBattleScene = true;
        StatsCanvas.gameObject.SetActive(true);
        BattleMenu.gameObject.SetActive(false);
    }
}
