using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrumBeatLogic : MonoBehaviour
{
    Vector3[] positions = new Vector3[4];

    public GameObject myPrefab = null;

    public GameObject[] prefabs = new GameObject[4];

    public bool autoGenerate = true;

    [SerializeField] private List<GameObject> drumsP1;
    [SerializeField] private List<GameObject> drumsP2;
    [SerializeField] private List<GameObject> spawnpoints;

    int drumIndex;
    public int count = 0;
    public int difficulty;
    public float ringSpeed;

    Vector3 speed = new Vector3(0, 0, -1);

    public int score = 0;
    public int fail = 0;
    public Vector3 hitDistance;

    public float timer;
    public bool timerstart;

    // Start is called before the first frame update
    void Start()
    {
        timerstart = false;
        timer = 0;
        //spawning position
        for (int i = 0; i < drumsP1.Count; i++)
        {
            positions[i] = spawnpoints[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Generate 2 random circle per spawn point, going both directions */
        if (count < difficulty)
        {
            // create a new circle at the end of the path 
            drumIndex = Random.Range(0, 4);

            if (autoGenerate)
            {
                GameObject beatGO1 = Instantiate(prefabs[drumIndex], positions[drumIndex], Quaternion.identity);
                GameObject beatGO2 = Instantiate(prefabs[drumIndex], positions[drumIndex], Quaternion.identity);
                checkTrigger_new beat1 = beatGO1.GetComponent<checkTrigger_new>();
                checkTrigger_new beat2 = beatGO2.GetComponent<checkTrigger_new>();

                // Assign a drum to this beat
                beat1.drum = drumsP1[drumIndex];
                beat1.spawner = this;
                beat2.drum = drumsP2[drumIndex];
                beat2.spawner = this;

                beatGO1.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
                beatGO2.GetComponent<Rigidbody>().velocity = -speed * ringSpeed;
            }

        }

        if (timerstart)
            timer += Time.deltaTime;
        else
            timer = 0;
    }

    public void GenerateCircle(int number)
    {
        drumIndex = Random.Range(0, 4);
        GameObject beatGO1 = Instantiate(prefabs[drumIndex], positions[drumIndex], Quaternion.identity);
        GameObject beatGO2 = Instantiate(prefabs[drumIndex], positions[drumIndex], Quaternion.identity);
        checkTrigger_new beat1 = beatGO1.GetComponent<checkTrigger_new>();
        checkTrigger_new beat2 = beatGO2.GetComponent<checkTrigger_new>();
        beat1.drum = drumsP1[drumIndex];
        beat1.spawner = this;
        beat2.drum = drumsP2[drumIndex];
        beat2.spawner = this;
        beatGO1.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
        beatGO2.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
    }

}
