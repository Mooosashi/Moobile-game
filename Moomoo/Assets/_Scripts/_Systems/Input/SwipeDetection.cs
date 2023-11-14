using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SwipeDetection : MonoBehaviour
{
    private GameManager gameManager;
    private InputManager inputManager;

    [Header("Parameters")]
    [SerializeField] private float rotationSpeed = 1f;

    private bool shouldRotate;


    private void Awake()
    {
        gameManager = GameManager.instance;
        inputManager = GameManager.instance.inputManager;
    }

    private void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    private void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 position)
    {
        StartCoroutine(Rotate());
    }

    private void SwipeEnd(Vector2 position)
    {
        shouldRotate = false;
    }

    private IEnumerator Rotate()
    {
        shouldRotate = true;
        while (shouldRotate && gameManager.playerCharacter.CurrentPlayerState == PlayerState.still)
        {
            inputManager.swipeAxis *= rotationSpeed;
            // camera1.transform.Rotate(Vector3.up, inputManager.swipeAxis.x, Space.World);
            // cmFreeLook.m_XAxis.m_InputAxisValue += inputManager.swipeAxis.x;
            yield return null;
        }
    }

}
