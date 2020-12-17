using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using UnityEngine;

namespace Assets.Code.Models
{
    [Serializable]
    public struct DialogueItem
    {
        public Sprite Portrait;
        public string Dialogue;
        public string Name;

        public DialogueItem(string name, string dialogue, Sprite portrait)
        {
            Name = name;
            Dialogue = dialogue;
            Portrait = portrait;
        }
    }
}
