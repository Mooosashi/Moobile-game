using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { freeMovement, interacting, pause, dead }
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private GameObject UIJoystick;
    [SerializeField] private GameObject closeUpCamera;

    private GameState currentState;

    public GameState CurrentState {
        get => currentState;
        
        private set {
            currentState = value;

            switch (value)
            {
                case GameState.freeMovement:
                    UIJoystick.SetActive(true);
                    closeUpCamera.SetActive(false);
                    break;

                case GameState.interacting:
                    UIJoystick.SetActive(false);
                    closeUpCamera.SetActive(true);
                    break;

                case GameState.pause:
                    Debug.Log("Pause state");
                    break;

                default:
                    break;
            }
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public void SetState(GameState state)
    {
        CurrentState = state;
    }
}
