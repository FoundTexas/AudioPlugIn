using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BeatListener : MonoBehaviour
{
    GameObject[] objs;
    GameObject[] objsL;
    Color[] objsLColor;

    void Start()
    {
        objsL = GameObject.FindGameObjectsWithTag("BeatLight");

        objsLColor = new Color[objsL.Length+1];
        for(int i = 0; i < objsL.Length; i++)
        {
            objsLColor[i] = objsL[i].GetComponent<BarComponent>().getColor();
        }
        InvokeRepeating("UpdateBeat", 0.1f, 0.3f);
    }
    void UpdateBeat()
    {
        float[] Spectrum = new float[1024];
        AudioListener.GetOutputData(Spectrum, 0);
        float sum = Spectrum.ToList().Sum();

        objs = GameObject.FindGameObjectsWithTag("Beat");

        foreach (var obj in objs)
        {
            float sumtmp = Mathf.Clamp(sum, 1f, 3f);
            obj.transform.localScale = new Vector3(sumtmp * 0.9f, sumtmp, 1);
        }

        for (int i = 0; i < objsL.Length; i++)
        {
            float sumtmp = Mathf.Clamp(sum, 0.1f, 1f);
            BarComponent bar = objsL[i].GetComponent<BarComponent>();
            bar.setColor(new Color(objsLColor[i].r*sumtmp, objsLColor[i].g*sumtmp, objsLColor[i].b*sumtmp, bar.getColor().a));
        }
    }
}
