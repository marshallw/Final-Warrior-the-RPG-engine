using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models
{
    public enum PossibleGameStates { Map, Dialogue }
    public class GameState
    {
        public PossibleGameStates currentState;

        public GameState()
        {
            currentState = PossibleGameStates.Map;
        }

        public GameState(PossibleGameStates startingState)
        {
            currentState = startingState;
        }
    }
}
