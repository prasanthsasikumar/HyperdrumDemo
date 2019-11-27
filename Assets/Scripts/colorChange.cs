using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class colorChange : MonoBehaviour
{
    Renderer rend;
    Color red;
    public ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        //red = new Color(1, 0, 0, 1);
        rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("HDRP/Lit");
        ps.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        var emission = ps.emission;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube")
        {
            ps.Play();
            //ps.Simulate(0.2f);
            //StartCoroutine(stopEmission());
        }
    }

    void OnTriggerStay(Collider other)
    {
        //print(other.name);
        if (other.name == "Cube")
        {
            rend.material.SetColor("_BaseColor", Color.red);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        rend.material.SetColor("_BaseColor", Color.white);
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }

    IEnumerator stopEmission()
    {
        yield return new WaitForSeconds(.4f);
        print("stopping particle");
        ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
