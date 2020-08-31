using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Tooltip("A target to be followed by the camera object")]
    public Transform target;

    [Tooltip("Relative distance/position between camera and target")]
    public Vector3 offset = new Vector3(0, 5, -10);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (target != null)
        {
            // Change the camera position to the Player, adding offset to it
            transform.position = target.position + offset;

            // Change the camera rotation
            transform.LookAt(target);
        }

    }
}
