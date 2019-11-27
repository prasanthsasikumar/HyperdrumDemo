using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumBeatLogic : MonoBehaviour
{
    Vector3[] positionsP1 = new Vector3[4];
    Quaternion[] rotationsP1 = new Quaternion[4];
    Vector3[] positionsP2 = new Vector3[4];
    Quaternion[] rotationsP2 = new Quaternion[4];

    public GameObject[] prefabs = new GameObject[4];

    public bool autoGenerate = true;

    [SerializeField] private List<GameObject> drumsP1;
    [SerializeField] private List<GameObject> drumsP2;
    [SerializeField] private List<GameObject> spawnpointsP1;
    [SerializeField] private List<GameObject> spawnpointsP2;

    int drumIndex;
    public int count = 0;
    public int difficulty;
    public float ringSpeed;

    Vector3 speed = new Vector3(0, -1, 0);

    public int score = 0, lastScore;
    public int fail = 0, lastFail;
    public Vector3 hitDistance;

    public float timer;
    public bool timerstart;

    public ApplicationController playerselector;

    // Start is called before the first frame update
    void Start()
    {
        timerstart = false;
        timer = 0;
        lastScore = score;
        lastFail = fail;
        //spawning position
        for (int i = 0; i < drumsP1.Count; i++)
        {
            positionsP1[i] = spawnpointsP1[i].transform.position;
            rotationsP1[i] = spawnpointsP1[i].transform.rotation;
            positionsP2[i] = spawnpointsP2[i].transform.position;
            rotationsP2[i] = spawnpointsP2[i].transform.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < drumsP1.Count; i++)
        {
            positionsP1[i] = spawnpointsP1[i].transform.position;
            rotationsP1[i] = spawnpointsP1[i].transform.rotation;
            positionsP2[i] = spawnpointsP2[i].transform.position;
            rotationsP2[i] = spawnpointsP2[i].transform.rotation;
        }

        /* Generate 2 random circle per spawn point, going both directions */
        if (count < difficulty)
        {
            // create a new circle at the end of the path 
            drumIndex = Random.Range(0, 4);

            if (autoGenerate)
            {
                //instantiate rings
                GameObject beatGO1 = Instantiate(prefabs[drumIndex], positionsP1[drumIndex], rotationsP1[drumIndex]);
                GameObject beatGO2 = Instantiate(prefabs[drumIndex], positionsP2[drumIndex], rotationsP2[drumIndex]);
                checkTrigger_new beat1 = beatGO1.GetComponent<checkTrigger_new>();
                checkTrigger_new beat2 = beatGO2.GetComponent<checkTrigger_new>();

                // Assign a drum to this beat
                beat1.drum = drumsP1[drumIndex];
                beat1.spawner = this;
                beat2.drum = drumsP2[drumIndex];
                beat2.spawner = this;

                //set their repective drums as parent
                beatGO1.transform.SetParent(beat1.drum.transform);
                beatGO2.transform.SetParent(beat2.drum.transform);

                beatGO1.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
                beatGO2.GetComponent<Rigidbody>().velocity = speed * ringSpeed;

                if (playerselector.imPlayer1)
                {
                    beatGO1.GetComponent<SpriteRenderer>().enabled = true;
                    beatGO2.GetComponent<SpriteRenderer>().enabled = false;
                }
                else
                {
                    beatGO1.GetComponent<SpriteRenderer>().enabled = false;
                    beatGO2.GetComponent<SpriteRenderer>().enabled = true;
                }

            }

        }

            

        if (timerstart)
            timer += Time.deltaTime;
        else
            timer = 0;
    }

    public void GenerateCircle(int number)
    {
        drumIndex = number;// Random.Range(0, 4);
        GameObject beatGO1 = Instantiate(prefabs[drumIndex], positionsP1[drumIndex], rotationsP1[drumIndex]);
        GameObject beatGO2 = Instantiate(prefabs[drumIndex], positionsP2[drumIndex], rotationsP2[drumIndex]);
        checkTrigger_new beat1 = beatGO1.GetComponent<checkTrigger_new>();
        checkTrigger_new beat2 = beatGO2.GetComponent<checkTrigger_new>();
        beat1.drum = drumsP1[drumIndex];
        beat1.spawner = this;
        beat2.drum = drumsP2[drumIndex];
        beat2.spawner = this;
        beatGO1.transform.SetParent(beat1.drum.transform);
        beatGO2.transform.SetParent(beat2.drum.transform);
        beatGO1.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
        beatGO2.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
    }

}
