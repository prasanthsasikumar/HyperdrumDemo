using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createObj : MonoBehaviour
{

    Vector3[] positions = new Vector3[4];                                                         

    public GameObject myPrefab = null;

    public GameObject[] prefabs = new GameObject[4];

    public bool autoGenerate = true;

    [SerializeField] private List<GameObject> drums;

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

    void Start()
    {
        timerstart = false;
        timer = 0;
        for (int i = 0; i < drums.Count; i++)
        {
            positions[i] = drums[i].transform.position + new Vector3(0, 0.8f, 10);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /* Check whether the user has failed the game */
        if (fail >= 30) {
            Debug.Log("create obj fail");
        
            Application.Quit();
        }

        /* Generate 1 random circle and makes it coming towards us */
        if (count < difficulty)
        {

            // create a new circle at the end of the path 
            drumIndex = Random.Range(0, 4);

            if (autoGenerate)
            {
                //StartCoroutine(Delay());

                GameObject beatGO = Instantiate(prefabs[drumIndex], positions[drumIndex], Quaternion.identity);
                checkTrigger beat = beatGO.GetComponent<checkTrigger>();

                // Assign a drum to this beat
                beat.drum = drums[drumIndex];
                beat.spawner = this;

                beatGO.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
            }

        }

        if (timerstart)
            timer += Time.deltaTime;
        else
            timer = 0;
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
    }

    public void GenerateCircle(int number)
    {
        drumIndex = Random.Range(0, 4);
        GameObject beatGO = Instantiate(prefabs[drumIndex], positions[drumIndex], Quaternion.identity);
        checkTrigger beat = beatGO.GetComponent<checkTrigger>();
        beat.drum = drums[drumIndex];
        beat.spawner = this;
        beatGO.GetComponent<Rigidbody>().velocity = speed * ringSpeed;
    }
}
