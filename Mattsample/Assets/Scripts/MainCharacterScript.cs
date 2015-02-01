//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class MainCharacterScript : MonoBehaviour {

    Luxon._CharacterStats _myCharacter = new Luxon._CharacterStats();

    private Vector3 startingPos;    
    public Transform EnemyPos;

    private Vector3 vel;

    private int level;
    private int currentExp;
    private int totalExp;
    private int classType;

    private int currentHP;
    private int maxHP;
    private int Attack;
    private int Defense;
    private int Speed;
    private int Intuition;

    private bool startAttackAnim;
    public bool addDamage;
    public bool playerAttack;

    private string typeName;

    private string[] type = {"Commando", "Sentinel", "Ravager"};

    void Start() {
        startingPos = this.gameObject.transform.position;
        vel = Vector3.zero;
        startAttackAnim = false;
        addDamage = false;
        playerAttack = false;

        _myCharacter.SetType(1);
        classType = 0;
    }

    //this function is called from MainMenuScript and will set the player's class type
    public void SetClassType(int _class) { 
        classType = _class; 
    }

    //Add damage to player
    public void AddDamage(int _damage) {
        this._myCharacter.ApplyDamage(_damage);
    }

    #region Getter
    public int GetLevel() { return level; }
    public int GetCurrentExp() { return currentExp; }
    public int GetTotalExp() { return totalExp; }
    public int GetCurrentHP() { return currentHP; }
    public int GetMaxHP() { return maxHP; }
    public int GetAttack() { return Attack; }
    public int GetDefense() { return Defense; }
    public int GetSpeed() { return Speed; }
    public int GetIntuition() { return Intuition; }
    public string GetClass() { return typeName; }
    public bool CheckAttackLog() { return addDamage; }
    #endregion

    //sets the local variables based on the values on _CharacterStats class
    void SetCharacterStats() {
        this.level = this._myCharacter.GetCurrentLevel();
        this.currentExp = this._myCharacter.GetCurrentExp();
        this.totalExp = this._myCharacter.GetTotalExpCurrentLevel();

        this.maxHP = this._myCharacter.GetMaxHP();
        this.currentHP = this._myCharacter.GetCurrentHP();
        this.Attack = this._myCharacter.GetAttack();
        this.Defense = this._myCharacter.GetDefense();
        this.Speed = this._myCharacter.GetSpeed();
        this.Intuition = this._myCharacter.GetIntuition();
    }
    
    //checks the chosen class type and apply the default stats
    void CheckClassType() {
        if (classType == 1) {
            this.typeName = type[0];
            this._myCharacter.SetType(1);
            this._myCharacter.SetCommandoBaseStat();
            classType = 0;
        }
        else if (classType == 2) {
            this.typeName = type[1];
            this._myCharacter.SetType(2);
            this._myCharacter.SetSentinelBaseStat();
            classType = 0;
        }
        else if (classType == 3) {
            this.typeName = type[2];
            this._myCharacter.SetType(3);
            this._myCharacter.SetRavagerBaseStat();
            classType = 0;
        }
    }

    //DEVELOPER'S CHEAT
    void EnableCheat() {
        if (Input.GetKey(KeyCode.Space)) {
            this._myCharacter.AddExp(1);
        }
        if (Input.GetKey(KeyCode.Backslash)) {
            this._myCharacter.AddExp(100);
        }
    }

    //simple animation when player attacks enemy
    IEnumerator StartAttackSequence() {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, EnemyPos.transform.position, ref vel, 0.75f);
        yield return new WaitForSeconds(0.25f);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, startingPos, ref vel, 0.75f);
        yield return new WaitForSeconds(0.25f);
        startAttackAnim = false;
    }

	// Update is called once per frame
	void Update () {

        //run these functions to check for players stats and cheat
        _myCharacter.LevelChecker();
        SetCharacterStats();
        CheckClassType();
        EnableCheat();

        if (Input.GetKeyUp(KeyCode.K)) {
            startAttackAnim = true;
            addDamage = true;
        }

        if(addDamage)
            startAttackAnim = true;

        if (startAttackAnim == true) {
            StartCoroutine(StartAttackSequence());
        }
	}
}
