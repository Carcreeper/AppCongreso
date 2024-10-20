using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
[RequireComponent(typeof(LineRenderer))]
public class MorionWaze : MonoBehaviour
{
    public NodoWaze nodoInicial;
    public NodoWaze nodoFinal;
	public InsipadorGPS inspirador;
	
	LineRenderer linea;


    public static MorionWaze singleton
	{
		get
		{
			MorionWaze mw = GameObject.FindAnyObjectByType<MorionWaze>(FindObjectsInactive.Exclude);
			return mw;
		}
	}
    public List<NodoWaze> ruta = new List<NodoWaze>();     // Ruta más corta encontrada

    private void Awake()
	{
		linea = GetComponent<LineRenderer>();
	}

	public void TrazarRuta()
    {
		if (nodoFinal==null || nodoInicial==null)
		{
			Debug.LogWarning("No están configurados los nodos correctamente");
			return;
		}
		ruta = nodoInicial.BuscarRuta(nodoFinal);
		VerRuta();
    }

	public void VerRuta()
	{
		if (linea == null)
		{
			linea = GetComponent<LineRenderer>();
		}
		Vector3[] posiciones = new Vector3[ruta.Count];
		for (int i = 0; i < posiciones.Length; i++)
		{
			posiciones[i] = ruta[i].transform.position;
		}

		Vector3[] nuevasPos = RecalcularLineas(posiciones);
		linea.positionCount = nuevasPos.Length;
		linea.SetPositions(nuevasPos);
		linea.enabled = true;
	}

	Vector3[] RecalcularLineas(Vector3[] lineasLinSuavizado)
	{
		List<Vector3> puntosSuavizados = new List<Vector3>();

		// Cantidad de subdivisiones entre cada segmento
		int subdivisiones = 10;

		// Agregar el primer punto directamente
		puntosSuavizados.Add(lineasLinSuavizado[0]);

		for (int i = 0; i < lineasLinSuavizado.Length - 1; i++)
		{
			Vector3 p0 = (i == 0) ? lineasLinSuavizado[i] : lineasLinSuavizado[i - 1];
			Vector3 p1 = lineasLinSuavizado[i];
			Vector3 p2 = lineasLinSuavizado[i + 1];
			Vector3 p3 = (i + 2 < lineasLinSuavizado.Length) ? lineasLinSuavizado[i + 2] : lineasLinSuavizado[i + 1];

			for (int j = 0; j < subdivisiones; j++)
			{
				float t = j / (float)subdivisiones;
				Vector3 puntoInterpolado = CatmullRom(p0, p1, p2, p3, t);
				puntosSuavizados.Add(puntoInterpolado);
			}
		}

		// Agregar el último punto directamente
		puntosSuavizados.Add(lineasLinSuavizado[lineasLinSuavizado.Length - 1]);

		return puntosSuavizados.ToArray();
	}

	// Función de interpolación de Catmull-Rom
	Vector3 CatmullRom(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
	{
		float t2 = t * t;
		float t3 = t2 * t;

		return 0.5f * (
			(2f * p1) +
			(-p0 + p2) * t +
			(2f * p0 - 5f * p1 + 4f * p2 - p3) * t2 +
			(-p0 + 3f * p1 - 3f * p2 + p3) * t3
		);
	}



	private void FixedUpdate()
	{
		//linea.SetPosition(0, inspirador.transform.position);
	}


#if UNITY_EDITOR
	// Método para dibujar gizmos en el editor
	private void OnDrawGizmos()
	{
		bool verRutas = EditorPrefs.GetBool("VisualSettings_VerRutas", true);
		
		if (verRutas)
		{
			Gizmos.color = Color.red;
			for (int i = 1; i < ruta.Count; i++)
			{
				Gizmos.DrawLine(ruta[i - 1].transform.position, ruta[i].transform.position);  // Dibuja la ruta
			}
		}
	}
#endif
}
