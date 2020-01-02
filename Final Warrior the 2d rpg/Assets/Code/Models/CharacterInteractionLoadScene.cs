using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class CharacterInteractionLoadScene : CharacterInteraction
{
    [Inject]
    public ZenjectSceneLoader SceneLoader { get; private set; }
    [Inject]
    public Player player { get; private set; }
    public string SceneName;

    public override void Interact()
    {
        if (SceneManager.GetSceneByName(SceneName).IsValid())
        {
            UnloadScene();
            //SceneManager.LoadScene(SceneName);
            SceneLoader.LoadSceneAsync(SceneName);
        }
    }

    private void LoadScene()
    {
        SceneLoader.LoadSceneAsync(SceneName);
    }

    private void UnloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.UnloadSceneAsync(currentScene.name);
    }
}