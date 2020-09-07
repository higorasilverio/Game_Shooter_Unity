using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinBehaviour : MonoBehaviour
{
    [Tooltip("Particle System of explosion")]
    public GameObject explosion;

    [Tooltip("Variable that counst the number of coins collected by the player")]
    public static int coinsCatch = 0;

    /// <summary>
    /// Function to be used when the coins is hit
    /// </summary>
    public void CoinTouched()
    {

        coinsCatch++;

        PlayerBehaviour.controlSpeedByCoin++;

        // Verify if the explosion particle system were attached to the Coin
        if (explosion != null)
        {
            // Instatite the explosion and, after a second, destroy it
            var particles = Instantiate(explosion, transform.position, Quaternion.identity);

            Destroy(particles, 1.0f);
#pragma warning disable
            if (explosion.active)
            {
                explosion.GetComponent<AudioSource>().Play();
            }
#pragma warning restore

        }
        // Actually destroy the Coin
        Destroy(gameObject);

    }

}
