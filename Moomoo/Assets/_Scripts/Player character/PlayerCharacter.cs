using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public enum PlayerState { moving, still }

public class PlayerCharacter : MonoBehaviour
{
    public GameManager gameManager;
    private PlayerState currentPlayerState;

    [Header("References")]
    [SerializeField] private PlayerCharacterMovement playerCharacterMovement;


    public PlayerState CurrentPlayerState
    {
        get => currentPlayerState;
        
        private set
        {
            currentPlayerState = value;

            switch (value)
            {
                case PlayerState.still:
                    StillState();
                    break;

                case PlayerState.moving:
                    MovingState();
                    break;

                default:
                    break;
            }
        }
    }

    private void Awake()
    {
        gameManager = GameManager.instance;

        gameManager.playerCharacter = this;
        gameManager.maskDataUpdater.GetPlayerCharacter(this.gameObject.transform);
        CameraSystem.instance.GetPlayerCharacter(this.gameObject);
    }

    public void SetPlayerState(PlayerState state)
    {
        CurrentPlayerState = state;
    }

    private void MovingState()
    {
        gameManager.SetDebugPlayerStateText(PlayerState.moving);
        gameManager.inputManager.cameraRotationAndZoom.RotationAndZoomInput(false);
    }

    private void StillState()
    {
        gameManager.SetDebugPlayerStateText(PlayerState.still);
        gameManager.inputManager.cameraRotationAndZoom.RotationAndZoomInput(true);
    }
}
