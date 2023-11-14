using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterMovement : MonoBehaviour
{
    [Header("References")]
    private GameManager gameManager;
    [SerializeField] private PlayerCharacter playerCharacter;
    [SerializeField] private CharacterController characterController;
    
    [Header("Parameters")]
    [SerializeField] private float speed = 3f;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private float gravityForce = 10f;
    // [SerializeField] private float gravityRayLength = 1f;
    // [SerializeField] private LayerMask environmentMask;
    private float turnSmoothVelocity;

    private Joystick joystick;
    Vector3 moveDirection;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        playerCharacter.SetPlayerState(PlayerState.still);

        joystick = gameManager.inputManager.joystick.GetComponent<Joystick>();
    }

    void Update()
    {
        if (gameManager.CurrentGameState == GameState.freeMovement)
            Move();
    }

    private void Move()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            playerCharacter.SetPlayerState(PlayerState.moving);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + gameManager.mainCamera.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        else
        {
            playerCharacter.SetPlayerState(PlayerState.still);

            moveDirection.x = 0f;
            moveDirection.z = 0f;
        }

        // INTENTO DE PROGRAMAR LIMPIA LA GRAVEDAD

        //RaycastHit hit;
        //Vector3 rayOrigin = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        //Debug.DrawRay(rayOrigin, Vector3.down * gravityRayLength, Color.green);
        
        //if (!Physics.Raycast(rayOrigin, Vector3.down, out hit, gravityRayLength, environmentMask))
        //{
            
        //}

        moveDirection.y = -gravityForce * Time.deltaTime;

        characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
    }
}
