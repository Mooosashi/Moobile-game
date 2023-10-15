using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    private InputManager inputManager;
    private Camera cameraMain;

    [Header("Parameters")]
    [SerializeField] [Range(0, 10000)] private float raycastLength = 500f;
    [SerializeField] private LayerMask interactableMask;

    [Header("Debug")]
    [SerializeField] private float raycastDuration = 20f;


    private void Awake()
    {
        inputManager = GameManager.instance.inputManager;
        cameraMain = Camera.main;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += Interact;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= Interact;
    }


    public void Interact(Vector2 touchPosition)
    {
        if (GameManager.instance.CurrentGameState == GameState.freeMovement)
        {
            RaycastHit hit;
            Ray ray = cameraMain.ScreenPointToRay(touchPosition);

            if (Physics.Raycast(ray, out hit, raycastLength, interactableMask))
            {
                Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.red, raycastDuration);
                if (hit.transform.gameObject.GetComponent<IClickable>() != null && hit.transform.gameObject.GetComponent<Interactable>().currentState == InteractableState.interactable)
                {
                    GameManager.instance.UIManager.PulseAnimation(touchPosition);

                    IClickable clickableInterface;
                    clickableInterface = hit.transform.GetComponent<IClickable>();
                    clickableInterface.OnClick();
                }
            }
        }
    }
}
