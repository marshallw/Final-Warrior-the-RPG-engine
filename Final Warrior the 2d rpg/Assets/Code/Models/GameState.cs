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
        public Dictionary<string, int> _temporaryGameVariables { get; set; }

        public GameState()
        {
            currentState = PossibleGameStates.Map;
        }

        public GameState(PossibleGameStates startingState)
        {
            currentState = startingState;
        }

        public void AddOrSetGameVariable(string name, int value)
        {
            if (_temporaryGameVariables.ContainsKey(name))
                _temporaryGameVariables[name] = value;
            else
                _temporaryGameVariables.Add(name, value);
        }

        public int GetOrAddGameVariable(string name)
        {
            if (!_temporaryGameVariables.ContainsKey(name))
                _temporaryGameVariables.Add(name, 0);

            return _temporaryGameVariables[name];
        }
    }
}
