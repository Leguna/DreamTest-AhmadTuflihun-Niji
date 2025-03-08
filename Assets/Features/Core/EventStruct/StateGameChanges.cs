using System;
using System.Threading.Tasks;
using static LoadingModule.LoadingManager;

namespace EventStruct
{
    public struct StateGameChanges
    {
        public readonly GameState gameState;
        
        public StateGameChanges(GameState gameState)
        {
            this.gameState = gameState;
        }
    }
}