using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyPoint : MonoBehaviour
{
    public float ep = 15f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().RegenEnergy(ep);
            Destroy(gameObject);
        }
    }
}
