using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrange : MonoBehaviour
{
    public GameObject drumModule;
    public GameObject drumset1, drumset2, drumset3, drumset4;
    public GameObject right, left;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            drumset1.transform.position = left.transform.position;
            drumset4.transform.position = right.transform.position;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            drumset2.transform.position = left.transform.position;
            drumset3.transform.position = right.transform.position;
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            drumModule.transform.position = right.transform.position;
        }
    }
}
