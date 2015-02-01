//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

    public GameObject MainCharacter;
    public GameObject BattleSceneWarriors;
    public Canvas MainMenuCanvas;
    public Canvas BattleMenuCanvas;

    //if player chooses commando type, pass to the player the chosen type
    public void SetToCommandoType() {
        MainCharacter.GetComponent<MainCharacterScript>().SetClassType(1);
        Debug.Log("Commando Class Chosen");
        BattleSceneWarriors.SetActive(true);
        BattleMenuCanvas.gameObject.SetActive(true);
        MainMenuCanvas.gameObject.SetActive(false);
    }

    //if player chooses sentinel type, pass to the player the chosen type
    public void SetToSentinelType() {
        MainCharacter.GetComponent<MainCharacterScript>().SetClassType(2);
        Debug.Log("Sentinel Class Chosen");
        BattleSceneWarriors.SetActive(true);
        BattleMenuCanvas.gameObject.SetActive(true);
        MainMenuCanvas.gameObject.SetActive(false);
    }

    //if player chooses ravager type, pass to the player the chosen type
    public void SetToRavagerType() {
        MainCharacter.GetComponent<MainCharacterScript>().SetClassType(3);
        Debug.Log("Ravager Class Chosen");
        BattleSceneWarriors.SetActive(true);
        BattleMenuCanvas.gameObject.SetActive(true);
        MainMenuCanvas.gameObject.SetActive(false);
    }
}
