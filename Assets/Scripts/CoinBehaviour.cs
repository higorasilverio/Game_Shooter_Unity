using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    [Tooltip("Particle System of explosion")]
    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CoinTouched()
    {
        // Verify if the explosion particle system were attached to the Coin
        if (explosion != null)
        {
            // Instatite the explosion and, after a second, destroy it
            var particles = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(particles, 1.0f);
        }
        // Actually destroy the Coin
        Destroy(gameObject);
    }

}
