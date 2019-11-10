using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class registerCollision : MonoBehaviour
{
    public Material onHitMaterial, normalMaterial;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        this.GetComponent<Renderer>().material = onHitMaterial;
        this.GetComponent<AudioSource>().Play();
        print("Collision Entered"+ this.GetComponent<Renderer>().material.color);
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<Renderer>().material = normalMaterial;
        print("Exited");
    }
}
