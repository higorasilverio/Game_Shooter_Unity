using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScratchTouch : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>())
        {

            collision.gameObject.GetComponent<AudioSource>().Play();

        }
    }
}
