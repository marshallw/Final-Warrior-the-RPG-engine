using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Code.Models.Events
{
    public class CharacterInteractionWarpPlayerEvent: CharacterInteractionEvent, IEquatable<CharacterInteractionWarpPlayerEvent>
    {
        public Vector3 newCoordinates { get; private set; }
        public CharacterInteractionWarpPlayerEvent(Vector3 coordinates)
        {
            newCoordinates = coordinates;
        }

        public bool Equals(CharacterInteractionWarpPlayerEvent other)
        {
            return this.newCoordinates == other.newCoordinates;

        }
    }
}
