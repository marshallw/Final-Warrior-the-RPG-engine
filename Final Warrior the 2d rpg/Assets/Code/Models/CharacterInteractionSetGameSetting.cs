﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Code.Models
{
    public class CharacterInteractionSetGameSetting : CharacterInteraction
    {
        [Inject]
        private GameState _gameState;
        public string VariableName;
        public int VariableValue;

        public override void Interact()
        {
            _gameState.TemporaryGameSettings.SetOrAdd(VariableName, VariableValue);
        }
    }
}
