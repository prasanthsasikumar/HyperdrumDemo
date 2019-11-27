using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ChangeDancingSpeed : MonoBehaviour
{
    // Start is called before the first frame update
    public ApplicationController pointcloudcontroller;
    public PointCloudController overridePLV;
    public TimelineAsset karate;
    [Range(0, 1)]
    public float timeScale;
    [Range(0, 1)]
    public float PLVValue;


    TrackAsset AnimationTrack;
    PlayableDirector director;


    void Start()
    {
        director = this.GetComponent<PlayableDirector>();
        karate = (TimelineAsset)director.playableAsset;
        AnimationTrack = karate.GetOutputTrack(1);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (TimelineClip clip in AnimationTrack.GetClips())
        {
            //print(clip.ToString());
            //clip.timeScale = timeScale;

            if (overridePLV.overRide)
            {
                clip.timeScale = Mathf.Abs(pointcloudcontroller.PLVValue - 1);
            }
            else
            {
                clip.timeScale = PLVValue;
            }
        }


    }

    public void ReceivePLV(float plv)
    {
        //print("PLV received: " + plv);
        PLVValue = plv;
    }
}
