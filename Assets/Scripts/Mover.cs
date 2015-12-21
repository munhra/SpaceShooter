using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

    public float speed;
    void Start()
    {
        Rigidbody rg = GetComponent<Rigidbody>();
        rg.velocity = transform.forward * speed;
    }
}
