using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class skyManipulator : MonoBehaviour
{
    public ApplicationController pointcloudcontroller;
    public PointCloudController overridePLV;
    public Volume manipulator;
    public float rot;
    [Range(-10f, 0f)]
    public float ex;
    [Range(0, 1)]
    public float PLVValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (rot < 360)
            rot += Time.deltaTime * 5;
        else
            rot = 0;
        SetHdriRotation(rot);

        //let exposure be effected by PLV, from -10 to 0
        if (overridePLV.overRide)
        {
            float temp = Mathf.Abs(pointcloudcontroller.PLVValue - 1);
            ex = (0 - (-10)) / (1 - 0) * (temp - 1) + 0;
            SetHdriExposure(ex);
        }
        else
        {
            ex = (0 - (-10)) / (1 - 0) * (PLVValue - 1) + 0;
            SetHdriExposure(ex);
        }

        
    }

    void SetHdriRotation(float rot)
    {
        HDRISky sky;
        manipulator.profile.TryGet(out sky);
        sky.rotation.value = rot;
    }

    void SetHdriExposure(float ex)
    {
        HDRISky sky;
        manipulator.profile.TryGet(out sky);
        sky.exposure.value = ex;
    }

    public void ReceivePLV(float plv)
    {
        //print("PLV received: " + plv);
        PLVValue = plv;
    }
}
