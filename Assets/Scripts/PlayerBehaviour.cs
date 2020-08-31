using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
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
    private UnityEngine.Vector3 playerPosition;

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

    [Header("Swipe Attributes")]

    [Tooltip("Minimal finger distance for swipe detection")]
    public float swipeMinimalDistance = 2.0f;

    [Tooltip("Swipe movement total distance")]
    public float swipeMovement = 2.0f;

    /// <summary>
    /// Initial touch point to start the swipe movement
    /// </summary>
    private UnityEngine.Vector2 initialTouch;

    [Tooltip("Variable that tells if the player can destruy an obstacle")]
    public static bool indestructible = false;

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
            // Moves the player side to side, based on the side its reallife player press with its mouse
            if (Input.GetMouseButton(0))
            {
                TouchCoin(Input.mousePosition);
                playerPosition.x += MotionCalculation(Input.mousePosition);
            }
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

    /// <summary>
    /// Method used to determinate the movement direction
    /// </summary>
    /// <param name="screenSpaceCoord"></param>
    /// <returns></returns>
    private float MotionCalculation(UnityEngine.Vector2 screenSpaceCoord)
    {
        float xDirection = 0.0f;
        var cameraPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (cameraPosition.x < 0.5f) xDirection = -1.0f;
        else xDirection = 1.0f;
        return (dodgeSpeed / 20) * (xDirection / 1.5f);
    }

    /// <summary>
    /// Method used to verify and apply a swipe to player
    /// </summary>
    /// <param name="touch"> Parameter received by the method that validate the swipe and its direction </param>
    private void SwipeTeleport(Touch touch)
    {
        // Verify the swipe begining
        if (touch.phase == TouchPhase.Began) initialTouch = touch.position;
        // Verify the swipe ending
        else if (touch.phase == TouchPhase.Ended)
        {
            UnityEngine.Vector2 endTouch = touch.position;
            UnityEngine.Vector3 movementDirection;
            //Calculate the difference between the initial and final position of the swipe movement
            float difference = endTouch.x - initialTouch.x;
            // If the swipe distance is long enough
            if (Mathf.Abs(difference) >= swipeMinimalDistance)
            {
                // Determinates the swipe direction
                if (difference < 0) movementDirection = UnityEngine.Vector3.left;
                else movementDirection = UnityEngine.Vector3.right;
            }
            else return;
            // Using a Raycast varible to determinates some side collision
            RaycastHit hit;
            // If there is no side collision, the swipe is actually performed
            if (!rb.SweepTest(movementDirection, out hit, swipeMovement)) rb.MovePosition(rb.position + (movementDirection * swipeMovement));
        }
    }

    /// <summary>
    /// Dash Power method
    /// </summary>
    private void DashPower()
    {
        indestructible = true;
        GameObject.Find("Player").GetComponent<Renderer>().material.color = new Color(255f / 255f, 255f / 255f, 10f / 255f);
        speed = speed * 15;
        StartCoroutine(Countdown(dashTimerCountdown));
        powerWereUsed = true;
        
    }

    /// <summary>
    /// Dash Power cancel method, setting the speed to its initial value
    /// </summary>
    private void DashEnd()
    {
        indestructible = false;
        GameObject.Find("Player").GetComponent<Renderer>().material.color = new Color(10f / 255f, 10f / 255f, 200f / 255f);
        speed = speed / 15;
        powerWereUsed = false;
    }

    /// <summary>
    /// Coroutine used to implement the countdown into the dash power
    /// </summary>
    /// <param name="dashTimerCountdown"> Time between uses </param>
    /// <returns></returns>
    private IEnumerator Countdown(int dashTimerCountdown)
    {
        dashControl = false;
        yield return new WaitForSeconds(dashTimerCountdown);
        dashControl = true;
    }

    /// <summary>
    /// Method used to identify the touch into the coin object
    /// </summary>
    /// <param name="touch"> Touch occur on this frame </param>
    private static void TouchCoin(UnityEngine.Vector3 touch)
    {
        // Touch on the screent converted to a Ray variable
        Ray touchRay = Camera.main.ScreenPointToRay(touch);
        // Object where we save the information from the coin touched
        RaycastHit hit;
        if (Physics.Raycast(touchRay, out hit))
            hit.transform.SendMessage("CoinTouched", SendMessageOptions.DontRequireReceiver);
    }

}



