using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_new : MonoBehaviour
{

    public Text scoreCounter;
    public DrumBeatLogic objFunction;
    public GameObject[] drumsP1;
    public GameObject[] drumsP2;
    public float accuracyP1, accuracyP2, timing, multiplier, finalscore;

    // Start is called before the first frame update
    void Start()
    {
        objFunction = GameObject.Find("beat_circle").GetComponent<DrumBeatLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //accuracyP1 = Vector3.Distance(this.transform.position, drum.transform.position);

        gameObject.GetComponent<TextMesh>().text = finalscore.ToString();

    }

}
