using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarComponent : MonoBehaviour
{
    public GameObject Prefab;
    public float height;
    GameObject obj;
    Renderer ren;
    void Start()
    {
        obj =  Instantiate(Prefab, this.transform);
        obj.transform.localPosition = new Vector3(0, height / 2, 0);
        ren = obj.GetComponent<Renderer>();
    }

    public void setColor(Color color)
    {
        ren.material.color = color;
    }
    public Color getColor()
    {
        return ren.material.color;
    }
}
