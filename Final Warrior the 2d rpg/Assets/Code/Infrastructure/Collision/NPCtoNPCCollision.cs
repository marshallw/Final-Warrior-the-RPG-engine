using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Zenject;

public class NPCtoNPCCollision
{
    private List<NPC> NPCs;

    [Inject]
    public Player player { get; private set; }

    public NPCtoNPCCollision()
    {
        NPCs = new List<NPC>();
    }

    public void AddNpctoNpcCollision(NPC npc)
    {
        foreach (NPC collisionNPC in NPCs)
        {
          //  collisionNPC.
            collisionNPC.CheckForCollision += coordinates => npc.Collision(coordinates);
            npc.CheckForCollision += coordinates => collisionNPC.Collision(coordinates);
        }

        player.CharacterEvents.OfType<CharacterEvent, CharacterInteractedEvent>().Subscribe(npc.ObservableEvents);
        NPCs.Add(npc);
    }
}
