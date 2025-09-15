using System.Collections.Generic;
using UnityEngine;

public class Desaparecer : MonoBehaviour
{
    public List<GameObject> objetos;
    public void DesactivarObjetos()
    {
        foreach (GameObject obj in objetos)
        {
            obj.GetComponent<Renderer>().enabled = false;
        }
    }
    public void ActivarObjetos()
    {
        foreach (GameObject obj in objetos)
        {
            obj.GetComponent<Renderer>().enabled = true;
        }
    }
}
