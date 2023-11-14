using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraSystem : MonoBehaviour
{
    public static CameraSystem instance;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineFreeLook mainFLCamera;
    [SerializeField] public CinemachineVirtualCamera closeUpVCamera;
    [SerializeField] private CinemachineTargetGroup closeUpTargetGroup;

    private GameObject currentInteractable;

    private GameManager gameManager;

    private void Awake()
    {
        #region Singleton
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        #endregion

        gameManager = GameManager.instance;

        gameManager.onInteractingState += EnableCloseUpCamera;
        gameManager.onFreeMovementState += EnableMainCamera;
    }


    public void GetPlayerCharacter(GameObject playerCharacter)
    {
        mainFLCamera.Follow = playerCharacter.transform;
        mainFLCamera.LookAt = playerCharacter.transform;
        closeUpVCamera.Follow = playerCharacter.transform;
        closeUpTargetGroup.AddMember(playerCharacter.transform, 1f, 1f);
    }

    private void EnableCloseUpCamera(GameObject interactable)
    {
        currentInteractable = interactable;
        closeUpTargetGroup.AddMember(interactable.transform, 2f, 2f);
        closeUpVCamera.gameObject.SetActive(true);
    }

    private void EnableMainCamera()
    {
        closeUpVCamera.gameObject.SetActive(false);

        if (currentInteractable != null)
        {
            closeUpTargetGroup.RemoveMember(currentInteractable.transform);
            currentInteractable.gameObject.GetComponent<Interactable>().SetState(InteractableState.interactable);
        }
    }


    private void OnDestroy()
    {
        gameManager.onInteractingState -= EnableCloseUpCamera;
        gameManager.onFreeMovementState -= EnableMainCamera;
    }
}
