using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Code.Models
{
    public class CharacterInteractionBranch : CharacterInteraction
    {
        [Inject]
        private GameState _gameState { get; set; }
        public string branchingVariableName;
        public int[] branchingVariableValues;
        public CharacterInteraction[] branchingCharacterInteractions;
        public override void Interact()
        {
            EndInteraction();
        }

        private CharacterInteraction GetNextInteractionFromBranch()
        {
            int branchingValue = _gameState.GetOrAddGameVariable(branchingVariableName);

            if (branchingVariableValues.Length != branchingCharacterInteractions.Length)
                throw new Exception("number of values should match number of interactions you can branch with.");

            for (int index = 0; index < branchingVariableValues.Length; index++)
            {
                if (branchingVariableValues[index] == branchingValue)
                {
                    return branchingCharacterInteractions[index];
                }
            }

            return null;
        }

        public override CharacterInteraction NextInteraction 
        { 
            get
            { 
                return GetNextInteractionFromBranch(); 
            } 
            
            set => base.NextInteraction = value; 
        }
    }
}
