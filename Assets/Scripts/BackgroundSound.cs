using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSound : MonoBehaviour
{

    [Tooltip("Reference to the background sound")]
    public static BackgroundSound backgroundSound = null;

    /// <summary>
    /// Plays the background sound when awaked and does not stop
    /// </summary>
    private void Awake()
    {
        if (backgroundSound != null)
        {
            Destroy(gameObject);
        }
        else
        {
            backgroundSound = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(gameObject);
    }
}
