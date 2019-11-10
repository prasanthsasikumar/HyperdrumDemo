using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checkTrigger : MonoBehaviour
{
    public createObj spawner;
    public GameObject drum;
    //public Text scoreText;

    private void Update()
    {
        var deltaToDrum = (drum.transform.position - transform.position);
        if (deltaToDrum.z > 2)
        {
           // print("unpdate called");
            GameObject obj = GameObject.Find("beat_circle");
            createObj objFunction = obj.GetComponent<createObj>();
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
                createObj objFunction = obj.GetComponent<createObj>();
                //if (objFunction.myPrefab != null)
                //{
                    objFunction.score++;
                    Destroy(gameObject);
                //}

            }
            else {
                GameObject obj = GameObject.Find("beat_circle");
                createObj objFunction = obj.GetComponent<createObj>();

               // Debug.Log("fail in check Trigger");
                Debug.Log(objFunction.fail);
                    
                    objFunction.fail++;
                    //Destroy(gameObject);
            }
        }
        
    }

    private void Start()
    {
        spawner.count++;
        //scoreText.text = "" + spawner.count;
    }

    /*
    private void OnDestroy()
    {
        spawner.count--;
        //scoreText.text = "" + spawner.count;
    }
    */
}
