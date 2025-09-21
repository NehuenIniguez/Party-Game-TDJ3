using System.Collections.Generic;
using UnityEngine;

public class AparicionOrbes : MonoBehaviour
{
    public List<GameObject> orbes;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            foreach (GameObject orb in orbes)
            {
                orb.GetComponent<Renderer>().enabled = true;
                orb.GetComponent<Collider2D>().enabled = true;
            }
        }
    }
}
