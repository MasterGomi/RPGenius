using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RPGenius
{
    /// <summary>
    /// The class that runs the battle and contains a record of all participants
    /// </summary>
    class Battle
    {
        private List<Entity> _turnOrder = new List<Entity>();
        //
        public int EntityCount { get { return Players.Count + Enemies.Count; } }
        public int PlayerCount { get { return Players.Count; } }
        public int EnemyCount { get { return Enemies.Count; } }
        public Dictionary<Entity, int> EntityReg = new Dictionary<Entity, int>();   // to represent static entities attached to a changing number (turn order number)
        public List<Player> Players = new List<Player>();   // a collection of all player entities
        public List<Enemy> Enemies = new List<Enemy>();     // a collection of all enemy entities
        public List<Entity> RemoveQueue { get; }        //a collection of enemies to be removed from battle at the end of a round
            = new List<Entity>();
        //
        public Battle(/*battle parameters?*/) { }
        /// <summary>
        /// Adds specified entity (player or enemy) into the battle
        /// </summary>
        /// <param name="e">The entity to be added</param>
        public void AddEntity(Entity e)
        {
            if (e is Enemy) { Enemies.Add(e as Enemy); }
            else { Players.Add(e as Player); }
            EntityReg.Add(e, e.TurnOrder);
        }
        /// <summary>
        /// Removes an entity (player or enemy) from the battle (typically when said enemy is killed)
        /// </summary>
        /// <param name="e">The entity to be removed</param>
        public void RemoveEntity(Entity e)
        {
            if (e is Enemy) { Enemies.Remove(e as Enemy); }
            else { Players.Remove(e as Player); }
            EntityReg.Remove(e);
        }
        /// <summary>
        /// Starts the battle
        /// </summary>
        public void Start()
        {
            Console.WriteLine("You have encountered:");
            foreach(Enemy e in Enemies)
            {
                Console.WriteLine("\t{0}", e.Name);
            }
            Thread.Sleep(1500);
            Console.WriteLine("");
            Console.WriteLine("Battle commences!");
            Thread.Sleep(1000);
            while(PlayerCount > 0 && EnemyCount > 0)    //so long as there are any entities left in the battle...
            {
                DetermineTurnOrder();
                Turn();         //... loop through their turns
                foreach(Entity e in RemoveQueue)
                {
                    RemoveEntity(e);
                }
                RemoveQueue.Clear();
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
                Thread.Sleep(2500);
                if(PlayerCount <= 0 || EnemyCount <= 0) { break; }  //stop looping turns if all of one side is defeated
            }
        }
        /// <summary>
        /// Removes and dead entities
        /// </summary>
        /// <param name="deaths">An array of dead entities</param>
        public void HandleDeaths(Entity[] deaths)
        {
            foreach(Entity e in deaths)
            {
                Thread.Sleep(500);
                Console.WriteLine("\n> {0} has been defeated!", e.Name);
                RemoveEntity(e);
            }
        }
    }
}
