using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] public int spawnID;
    [SerializeField] public Transform spawnPoint;

    private GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.instance;

        // gameManager.sceneTriggers.Add(this);

        gameManager.onInitializeState += SetSceneTrigger;
    }

    private void SetSceneTrigger()
    {
        gameManager.sceneTriggers.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.TransitionOut(sceneName);
            gameManager.sceneSpawnID = spawnID;
            gameManager.SetGameState(GameState.transition);
        }
    }

    private void OnDestroy()
    {
        gameManager.onInitializeState -= SetSceneTrigger;
    }
}
