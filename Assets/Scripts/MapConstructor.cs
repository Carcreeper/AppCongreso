using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapConstructor : MonoBehaviour
{
    public Vector3 posCero;
    public MapPoint[] puntos;
    public float rango; 

    void Generar()
    {
        for (int i = 0; i < puntos.Length; i++)
        {
            puntos[i].objeto.position = transform.position + (puntos[i].puntoGPS - posCero) * rango;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Generar();
    }
}


[System.Serializable]
public class MapPoint
{
    public Vector3 puntoGPS;
    public Transform objeto;
}