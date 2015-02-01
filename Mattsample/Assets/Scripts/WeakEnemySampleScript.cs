//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class WeakEnemySampleScript : MonoBehaviour {

    Luxon._EnemyAIStats myEnemy = new Luxon._EnemyAIStats();

    public GameObject CombatManager;
    private CombatManagerScript CombatScript;

    public int EnemyType; //0 = weak, 1 = average, 2 = tough enemy

    private Vector3 startingPos;
    public Transform PlayerPos;

    private Vector3 vel;

    private int maxHP;
    private int currentHP;
    private float Attack;
    private float Defense;
    private float Speed;
    private int expReward;

    private bool startAttackAnim;
    public bool addDamage;
    public bool enemyAttack;

	// Use this for initialization
	void Start () {
        CombatScript = CombatManager.GetComponent<CombatManagerScript>();
        int temp = EnemyType + 1;
        this.myEnemy.SelectEnemyType(temp);
        startingPos = this.transform.position;

        startAttackAnim = false;
        addDamage = false;
        enemyAttack = false;
        vel = Vector3.zero;
	}

    void OnMouseDown() {
        CombatScript.SelectedEnemyID = this.EnemyType;
        Debug.Log("EnemyType: " + this.EnemyType);
    }

    //add damage to enemy
    public void AddDamage(int _damage) {
        this.myEnemy.ApplyDamage(_damage);
    }

    #region Getter
    public int GetCurrentHP() { return currentHP; }
    public int GetMaxHP() { return maxHP; }
    public int GetAttack() { return (int)Attack; }
    public int GetDefense() { return (int)Defense; }
    public int GetSpeed() { return (int)Speed; }
    public int GetExpReward() { return (int)expReward; }
    #endregion

    //sets the local variables based on the values on _EnemyAIStats class
    void CheckStats() {
        this.maxHP = this.myEnemy.GetMaxHP();
        this.currentHP = this.myEnemy.GetCurrentHP();
        this.Attack = this.myEnemy.GetAttack();
        this.Defense = this.myEnemy.GetDefense();
        this.Speed = this.myEnemy.GetSpeed();
        this.expReward = this.myEnemy.GetExpReward();

        if (this.currentHP <= 0) {
            this.currentHP = 0;
            Debug.Log("Enemy has died.");
            Destroy(this.gameObject);
        }
    }

    //simple enemy attack animation
    IEnumerator StartAttackSequence() {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, PlayerPos.transform.position, ref vel, 0.75f);
        yield return new WaitForSeconds(0.25f);
        this.transform.position = Vector3.SmoothDamp(this.transform.position, startingPos, ref vel, 0.75f);
        yield return new WaitForSeconds(0.25f);
        startAttackAnim = false;
    }

	// Update is called once per frame
	void Update () {
        CheckStats();

        //if (Input.GetKeyUp(KeyCode.L)) {
        //    startAttackAnim = true;
        //    addDamage = true;
        //}

        if (addDamage)
            startAttackAnim = true;

        if (startAttackAnim == true) {
            StartCoroutine(StartAttackSequence());
        }

        if (Input.GetKeyUp(KeyCode.W)) {
            this.myEnemy.ApplyDamage(68);
        }
	}
}
