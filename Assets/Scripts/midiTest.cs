using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using System;

public class midiTest : MonoBehaviour
{
    public float decValueFloat = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //print("Knob numbers:  " + MidiMaster.GetKnobNumbers(MidiChannel.All)[0]);
        //print(MidiMaster.GetKnob(MidiChannel.All, 23));
        //print(MidiMaster.GetKey(MidiChannel.All, 7));
        var temp = "00000000000000000000";
        foreach (var message in MidiDriver.Instance.History)
            temp = message.ToString();
        string hexValue = temp.Substring(8,2);
        int decValue = Convert.ToInt32(hexValue, 16);
        decValueFloat = decValue / 100f;
        if (decValueFloat > 1) decValueFloat = 1f;
        //print(decValueFloat);
    }

}
