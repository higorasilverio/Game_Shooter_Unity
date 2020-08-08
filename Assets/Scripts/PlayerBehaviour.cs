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
    [Range(0, 10)]
    public float dodgeSpeed = 5.0f;

    [Tooltip("The speed which the ball/player will move forward")]
    [Range(0, 10)]
    public float rollingSpeed = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the access to the RigidBody associated to this GO (Game Object)
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //Check which side the player wants to dodge
        var horizontalSpeed = Input.GetAxis("Horizontal") * dodgeSpeed;

        //Apply a force so the ball moves
        rb.AddForce(horizontalSpeed, 0, rollingSpeed);

    }
}
