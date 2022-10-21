using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpectrumView : MonoBehaviour
{
    public Gradient color;
    public int numOfBars;
    public float radius = 50, amp = 50;
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
        
        for(int i = 0; i < numOfBars; i++)
        {
            Vector3 rot = spawn.transform.eulerAngles;
            rot.z = angle;

            GameObject g = Instantiate(bar, Vector3.zero, Quaternion.identity, spawn.transform);

            g.transform.localPosition = Vector3.zero;
            g.transform.Rotate(rot);
            g.transform.localPosition = g.transform.up*radius;
            angle += angleStep;

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
                prevScale.y = Spectrum[i] * amp;
                sum += Spectrum[i];

                bars[i].transform.localScale = prevScale;
                bars[i].GetComponent<Image>().color = color.Evaluate(Spectrum[i] * amp/ 5);
            }

            if (icon)
            {
                sum = Mathf.Clamp(sum, 1f, 1.1f);

                icon.transform.localScale = new Vector3(sum, sum, 1);
            }
        }
    }

    private void Update()
    {
    }
}
