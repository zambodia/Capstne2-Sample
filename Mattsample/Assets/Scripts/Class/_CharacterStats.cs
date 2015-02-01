//********************Project Luxon********************//
//Created by:       Matthew Fabic                      //
//Date created:     January 22, 2014                   //
//Edited by:                                           //
//Date edited:                                         //
//*****************************************************//

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Luxon
{
    public class _CharacterStats : MonoBehaviour
    {

        private int type;    //player's type

        //for player's stat
        private int maxHP;
        private int currentHP;
        private float Attack;
        private float Defense;
        private float Speed;
        private float Intuition;

        //for player's level up mechanism
        private int currentLevel;            //player's level
        private int totalExpCurrentLevel;    //player's total experience
        private int expToNextLevel;          //total needed experience to level up
        private int currentExp;              //player's current experience
        private int currentExpToNextLevel;   //current experience needed
        private int temp;                    //storage for previous totalExpCurrentLevel

        public _CharacterStats()    //Constructor
        {
            this.currentLevel = 1;
            this.currentExp = 0;
            this.currentExpToNextLevel = 105;
            this.expToNextLevel = 105;
            this.totalExpCurrentLevel = 105;
        }

        //set the class of the player
        public void SetType(int _typeNum) {
            type = _typeNum;
        }

        #region Setting the Base Stat of each type
        public void SetCommandoBaseStat() {     //base stat of Commando Class
            this.currentHP = 554;
            this.maxHP = 554;
            this.Attack = 68;
            this.Defense = 44;
            this.Speed = 35;
            this.Intuition = 25;
        }
        public void SetSentinelBaseStat() {     //base stat of Sentinel class
            this.currentHP = 577;
            this.maxHP = 577;
            this.Attack = 62;
            this.Defense = 48;
            this.Speed = 40;
            this.Intuition = 17;
        }
        public void SetRavagerBaseStat() {      //base stat of Ravager class
            this.currentHP = 531;
            this.maxHP = 531;
            this.Attack = 60;
            this.Defense = 46;
            this.Speed = 38;
            this.Intuition = 25;
        }
        #endregion

        #region Increasing Stats During Level Up
        void IncreaseCommandoStats()
        {
            this.maxHP += 55;
            this.currentHP = this.maxHP;
            this.Attack += 4.2f;
            this.Defense += 3.2f;
            this.Speed += 3;
            this.Intuition += 1.2f;
        }
        void IncreaseSentinelStats()
        {
            this.maxHP += 65;
            this.currentHP = this.maxHP;
            this.Attack += 2.9f;
            this.Defense += 4.3f;
            this.Speed += 1.5f;
            this.Intuition += 2f;
        }
        void IncreaseRavagerStats()
        {
            this.maxHP += 59;
            this.currentHP = this.maxHP;
            this.Attack += 3.5f;
            this.Defense += 2.9f;
            this.Speed += 2;
            this.Intuition += 3;
        }
        #endregion

        //applying damage taken by the enemy to player HP
        public int ApplyDamage(float _enemyDamage) {
            int damage;
            //compute for the damage taken
            damage = (int)((_enemyDamage * 2) - this.Defense);
            //subtract damage to current HP
            return this.currentHP -= damage;
        }

        //dev cheat, add exp as many as you want
        public void AddExp(int _exp) { 
            this.currentExp += _exp; 
        }

        #region Basic Getter
        public int GetMaxHP() { return this.maxHP; }
        public int GetCurrentHP() { return this.currentHP; }
        public int GetAttack() { return (int) this.Attack; }
        public int GetDefense() { return (int) this.Defense; }
        public int GetSpeed() { return (int) this.Speed; }
        public int GetIntuition() { return (int) this.Intuition; }
        public int GetCurrentLevel() { return this.currentLevel; }
        public int GetTotalExpCurrentLevel() { return this.totalExpCurrentLevel; }
        public int GetExpToLevel() { return this.expToNextLevel; }
        public int GetCurrentExp() { return this.currentExp; }
        public int GetCurrentExpToNextLevel() { return this.currentExpToNextLevel; }
        #endregion

        //this will solve for the max experience the player can get depending on his level
        private void LevelExperienceCalculator()
        {
            //storing previous value of totalExpCurrentLevel, then use it to solve the new totalExpCurrentLevel
            this.temp = totalExpCurrentLevel;
            this.totalExpCurrentLevel = (this.temp + this.currentLevel * 100 + (5 * this.currentLevel * this.currentLevel));

            //subtract current totalExpCurrentLevel by previous totalExpCurrentLevel
            this.expToNextLevel = this.totalExpCurrentLevel - this.temp;
        }

        //checks if player increase in level
        public void LevelChecker()
        {
            //solving the needed exp to level up
            this.currentExpToNextLevel = this.totalExpCurrentLevel - this.currentExp;

            //checks if current experience reach the needed experience to level up
            if (this.currentExp >= this.totalExpCurrentLevel)
            {
                //level plus 1
                this.currentLevel += 1;
                //increase stats depending on class type
                if (this.type == 1)
                    IncreaseCommandoStats();
                if (this.type == 2)
                    IncreaseSentinelStats();
                if (this.type == 3)
                    IncreaseRavagerStats();

                //solve the new needed experience before level up
                this.LevelExperienceCalculator();
            }
        }
    }
}