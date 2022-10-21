using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectrumView : MonoBehaviour
{
    public Gradient color;
    [Range(0, 360)]
    public int numOfBars;
    [Min (0)]
    public float radius = 50, amp = 50; 

    [Range(0,360)]
    public float degrees = 360;

    public bool X, Y, Z;

    public Vector3 dirstep;
    public Vector3 addstep;
    float angleStep;
    public GameObject bar;
    public GameObject[] bars;
    public GameObject spawn, icon;
    Vector3 pos;
    public bool canUpdate;

    void Start()
    {
        pos = spawn.transform.position;
        angleStep = 360 / numOfBars;
        setBars();
        InvokeRepeating("UpdateBars", 0.1f, 0.1f);
    }

    void setBars()
    {
        bars = new GameObject[numOfBars];
        float angle = 0;
        Vector3 movstep = Vector3.zero;


        for (int i = 0; i < numOfBars; i++)
        {
            Vector3 rot = spawn.transform.eulerAngles;
            rot.y = angle;

            GameObject g = Instantiate(bar, Vector3.zero, Quaternion.identity, spawn.transform);

            g.transform.localPosition = Vector3.zero;
            g.transform.Rotate(rot);
            g.transform.localPosition = ((g.transform.forward* dirstep.z + g.transform.up * dirstep.y + g.transform.right* dirstep.x )* radius )
                + movstep;
            angle += angleStep;
            movstep += addstep;

            bars[i] = g;
        }
    }

    void UpdateBars()
    {
        if (canUpdate)
        {
            float[] Spectrum = new float[1024];
            AudioListener.GetOutputData(Spectrum, 0);
            float sum = 0;

            for (int i = 0; i < numOfBars; i++)
            {
                Vector3 prevScale = bars[i].transform.localScale;

                if (X)
                    prevScale.x = Spectrum[i] * amp;
                if (Y)
                    prevScale.y = Spectrum[i] * amp;
                if (Z)
                    prevScale.z = Spectrum[i] * amp;
                sum += Spectrum[i];

                bars[i].transform.localScale = prevScale;
                bars[i].GetComponent<BarComponent>().setColor(color.Evaluate(Spectrum[i] * amp / 5));
            }

            if (icon)
            {
                sum = Mathf.Clamp(sum*amp, 1f, amp/4);

                icon.transform.localScale = new Vector3(sum, sum, sum);
            }
        }
    }

    private void Update()
    {
    }
}
