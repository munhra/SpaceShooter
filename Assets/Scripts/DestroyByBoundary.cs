﻿using UnityEngine;
using System.Collections;

public class DestroyByBoundary : MonoBehaviour {

    void Start()
    {
        Debug.Log("Boundary started");

    }


    void OnTriggerExit(Collider other)
    {
        Debug.Log("Destroy shoot");
        Destroy(other.gameObject);
    }

}
