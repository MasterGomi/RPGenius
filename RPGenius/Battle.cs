using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    class Battle
    {
        private List<Entity> _turnOrder = new List<Entity>();
        //
        public int EntityCount { get; set; }
        public int PlayerCount { get; set; }
        public int EnemyCount { get; set; }
        public Dictionary<Entity, int> EntityReg = new Dictionary<Entity, int>();   // to represent static entities attached to a changing number (turn order number)
        public List<Player> Players = new List<Player>();   // a collection of all player entities
        public List<Enemy> Enemies = new List<Enemy>();     // a collection of all enemy entities
        //
        public Battle(/*battle parameters?*/) { }
        /// <summary>
        /// Adds specified entity (player or enemy) into the battle
        /// </summary>
        /// <param name="e">The entity to be added</param>
        public void AddEntity(Entity e)
        {
            EntityCount++;
            if(e.GetType() == typeof(Enemy))
            {
                EnemyCount++;
                Enemies.Add(e as Enemy);
            }
            else
            {
                PlayerCount++;
                Players.Add(e as Player);
            }
            EntityReg.Add(e, e.TurnOrder);
        }
        /// <summary>
        /// Removes an entity (player or enemy) from the battle (typically when said enemy is killed)
        /// </summary>
        /// <param name="e">The entity to be removed</param>
        public void RemoveEntity(Entity e)
        {
            EntityCount--;
            if (e.GetType() == typeof(Enemy))
            {
                EnemyCount--;
                Enemies.Remove(e as Enemy);
            }
            else
            {
                PlayerCount--;
                Players.Remove(e as Player);
            }
            EntityReg.Remove(e);
        }
        public void Start()
        {
            Console.WriteLine("You have encountered");
            foreach(Enemy e in Enemies)
            {
                Console.WriteLine("\t{0}", e.Name);
            }
            Console.WriteLine("");
            Console.WriteLine("Battle commences!");
            while(PlayerCount > 0 && EnemyCount > 0)    //so long as there are any entities left in the battle...
            {
                DetermineTurnOrder();
                Turn();         //... loop through their turns
            }
        }
        /// <summary>
        /// Loops through the Entity register to determine their position in the turn order and assigns entities in thier order to _turnOrder
        /// </summary>
        private void DetermineTurnOrder()
        {
            _turnOrder.Clear();
            for(int i = 1; i < 10; i++)
            {
                foreach(KeyValuePair<Entity, int> pair in EntityReg)
                {
                    if(pair.Value == i) { _turnOrder.Add(pair.Key); }
                }
            }
        }
        /// <summary>
        /// Runs a 'Turn'. (one loop through all entities' turns)
        /// </summary>
        private void Turn()
        {
            foreach(Entity e in _turnOrder)
            {
                if(e.HP == 0) { continue; }
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("It is {0}'s turn", e.Name);
                Console.WriteLine("");
                e.ExecuteTurn(this);
                Thread.Sleep(3000);
                if(PlayerCount <= 0 || EnemyCount <= 0) { break; }  //stop looping turns if all of one side is defeated
            }
        }
    }
}
