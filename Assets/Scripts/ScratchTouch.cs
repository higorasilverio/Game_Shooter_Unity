using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchTouch : MonoBehaviour
{
    /// <summary>
    /// This function, as this entire class is used to enable a sound 
    /// when the Player touches the wall
    /// </summary>
    /// <param name="collision"> Gets the object that collided with the wall a.k.a. Player</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {
            // Plays a scratch sound
            collision.gameObject.GetComponent<AudioSource>().Play();

        }
    }
}
