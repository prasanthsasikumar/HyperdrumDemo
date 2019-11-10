using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCloudController : MonoBehaviour
{
    Color _color = new Color(1,1,1,1);
    [Range(0, 1)]
    public float PLVValue;
    public bool overRide = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        _color = new Color(PLVValue, PLVValue, PLVValue,1);
        if (overRide)
        {
            PLVValue = this.GetComponent<midiTest>().decValueFloat;
        }
        gameObject.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", _color);
       // print(_color);
    }
    public void ReceivePLV(float plv)
    {
        print("PLV received: " + plv);
        PLVValue = plv;
    }
}
