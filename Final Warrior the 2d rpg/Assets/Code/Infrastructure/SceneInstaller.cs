using Assets.Code.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    public override void InstallBindings()
    {

        Container.Bind<Player>().AsSingle();
        Container.Bind<Map>().AsSingle();
        Container.Bind<NPC>().AsTransient();
        Container.Bind<Dialogue>().AsSingle();
        Container.Bind<GameState>().AsSingle();

        // Binding classes to each other
        Container.Bind<NPCtoNPCCollision>().AsSingle();
    }
}
