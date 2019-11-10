using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachToCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
