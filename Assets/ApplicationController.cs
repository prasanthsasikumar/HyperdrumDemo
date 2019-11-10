using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;
using UnityEngine.SceneManagement;

public class ApplicationController : MonoBehaviour
{
    //pubilc 
    public enum ApplicationParameters
    {
        remotePlvEnable = 1,
        remotePlvDisable = 2,
        remoteBeatsEnable = 3,
        remoteBeatsDisable = 4,
        startDemo = 5,
        stopDemo = 6,
        reloadScene = 7
    }
    
    public struct ApplicationSwitches
    {
        string name;
        bool value;

        public ApplicationSwitches(String name, bool value)
        {
            this.name = name;
            this.value = value;
        }

    };

    public List<ApplicationSwitches> switches = new List<ApplicationSwitches>();
    int lastReceivedParameter;
    GameObject[] drumGameObjects,drumGameObjectsSpriteRenderer;
    [Range(0, 1)]
    public float PLVValue;
    public bool PLVOverride = false, autoGenerateCircles, startDemo, stopDemo, reloadScene;

    // Start is called before the first frame update
    void Start()
    {
        foreach(ApplicationParameters parameter in (ApplicationParameters[])Enum.GetValues(typeof(ApplicationParameters)))
        {
            //switches.Add(new ApplicationSwitches(parameter.ToString(), false));
        }
        autoGenerateCircles = GameObject.Find("beat_circle").GetComponent<DrumBeatLogic>().autoGenerate;


    }

    // Update is called once per frame
    void Update()
    {        
        if (startDemo)
            ManageApplication((int)ApplicationParameters.startDemo); startDemo = false;
        if (stopDemo)
            ManageApplication((int)ApplicationParameters.stopDemo); stopDemo = false;
        if (reloadScene)
            ManageApplication((int)ApplicationParameters.reloadScene); reloadScene = false;
        GameObject.Find("PointCloud").GetComponent<PointCloudController>().overRide = PLVOverride;
        GameObject.Find("beat_circle").GetComponent<DrumBeatLogic>().autoGenerate = autoGenerateCircles;
        if (PLVOverride)
            GameObject.Find("PointCloud").GetComponent<PointCloudController>().PLVValue = this.PLVValue;
    }

    public void ManageApplication(int receivedParameter)
    {
        if (CheckActionDoneBefore(receivedParameter))
            return;

        switch (receivedParameter)
        {
            case (int)ApplicationParameters.remotePlvEnable:
                PLVOverride = true; 
                break;
            case (int)ApplicationParameters.remotePlvDisable:
                PLVOverride = false;
                break;
            case (int)ApplicationParameters.remoteBeatsEnable:
                GameObject.Find("beat_circle").GetComponent<DrumBeatLogic>().autoGenerate = false;
                break;
            case (int)ApplicationParameters.remoteBeatsDisable:
                GameObject.Find("beat_circle").GetComponent<DrumBeatLogic>().autoGenerate = true;
                break;
            case (int)ApplicationParameters.startDemo:
                SwitchMeshRenderer(true);
                break;
            case (int)ApplicationParameters.stopDemo:
                SwitchMeshRenderer(false);
                break;
            case (int)ApplicationParameters.reloadScene:
                SceneManager.UnloadSceneAsync("Combined");
                SceneManager.LoadScene("Combined");
                break;
        }
        lastReceivedParameter = receivedParameter;
    }

    public bool CheckActionDoneBefore(int newParamneter)
    {
        if(lastReceivedParameter == newParamneter)
        return true;

        return false;
    }

    public void SwitchMeshRenderer(bool value)
    {
        drumGameObjects = GameObject.FindGameObjectsWithTag("DrumGame");
        drumGameObjectsSpriteRenderer = GameObject.FindGameObjectsWithTag("DrumGameSpriteRenderer");
        foreach (GameObject gameObject in drumGameObjects)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = value;
        }
        foreach (GameObject gameObject in drumGameObjectsSpriteRenderer)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = value;
        }
        GameObject.Find("beat_circle").GetComponent<DrumBeatLogic>().enabled = value;
        GameObject.Find("PointCloud").GetComponent<MeshRenderer>().enabled = value;
        GameObject.Find("ParticleEffectCamera").GetComponent<VisualEffect>().enabled = value;
    }
}
