using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkTrigger_new : MonoBehaviour
{
    public DrumBeatLogic spawner;
    public GameObject drum;

    // Start is called before the first frame update
    void Start()
    {
        spawner.count++;
    }

    // Update is called once per frame
    void Update()
    {
        var deltaToDrum = (drum.transform.position - transform.position);
        if (deltaToDrum.z > 2)
        {
            // print("unpdate called");
            GameObject obj = GameObject.Find("DrumBeatLogic");
            DrumBeatLogic objFunction = obj.GetComponent<DrumBeatLogic>();
            objFunction.fail++;
            //print(objFunction.fail);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //scoring only for P1, remember this is actually a single player game
        if (drum.name == "drum1" || drum.name == "drum2" || drum.name == "drum3" || drum.name == "drum4")
        {
            //check if left or right hand hits the object
            if (other.name == "Cube" || other.name == "RightHand")
            {
                //check distance between object and drum at the moment the player hits
                if (Vector3.Distance(this.transform.position, drum.transform.position) <= 2)
                {
                    GameObject obj = GameObject.Find("DrumBeatLogic");
                    DrumBeatLogic drumBeatLogic = obj.GetComponent<DrumBeatLogic>();
                    //objFunction.score++;
                    drumBeatLogic.score += (int)Mathf.Round(Vector3.Distance(this.transform.position, drum.transform.position));
                    GameObject.Find("NetworkManager").GetComponent<ApplicationController>().RegisterHit(GetNumberfromName(drum.name));
                    Destroy(gameObject);
                }
                else
                {
                    GameObject obj = GameObject.Find("DrumBeatLogic");
                    DrumBeatLogic drumBeatLogic = obj.GetComponent<DrumBeatLogic>();
                    Debug.Log(drumBeatLogic.fail);
                    drumBeatLogic.fail++;
                    GameObject.Find("NetworkManager").GetComponent<ApplicationController>().RegisterHit(0);
                    //Destroy(gameObject);
                }
            }
            else if (other.name == "destructionzone1" || other.name == "destructionzone2")
            {
                Destroy(gameObject);
            }
        }
        else if (drum.name == "drum5" || drum.name == "drum6" || drum.name == "drum7" || drum.name == "drum8")
        {
            if (other.name == "destructionzone1" || other.name == "destructionzone2")
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        spawner.count--;
        //scoreText.text = "" + spawner.count;
    }

    private int GetNumberfromName(string name)
    {
        //print(name.Substring());
        //return 1;
        return int.Parse(name.Substring(4));
    }
}
