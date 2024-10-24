using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Senos : MonoBehaviour
{
    public float frecuencia;
    public float amplitud;
    public float desface;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = pos + Vector3.up * Mathf.Sin((desface + Time.time) * frecuencia) * amplitud;
    }
}
