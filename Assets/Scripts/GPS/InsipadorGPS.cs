using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsipadorGPS : MonoBehaviour
{
    public NodoWaze[] puntos;
    NodoWaze cercano;
    private float distancia = 100000;

    void Start()
    {
        puntos = FindObjectsByType<NodoWaze>(FindObjectsSortMode.InstanceID);
        cercano = puntos[0];
        StartCoroutine(UpdateMorion());
    }

    // Update is called once per frame
    IEnumerator UpdateMorion()
    {
        float d;
		while (true)
		{

            distancia = (transform.position - cercano.transform.position).sqrMagnitude;
            for (int i = 0; i < puntos.Length; i++)
			{
                d = (transform.position - puntos[i].transform.position).sqrMagnitude;

                if (d < distancia)
				{
                    distancia = d;
					if (cercano != puntos[i])
					{
                        cercano = puntos[i];
                        MorionWaze.singleton.nodoInicial = cercano;
                        MorionWaze.singleton.TrazarRuta();
					}
				}
			}
            yield return new WaitForSeconds(0.5f);

		}
    }
}
