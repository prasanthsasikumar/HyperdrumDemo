using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class score : MonoBehaviour
{
    public Text scoreCounter;
    //  public GameObject newText;
    public GameObject obj;
    public createObj objFunction;

    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("beat_circle");
        //objFunction = obj.GetComponent<createObj>();
        //scoreCounter.text = objFunction.score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("beat_circle");
        createObj objFunction = obj.GetComponent<createObj>();
        //scoreCounter.text = objFunction.score.ToString();
        gameObject.GetComponent<TextMesh>().text = objFunction.score.ToString();
    }
}
