using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// A reference to RigidBody component
    /// </summary>
    private Rigidbody rb;

    [Tooltip("The speed which the ball/player will dodge")]
    [Range(0, 5)]
    public float dodgeSpeed = 1.0f;

    [Tooltip("The speed which the ball/player will move forward")]
    [Range(0, 15)]
    public float speed = 5.0f;

    /// <summary>
    /// This variable is used to add moviment to the player by its position
    /// </summary>
    private Vector3 playerPosition;

    /// <summary>
    /// Variable to control if player can use dash
    /// </summary>
    public static bool dashControl = true;

    /// <summary>
    /// Countdown time until the player will be able to use dash again
    /// </summary>
    private int dashTimerCountdown = 5;

    /// <summary>
    /// Controls if the dash power were used or not
    /// </summary>
    private bool powerWereUsed = false;

    // Start is called before the first frame update
    void Start()
    {
        // Gets the access to the RigidBody associated to this GO (Game Object)
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        // Receives the GO Player position
        playerPosition = transform.position;

        // Add speed (moves the player) after a time (deltaTime .: provides time between the current and previous frame)
        playerPosition.z += speed * Time.deltaTime;

        // Check if the player is inside the road (Floor from the Basic Tile)
        // It avoid the moviment to outside the road, given the transform.position
        if (playerPosition.x >= -3 && playerPosition.x <= 3)
        {
            // Moves the player side to side, based on the arrow key pressed
            playerPosition.x += (dodgeSpeed / 20) * Input.GetAxis("Horizontal");
        }

        // Atributtes the current position to Player
        transform.position = playerPosition;

        // Verify if the space bar is been pressed to call the Dash Power method
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!IsInvoking("DashPower") && dashControl && !powerWereUsed)
            {
                Invoke("DashPower", 0f);
                CancelInvoke("DashEnd");
            }
        }
        // When released, the space bar should cancel the Dash Power
        if (Input.GetKeyUp(KeyCode.Space) && !dashControl && powerWereUsed)
        {
            CancelInvoke("DashPower");
            Invoke("DashEnd", 0f);
        }
    }

    // Dash Power method
    void DashPower()
    {
        speed = speed * 15;
        StartCoroutine(Countdown(dashTimerCountdown));
        powerWereUsed = true;
    }

    // Dash Power cancel method, setting the speed to its initial value
    void DashEnd()
    {
        speed = speed / 15;
        powerWereUsed = false;
    }
    
    // Coroutine used to implement the countdown into the dash power
    private IEnumerator Countdown(int dashTimerCountdown)
    {
        dashControl = false;
        yield return new WaitForSeconds(dashTimerCountdown);
        dashControl = true;
    }

}



