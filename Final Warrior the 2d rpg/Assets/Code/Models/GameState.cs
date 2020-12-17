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
        public GameSettings TemporaryGameSettings { get; private set; }

        public GameState(): this(PossibleGameStates.Map)
        {
        }

        public GameState(PossibleGameStates startingState)
        {
            currentState = startingState;
            TemporaryGameSettings = new GameSettings();
        }
    }
}
