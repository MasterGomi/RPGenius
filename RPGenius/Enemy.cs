﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    class Enemy : Entity
    {
        //private Player _lastAttackedBy; //consider using something like this to increase complexity of enemy target selection
        public Enemy(string name, int turnOrder, int hp, int atk, int def, int mp = 0, int mag = 0, int spr = 0) : base(name, turnOrder, hp, atk, def, mp, mag, spr) { }
        //
        public override void ExecuteTurn(Battle battle)
        {
            Thread.Sleep(2000);
            IsDefending = false;
            Random rnd = new Random();
            int attackChance = HP / BaseHp * 100;     //Chance of attack is higher if health is higher. (represented as an integer percentage)
            if(attackChance > 90) { attackChance = 90; }    //This statement ensures the chance of defense is always at least 10%
            else { if(attackChance > 70) { attackChance = 80; } }   // This statement is a fun flavour statment, this means that when the enemy is between 70% and 90% of their health, they'll have an 80% chance of attacking
            if(attackChance < 50) { attackChance = 40; }    //this statement means that as soon as the enemy drops below 50% health, they will always have only a 40% chance of attacking
            int choice = rnd.Next(1, 100);     //Generates a random integer between 1 and 100 inclusive
            int target;
            target = rnd.Next(1, battle.PlayerCount);   // randomly chooses a player to attack, each with equal likelyhood  -> maybe make it more inclined to attack whoever hit it last, or something even more complicated
            if(choice <= attackChance) { Attack(battle.Players[target-1], battle); } //If the random number isn't high enough to trump the chance of attacking, the enemy will attack
            else
            {
                IsDefending = true;
                Console.WriteLine("> {0} defends", Name);
                Console.WriteLine("");
            }
        }
    }
}