//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;

public class RockPaperScissorManager : MonoBehaviour
{

    #region variables
    public GameObject[] E_Choice1;
    public GameObject[] P_Choice1;

    public Material E_Rock;
    public Material E_Paper;
    public Material E_Scissors;

    public Material P_Rock;
    public Material P_Paper;
    public Material P_Scissors;

    private int[] enemyChoiceData = new int[3];
    private int[] playerChoiceData = new int[3];

    public bool StartBattle;
    public bool StopShuffling;
    private bool ShowEnemyChoices;

    public bool enemyTurn;
    private bool playerTurn;

    public int input;
    #endregion

    // Use this for initialization
	void Start () {
        StartBattle = false;
        StopShuffling = false;
        ShowEnemyChoices = false;
        enemyTurn = true;
        playerTurn = false;

        input = 0;

        enemyChoiceData[0] = enemyChoiceData[1] = enemyChoiceData[2] = 0;
        playerChoiceData[0] = playerChoiceData[1] = playerChoiceData[2] = 0;
	}

    IEnumerator WaitBeforeShufflingStop() {
        yield return new WaitForSeconds(1.0f);
        StopShuffling = true;
        ShowEnemyChoices = true;
    }

    Material ApplyDesignatedTexture(int _textureData) {
        Material _mat = E_Rock;

        if (_textureData == 1)
            _mat = E_Rock;
        else if (_textureData == 2)
            _mat = E_Paper;
        else if (_textureData == 3)
            _mat = E_Scissors;

        return _mat;
    }

    void ShowEnemySet() {
        int max = 3;
        if (!StopShuffling) {       //this will do the shuffling effect
            for (int i = 0; i < max; i++) {
                int choice = Random.Range(1, 4);

                if (choice == 1)
                    E_Choice1[i].renderer.material = E_Rock;
                else if (choice == 2)
                    E_Choice1[i].renderer.material = E_Paper;
                else if (choice == 3)
                    E_Choice1[i].renderer.material = E_Scissors;

                if (i == 2)
                    StartCoroutine(WaitBeforeShufflingStop());
            }
        }

        if (StopShuffling == true && ShowEnemyChoices == true) {    //this will store the value of each enemy choice

            Debug.Log("Finished Shuffling");
            enemyChoiceData[0] = Random.Range(1, 4);
            enemyChoiceData[1] = Random.Range(1, 4);
            enemyChoiceData[2] = Random.Range(1, 4);

            E_Choice1[0].renderer.material = ApplyDesignatedTexture(enemyChoiceData[0]);
            E_Choice1[1].renderer.material = ApplyDesignatedTexture(enemyChoiceData[1]);
            E_Choice1[2].renderer.material = ApplyDesignatedTexture(enemyChoiceData[2]);
            Debug.Log("Finished Processing");

            ShowEnemyChoices = StopShuffling = enemyTurn = false;
            playerTurn = true;
        }
    }

    void InsertPlayerChoice() {         //storing player choices
        if (Input.GetKeyDown(KeyCode.A)) {
            P_Choice1[input].renderer.material = P_Rock;
            playerChoiceData[input] = 1;
            input += 1;
        }
        else if (Input.GetKeyDown(KeyCode.S)) {
            P_Choice1[input].renderer.material = P_Paper;
            playerChoiceData[input] = 2;
            input += 1;
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            P_Choice1[input].renderer.material = P_Scissors;
            playerChoiceData[input] = 3;
            input += 1;
        }
    }

	// Update is called once per frame
	void Update () {
        //startBattle variable is activated in MoveLocator() in CombatManagerScript attached in Camera
        if (StartBattle) {      
            if(enemyTurn == true)
                ShowEnemySet();
            if (playerTurn == true)
                InsertPlayerChoice();
        }

        if (Input.GetKeyDown(KeyCode.Y)) {
            StartBattle = false;
        }
	}
}

public enum BattleSceneStages {

    _LocatorMove,
    _ShuffleEnemyChoices,
    _ShowEnemyFinalChoices,
    _InputPlayerChoices,
    _CompareChoices,
    _EnterBattleScene
}
