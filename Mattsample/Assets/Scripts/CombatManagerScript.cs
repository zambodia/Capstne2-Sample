//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CombatManagerScript : MonoBehaviour
{

    #region variables
    public GameObject BattleMenuManager;

    public GameObject Player;
    public GameObject[] Enemy;

    public GameObject CharLocator;
    public GameObject[] EnemyLocator;

    public GameObject Countdown;

    public Transform StartingLine;
    public Transform FinishLine;

    public Text p_Class;
    public Text p_Level;
    public Text p_Exp;
    public Text p_HP;
    public Text p_Attack;
    public Text p_Defense;
    public Text p_Speed;
    public Text p_Intuition;

    public Text e_HP;
    public Text e_Attack;
    public Text e_Defense;
    public Text e_Speed;
    public Text e_ExpReward;

    public int SelectedEnemyID;
    public bool startBattleScene;
    private bool stopLocators;
    private string battlePhase;

    BattleMenuScript battleMenu;
    MainCharacterScript playerClone;
    WeakEnemySampleScript enemyClone;
    WeakEnemySampleScript enemy1, enemy2, enemy3;
    #endregion

    #region For RockPaperScissors
    public GameObject[] E_Choice1;
    public GameObject[] P_Choice1;
    public GameObject[] R_Choice1;

    public Text playerDamage;
    public Text enemyDamage;

    public Material E_Rock;
    public Material E_Paper;
    public Material E_Scissors;

    public Material P_Rock;
    public Material P_Paper;
    public Material P_Scissors;

    public Material Win;
    public Material Loss;
    public Material Tie;

    private Material Hidden;

    private int[] enemyChoiceData = new int[3];
    private int[] playerChoiceData = new int[3];
    private int[] resultData = new int[3];

    private int playerInput;
    private int turn;
    private float currentTime;

    private bool allowInput;
    private bool stopShuffling;
    private bool timesUp;
    #endregion

    // Use this for initialization
	void Start () {
        battlePhase = BattleScenePhases._Blank.ToString();

        playerInput = 0;
        turn = 0;

        playerChoiceData[0] = playerChoiceData[1] = playerChoiceData[2] = 0;
        Hidden = E_Choice1[0].renderer.material;

        battleMenu = BattleMenuManager.GetComponent<BattleMenuScript>();
        playerClone = Player.GetComponent<MainCharacterScript>();
        enemyClone = Enemy[SelectedEnemyID].GetComponent<WeakEnemySampleScript>();

        enemy1 = Enemy[0].GetComponent<WeakEnemySampleScript>();
        enemy2 = Enemy[1].GetComponent<WeakEnemySampleScript>();
        enemy3 = Enemy[2].GetComponent<WeakEnemySampleScript>();
	}

    //shows the stats of player and enemy
    void ShowBothStats() {
        p_Class.text = "Class: " + playerClone.GetClass();
        p_Level.text = "Level: " + playerClone.GetLevel();
        p_Exp.text = "Exp: " + playerClone.GetCurrentExp() + "/ " + playerClone.GetTotalExp();
        p_HP.text = "HP: " + playerClone.GetCurrentHP() + "/ " + playerClone.GetMaxHP();
        p_Attack.text = "Atk: " + playerClone.GetAttack();
        p_Defense.text = "Def: " + playerClone.GetDefense();
        p_Speed.text = "Spd: " + playerClone.GetSpeed();
        p_Intuition.text = "Int: " + playerClone.GetIntuition();

        e_HP.text = "HP: " + enemyClone.GetCurrentHP() + "/ " + enemyClone.GetMaxHP();
        e_Attack.text = "Atk: " + enemyClone.GetAttack();
        e_Defense.text = "Def: " + enemyClone.GetDefense();
        e_Speed.text = "Spd: " + enemyClone.GetSpeed();
        e_ExpReward.text = "ExpRwd: " + enemyClone.GetExpReward();
    }

    //calls the AddDamage function in enemy script and add damage based on player's attack points
    void DamageEnemy(int _playerAttackPoints) {
        enemyClone.AddDamage(_playerAttackPoints);
        playerClone.addDamage = false;
    }

    //calls the AddDamage function in player script and add damage based on enemy's attack points
    void DamagePlayer(int _enemyAttackPoints) {
        playerClone.AddDamage(_enemyAttackPoints);
        enemyClone.addDamage = false;
    }

    void CheckEnemyNumber() {   //for debug purposes
        int EnemyCounter = battleMenu.enemyCount;
        for (int i = 0; i < EnemyCounter; i++) {
            Enemy[i].gameObject.SetActive(true);
            EnemyLocator[i].gameObject.SetActive(true);
        }
    }

    IEnumerator WaitBeforeShufflingStop() {
        yield return new WaitForSeconds(1.0f);
        stopShuffling = true;
    }

    Material ApplyDesignatedTexture(int _textureData) {
        Material _mat = E_Rock;

        if (_textureData == 1)
            _mat = E_Rock;
        else if (_textureData == 2)
            _mat = E_Paper;
        else if (_textureData == 3)
            _mat = E_Scissors;
        Debug.Log("Applying materials");
        return _mat;
    }

    IEnumerator Wait() {
        yield return new WaitForSeconds(0.75f);
        startBattleScene = true;
        stopLocators = false;
        ResetMaterials();
    }

    void ResetMaterials() {
        for (int i = 0; i < 3; i++) {
            E_Choice1[i].renderer.material = Hidden;
            P_Choice1[i].renderer.material = Hidden;
            R_Choice1[i].renderer.material = Hidden;
            playerChoiceData[i] = 0;
        }
        Debug.Log("Battle has ended");
        battlePhase = BattleScenePhases._Blank.ToString();
    }

    #region Battle Phases
    void MoveLocators() {
        //stopLocators is FALSE on default
        if (stopLocators) {
            battlePhase = BattleScenePhases._ShowEnemyChoices.ToString();
            startBattleScene = false;
        }

        if(!stopLocators) {
            CharLocator.transform.position += new Vector3(0.05f * playerClone.GetSpeed() * Time.deltaTime, 0, 0);
            EnemyLocator[0].transform.position += new Vector3(0.035f * enemy1.GetSpeed() * Time.deltaTime, 0, 0);
            EnemyLocator[1].transform.position += new Vector3(0.035f * enemy2.GetSpeed() * Time.deltaTime, 0, 0);
            EnemyLocator[2].transform.position += new Vector3(0.035f * enemy3.GetSpeed() * Time.deltaTime, 0, 0);

            //return locators to starting line
            if (CharLocator.transform.position.x >= FinishLine.position.x) {
                stopLocators = true;
                turn = 0;
                CharLocator.transform.position = new Vector3(StartingLine.position.x,
                                                            CharLocator.transform.position.y,
                                                            CharLocator.transform.position.z);
            }
            else if (EnemyLocator[0].transform.position.x >= FinishLine.position.x) {
                stopLocators = true;
                SelectedEnemyID = 0;
                EnemyLocator[0].transform.position = new Vector3(StartingLine.position.x,
                                                            EnemyLocator[0].transform.position.y,
                                                            EnemyLocator[0].transform.position.z);
            }
            else if (EnemyLocator[1].transform.position.x >= FinishLine.position.x) {
                stopLocators = true;
                SelectedEnemyID = 1;
                EnemyLocator[1].transform.position = new Vector3(StartingLine.position.x,
                                                            EnemyLocator[1].transform.position.y,
                                                            EnemyLocator[1].transform.position.z);
            }
            else if (EnemyLocator[2].transform.position.x >= FinishLine.position.x) {
                stopLocators = true;
                SelectedEnemyID = 2;
                EnemyLocator[2].transform.position = new Vector3(StartingLine.position.x,
                                                            EnemyLocator[2].transform.position.y,
                                                            EnemyLocator[2].transform.position.z);
            }
        }
    }

    void ShuffleEnemySet() {
        int max = 3;
        if (!stopShuffling) {       //this will do the shuffling effect
            Debug.Log("Shuffling choices");
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

        if (stopShuffling) {    //this will store the value of each enemy choice
            Debug.Log("Finished Shuffling");
            enemyChoiceData[0] = Random.Range(1, 4);
            enemyChoiceData[1] = Random.Range(1, 4);
            enemyChoiceData[2] = Random.Range(1, 4);

            E_Choice1[0].renderer.material = ApplyDesignatedTexture(enemyChoiceData[0]);
            E_Choice1[1].renderer.material = ApplyDesignatedTexture(enemyChoiceData[1]);
            E_Choice1[2].renderer.material = ApplyDesignatedTexture(enemyChoiceData[2]);

            battlePhase = BattleScenePhases._InputPlayerChoices.ToString();
            allowInput = true;
            timesUp = false;
            currentTime = 4;
        }
    }

    void GetPlayerChoices() {
        if (allowInput && timesUp == false) {

            currentTime -= 1 * Time.deltaTime;
            if (currentTime < 0)
                timesUp = true;

            if (Input.GetKeyDown(KeyCode.A)) {
                P_Choice1[playerInput].renderer.material = P_Rock;
                playerChoiceData[playerInput] = 1;
                playerInput += 1;
            }
            else if (Input.GetKeyDown(KeyCode.S)) {
                P_Choice1[playerInput].renderer.material = P_Paper;
                playerChoiceData[playerInput] = 2;
                playerInput += 1;
            }
            else if (Input.GetKeyDown(KeyCode.D)) {
                P_Choice1[playerInput].renderer.material = P_Scissors;
                playerChoiceData[playerInput] = 3;
                playerInput += 1;
            }
        }

        if (playerInput >= 3 || timesUp == true) {
            Debug.Log("Player finished choicing");
            Countdown.gameObject.SetActive(false);
            allowInput = false;
            battlePhase = BattleScenePhases._CompareChoices.ToString();
        }
    }

    void GetBattleResult() {
        //1 - Rock      2 - Paper       3 - Scissors
        //1 - Win       2 - Lose        3 - Tie
        for (int i = 0; i < 3; i++) {
            int difference = playerChoiceData[i] - enemyChoiceData[i];
            if (playerChoiceData[i] == 0) {
                R_Choice1[i].renderer.material = Loss;
                resultData[i] = 2;
            }
            else if (difference == 1 || difference == -2) {
                R_Choice1[i].renderer.material = Win;
                resultData[i] = 1;
            }
            else if (difference == 0) {
                R_Choice1[i].renderer.material = Tie;
                resultData[i] = 3;
            }
            else if (difference == -1 || difference == 2) {
                R_Choice1[i].renderer.material = Loss;
                resultData[i] = 2;
            }

            if (i == 2)
                battlePhase = BattleScenePhases._EnterBattleScene.ToString();
        }

        Debug.Log("Finished publishing result");
    }

    int ComputeDamage(int _type) {
        int damage = 0;
        if(_type == 1)
            damage = (int)((enemyClone.GetAttack() * 2) - playerClone.GetDefense());
        if(_type == 2)
            damage = (int)((playerClone.GetAttack() * 2) - enemyClone.GetDefense());
        return damage;
    }
    IEnumerator LoopGap(int _currentCounter) {
        yield return new WaitForSeconds(1.0f);
        StartBattleAnimations(_currentCounter + 1);
    }
    void StartBattleAnimations(int _counter) {

        if (resultData[_counter] == 1) {
            playerClone.addDamage = true;
            playerDamage.gameObject.SetActive(true);
            enemyDamage.gameObject.SetActive(false);
            enemyDamage.text = ComputeDamage(1).ToString();
            resultData[_counter] = 0;
        }
        else if (resultData[_counter] == 2) {
            enemyClone.addDamage = true;
            playerDamage.gameObject.SetActive(false);
            enemyDamage.gameObject.SetActive(true);
            playerDamage.text = ComputeDamage(2).ToString();
            resultData[_counter] = 0;
        }
        else if (resultData[_counter] == 3) {
            enemyClone.addDamage = true;
            playerClone.addDamage = true;
            playerDamage.gameObject.SetActive(true);
            enemyDamage.gameObject.SetActive(true);
            playerDamage.text = ComputeDamage(2).ToString();
            enemyDamage.text = ComputeDamage(1).ToString();
            resultData[_counter] = 0;
        }

        if (_counter == 2) {
            Debug.Log("HAHAHAHAHAHAHAH");
            stopLocators = false;
            startBattleScene = true;
            battlePhase = BattleScenePhases._LocatorMove.ToString();
            ResetMaterials();
        }
        else if (_counter < 2)
            StartCoroutine(LoopGap(_counter));
    }
    #endregion

    // Update is called once per frame
	void Update() {
        ShowBothStats();
        CheckEnemyNumber();
        CheckBattleScenePhases();

        enemyClone = Enemy[SelectedEnemyID].GetComponent<WeakEnemySampleScript>();
        Debug.Log(battlePhase);

        if (startBattleScene)
            battlePhase = BattleScenePhases._LocatorMove.ToString();

        if (playerClone.addDamage == true)
            DamageEnemy(playerClone.GetAttack());
        if (enemyClone.addDamage == true)
            DamagePlayer(enemyClone.GetAttack());

        if (Input.GetKeyDown(KeyCode.KeypadEnter))
            Application.LoadLevel(0);
 	}

    void CheckBattleScenePhases() {
        if (battlePhase == "_LocatorMove") {
            Debug.Log("Moving Locators");
            playerDamage.gameObject.SetActive(false);
            enemyDamage.gameObject.SetActive(false);
            MoveLocators();
        }
        if (battlePhase == "_ShowEnemyChoices") {
            Debug.Log("Initialize shuffle");
            ShuffleEnemySet();
        }
        if (battlePhase == "_InputPlayerChoices") {
            Debug.Log("Preparing for player inputs");
            stopShuffling = false;
            Countdown.gameObject.SetActive(true);
            Countdown.GetComponent<TextMesh>().text = ((int)currentTime).ToString();
            GetPlayerChoices();
        }
        if (battlePhase == "_CompareChoices") {
            playerInput = 0;
            GetBattleResult();
        }
        if (battlePhase == "_EnterBattleScene") {
            Debug.Log("Commencing battle animations");
            StartBattleAnimations(0);
            //StartCoroutine(ShowBattleSequence());
        }
    }
}

public enum BattleScenePhases {
    _Blank,
    _LocatorMove,
    _ShowEnemyChoices,
    _InputPlayerChoices,
    _CompareChoices,
    _EnterBattleScene
}