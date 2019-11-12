using OscCore;
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

    /***********************
     * Control Settings
     ***********************/

    [Header("Main Controls")]
    public bool PLVOverride = false;
    [Range(0, 1)]
    public float PLVValue;
    public bool autoGenerateCircles, startDemo, stopDemo, reloadScene;
    [Header("Server Information")]
    public bool imPlayer1 = true;
    public string server, serverPath; public int serverPort;
    [Header("Act as Server(use below incase server fails or for testing)")]
    public bool actAsServer = false;
    public List<string> addressList;
    public string pathToSend;
    public int port;
    public float value;
    public bool send = false;
    OscClient Server;
    public List<OscClient> clients = new List<OscClient>();

    void Start()
    {
        foreach(ApplicationParameters parameter in (ApplicationParameters[])Enum.GetValues(typeof(ApplicationParameters)))
        {
            //switches.Add(new ApplicationSwitches(parameter.ToString(), false));
        }
        autoGenerateCircles = GameObject.Find("DrumBeatLogic").GetComponent<DrumBeatLogic>().autoGenerate;
        Server = new OscClient(server, serverPort);
        foreach (string address in addressList)
        {
            clients.Add(new OscClient(address, port));
        }
    }

    void Update()
    {        
        if (startDemo)
            ManageApplication((int)ApplicationParameters.startDemo); startDemo = false;
        if (stopDemo)
            ManageApplication((int)ApplicationParameters.stopDemo); stopDemo = false;
        if (reloadScene)
            ManageApplication((int)ApplicationParameters.reloadScene); reloadScene = false;
        GameObject.Find("PointCloud").GetComponent<PointCloudController>().overRide = PLVOverride;
        GameObject.Find("DrumBeatLogic").GetComponent<DrumBeatLogic>().autoGenerate = autoGenerateCircles;
        if (PLVOverride)
            GameObject.Find("PointCloud").GetComponent<PointCloudController>().PLVValue = this.PLVValue;
        if (imPlayer1)
            serverPath = "/player1/registerhit";
        else
            serverPath = "/player2/registerhit";
        if (send)
        {
            SendtoClients();
            send = false;
        }
    }

    //Manages Application based on control input received from server
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
                GameObject.Find("DrumBeatLogic").GetComponent<DrumBeatLogic>().autoGenerate = false;
                break;
            case (int)ApplicationParameters.remoteBeatsDisable:
                GameObject.Find("DrumBeatLogic").GetComponent<DrumBeatLogic>().autoGenerate = true;
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

    //To make sure the duty is performed only once for a given signal
    public bool CheckActionDoneBefore(int newParamneter)
    {
        if(lastReceivedParameter == newParamneter)
        return true;

        return false;
    }

    //To temporary stop the scene
    public void SwitchMeshRenderer(bool value)
    {
        //drumGameObjects = GameObject.FindGameObjectsWithTag("DrumGame");
        drumGameObjectsSpriteRenderer = GameObject.FindGameObjectsWithTag("DrumGameSpriteRenderer");
        //foreach (GameObject gameObject in drumGameObjects)
        //{
        //    gameObject.GetComponent<MeshRenderer>().enabled = value;
        //}
        foreach (GameObject gameObject in drumGameObjectsSpriteRenderer)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = value;
        }
        GameObject.Find("DrumBeatLogic").GetComponent<DrumBeatLogic>().enabled = value;
        GameObject.Find("PointCloud").GetComponent<MeshRenderer>().enabled = value;
        GameObject.Find("ParticleEffectCamera").GetComponent<VisualEffect>().enabled = value;
    }

    //Each sucessfull drum hit is send to the server along with the drum number
    public void RegisterHit(int drum)
    {
        print("Drum" + drum + " hit registered");
        Server.Send(serverPath, drum);
    }

    public void SendtoClients()
    {
        foreach(OscClient client in clients)
        {
            client.Send(pathToSend, value);
        }
    }
}
