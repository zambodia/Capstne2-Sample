//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Luxon {

    public class _EnemyAIStats : MonoBehaviour {

        //for enemy's stat
        private int maxHP;
        private int currentHP;
        private float Attack;
        private float Defense;
        private float Speed;
        private float dodgeRate;
        private float critRate;
        private int expReward;

        //constructor
        public _EnemyAIStats() {
            this.maxHP = 420;
            this.currentHP = 420;
            this.Attack = 48;
            this.Defense = 29;
            this.Speed = 35;
            this.dodgeRate = 15;
            this.critRate = 10;
            this.expReward = 106;
        }
        
        //setting the stats of enemy based on its' type
        public void SelectEnemyType(int _chosenType) {
            if (_chosenType == 1) {         //Weak Enemy
                this.maxHP = 420;
                this.currentHP = 420;
                this.Attack = 48;
                this.Defense = 29;
                this.Speed = 35;
                this.dodgeRate = 15;
                this.critRate = 10;
                this.expReward = 106;
            }
            else if (_chosenType == 2) {    //Average Enemy
                this.maxHP = 512;
                this.currentHP = 512;
                this.Attack = 57;
                this.Defense = 45;
                this.Speed = 32;
                this.dodgeRate = 15;
                this.critRate = 10;
                this.expReward = 117;
            }
            else if (_chosenType == 3) {    //Tough Enemy
                this.maxHP = 620;
                this.currentHP = 620;
                this.Attack = 69;
                this.Defense = 51;
                this.Speed = 30;
                this.dodgeRate = 15;
                this.critRate = 10;
                this.expReward = 137;
            }
        }

        #region Basic Getter
        public int GetMaxHP()  { return this.maxHP; }
        public int GetCurrentHP() { return this.currentHP; }
        public float GetAttack() { return this.Attack; }
        public float GetDefense() { return this.Defense; }
        public float GetSpeed() { return this.Speed; }
        public float GetCritRate() { return this.critRate; }
        public float GetDodgeRate() { return this.dodgeRate; }
        public int GetExpReward() { return this.expReward; }
        #endregion

        //applying damage taken by the player to enemy HP
        public int ApplyDamage(float _playerDamage) {
            int damage;
            //compute for the damage taken
            damage = (int) ((_playerDamage * 2) - this.Defense);
            //subtract damage to current HP
            return this.currentHP -= damage;
        }

    }

}
