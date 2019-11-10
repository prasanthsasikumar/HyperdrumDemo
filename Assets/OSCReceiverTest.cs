using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCReceiverTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ReceiveData(Vector3 transform)
    {
        print("received : " + transform);
    }
}
