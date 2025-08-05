using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticForce : MonoBehaviour
{
    public float magneticRange = 10f;

    [Header("Magnetic Force Settings")]
    public float attractionForce = 15f;
    public float repulsionForce = 5f;

    private MagneticPlayer player;

    void Start()
    {
        player = GetComponent<MagneticPlayer>();
    }

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, magneticRange);
        foreach (var col in colliders)
        {
            if (col.CompareTag("MagneticObject"))
            {
                MagneticObject mo = col.GetComponent<MagneticObject>();
                if (mo != null)
                {
                    Vector3 direction = (col.transform.position - transform.position).normalized;
                    float distance = Vector3.Distance(transform.position, col.transform.position);
                    if (distance < 0.1f) continue;

                    bool isSamePolarity = mo.objectPolarity == player.currentPolarity;
                    float force = isSamePolarity ? repulsionForce : attractionForce;
                    Vector3 forceDir = isSamePolarity ? -direction : direction;

                    // Optional: falloff with distance
                    float distanceFactor = 1f / distance;

                    // Move the player using CharacterController
                    GetComponent<CharacterController>().Move(forceDir * force * distanceFactor * Time.deltaTime);
                }
            }
        }
    }
}

