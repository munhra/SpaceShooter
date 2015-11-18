using UnityEngine;
using System.Collections;

public class RandomRotate : MonoBehaviour {

    public float tumble;

    void Start()
    {
        Rigidbody rg = GetComponent<Rigidbody>();
        rg.angularVelocity = Random.insideUnitSphere * tumble;
    }
}
