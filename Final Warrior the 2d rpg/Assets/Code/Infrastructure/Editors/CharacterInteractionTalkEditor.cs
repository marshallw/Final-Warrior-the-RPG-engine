using Assets.Code.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Assets.Code.Infrastructure.Editors
{
    [CustomEditor(typeof(CharacterInteractionTalk))]
    public class CharacterInteractionTalkEditor: Editor
    { 
        public ReorderableList DialogueList;
        public CharacterInteractionTalk characterInteraction => target as CharacterInteractionTalk;
        public bool extendNeighbor;
        public virtual void OnEnable()
        {
            DialogueList = new ReorderableList(characterInteraction._dialogueItems, typeof(DialogueItem), true, true, true, true);
            DialogueList.drawHeaderCallback = OnDrawHeader;
            //DialogueList.drawElementCallback = OnDrawElement;
            //DialogueList.elementHeightCallback = GetElementHeight;
            //DialogueList.onChangedCallback = ListUpdated;
            //DialogueList.onAddCallback = OnAddElement;
        }

        public void OnDrawHeader(Rect rect)
        {
            GUI.Label(rect, "Dialogue");

            Rect toggleRect = new Rect(rect.xMax - rect.height, rect.y, rect.height, rect.height);
            Rect toggleLabelRect = new Rect(rect.x, rect.y, rect.width - toggleRect.width - 5f, rect.height);

            extendNeighbor = EditorGUI.Toggle(toggleRect, extendNeighbor);
            EditorGUI.LabelField(toggleLabelRect, "Extend Neighbor", new GUIStyle()
            {
                alignment = TextAnchor.MiddleRight,
                fontStyle = FontStyle.Bold,
                fontSize = 10,
            });
        }

    }
}
