using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Code.Models.Events
{
    public class CharacterInteractionLoadSceneEvent : CharacterInteractionEvent, IEquatable<CharacterInteractionLoadSceneEvent>
    {
        public string SceneName { get; set; }

        public CharacterInteractionLoadSceneEvent(string sceneName)
        {
            SceneName = sceneName;
        }

        public bool Equals(CharacterInteractionLoadSceneEvent other)
        {
            return other != null && other.SceneName == SceneName;
        }
    }
}