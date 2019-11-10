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
            GameObject obj = GameObject.Find("beat_circle");
            DrumBeatLogic objFunction = obj.GetComponent<DrumBeatLogic>();
            objFunction.fail++;
            //print(objFunction.fail);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //check if left or right hand hits the object
        if (other.name == "LeftHand" || other.name == "RightHand")
        {
            //check distance between object and drum at the moment the player hits
            if (Vector3.Distance(this.transform.position, drum.transform.position) <= 2)
            {
                GameObject obj = GameObject.Find("beat_circle");
                DrumBeatLogic objFunction = obj.GetComponent<DrumBeatLogic>();
                //if (objFunction.myPrefab != null)
                //{
                objFunction.score++;
                Destroy(gameObject);
                //}

            }
            else
            {
                GameObject obj = GameObject.Find("beat_circle");
                DrumBeatLogic objFunction = obj.GetComponent<DrumBeatLogic>();

                // Debug.Log("fail in check Trigger");
                Debug.Log(objFunction.fail);

                objFunction.fail++;
                //Destroy(gameObject);
            }
        }

        else if( other.name == "destructionzone1" || other.name == "destructionzone2")
        {
            Destroy(gameObject);
        }

    }

    private void OnDestroy()
    {
        spawner.count--;
        //scoreText.text = "" + spawner.count;
    }
}
