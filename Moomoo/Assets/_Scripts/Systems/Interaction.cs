using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] [Range(0, 10000)] private float raycastLength = 500f;

    [Header("Debug")]
    [SerializeField] private float raycastDuration = 20f;


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, raycastLength))
            {
                Debug.DrawRay(ray.origin, ray.direction * raycastLength, Color.red, raycastDuration);
                if (hit.transform.gameObject.GetComponent<IClickable>() != null && hit.transform.gameObject.GetComponent<Interactable>().currentState == InteractableState.interactable && GameManager.instance.CurrentState == GameState.freeMovement)
                {
                    IClickable clickableInterface;
                    clickableInterface = hit.transform.GetComponent<IClickable>();
                    clickableInterface.OnClick();
                }
                else if (GameManager.instance.CurrentState == GameState.interacting)
                {
                    
                    GameManager.instance.SetState(GameState.freeMovement);
                    
                }
            }
        }
    }
}
