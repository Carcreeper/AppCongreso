using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class NodoWaze : MonoBehaviour
{
    public List<NodoWaze> vecinos = new List<NodoWaze>();  // Vecinos conectados a este nodo
    public float radioVecinos;                             // Radio de alcance para los vecinos

    // Método para encontrar vecinos cercanos dentro de un radio
    public void BuscarVecinos()
    {
        vecinos.Clear();  // Limpiar la lista de vecinos
        NodoWaze[] posibles = GameObject.FindObjectsByType<NodoWaze>(FindObjectsSortMode.InstanceID);
        for (int i = 0; i < posibles.Length; i++)
        {
            if ((posibles[i].transform.position - transform.position).magnitude < radioVecinos && posibles[i] != this)
            {
                vecinos.Add(posibles[i]);  // Añadir los nodos vecinos cercanos
            }
        }
    }
    public List<NodoWaze> BuscarRuta(NodoWaze target)
    {
        return AStar(this, target);  // Ejecutar la búsqueda A* entre el nodo actual y el objetivo
    }
    // Algoritmo A* recursivo para encontrar la ruta más corta
    private List<NodoWaze> AStar(NodoWaze inicio, NodoWaze objetivo)
    {
        // Conjuntos para mantener los nodos explorados y por explorar
        HashSet<NodoWaze> explorados = new HashSet<NodoWaze>();
        PriorityQueue<NodoWaze> porExplorar = new PriorityQueue<NodoWaze>(); // Cola de prioridad

        // Diccionario para rastrear de dónde viene cada nodo
        Dictionary<NodoWaze, NodoWaze> padres = new Dictionary<NodoWaze, NodoWaze>();
        // Diccionario para almacenar el coste desde el nodo inicial hasta cada nodo
        Dictionary<NodoWaze, float> costeG = new Dictionary<NodoWaze, float>();
        // Inicialización
        porExplorar.Enqueue(inicio, 0);
        padres[inicio] = null;
        costeG[inicio] = 0;

        // Mientras haya nodos por explorar
        while (porExplorar.Count > 0)
        {
            NodoWaze actual = porExplorar.Dequeue();  // Nodo con la menor función de coste f(n)

            // Si hemos llegado al nodo objetivo, reconstruimos la ruta
            if (actual == objetivo)
            {
                return ReconstruirRuta(padres, actual);
            }

            explorados.Add(actual);  // Marcar el nodo como explorado

            // Explorar los vecinos del nodo actual
            foreach (NodoWaze vecino in actual.vecinos)
            {
                if (explorados.Contains(vecino)) continue;  // Si ya fue explorado, lo ignoramos

                // Calcular el coste desde el inicio hasta el vecino
                float nuevoCosteG = costeG[actual] + Vector3.Distance(actual.transform.position, vecino.transform.position);

                if (!costeG.ContainsKey(vecino) || nuevoCosteG < costeG[vecino])
                {
                    // Actualizar el coste hasta el vecino y registrar su padre
                    costeG[vecino] = nuevoCosteG;
                    float costeF = nuevoCosteG + Heuristica(vecino, objetivo);  // f(n) = g(n) + h(n)
                    porExplorar.Enqueue(vecino, costeF);
                    padres[vecino] = actual;
                }
            }
        }

        // Si no se encontró una ruta
        Debug.LogWarning("No se encontró una ruta hacia el objetivo.");
        return new List<NodoWaze>();
    }

    // Función heurística: distancia euclidiana entre dos nodos
    private float Heuristica(NodoWaze nodo, NodoWaze objetivo)
    {
        return Vector3.Distance(nodo.transform.position, objetivo.transform.position);
    }

    // Reconstruir la ruta desde el objetivo hasta el inicio
    private List<NodoWaze> ReconstruirRuta(Dictionary<NodoWaze, NodoWaze> padres, NodoWaze actual)
    {
        List<NodoWaze> ruta = new List<NodoWaze>();
        while (actual != null)
        {
            ruta.Insert(0, actual);  // Insertar el nodo al principio de la ruta
            actual = padres[actual]; // Retroceder al nodo padre
        }
        return ruta;  // Retornar la ruta reconstruida
    }


#if UNITY_EDITOR
    // Método para dibujar gizmos en el editor
    private void OnDrawGizmos()
    {
        bool verCirculos = EditorPrefs.GetBool("VisualSettings_VerCirculos", true);
        bool verVecinos = EditorPrefs.GetBool("VisualSettings_VerVecinos", true);
        if (verVecinos)
		{
			Gizmos.color = Color.green;
			for (int i = 0; i < vecinos.Count; i++)
			{
				Gizmos.DrawLine(transform.position, vecinos[i].transform.position);  // Dibuja las conexiones entre vecinos
			}
		}
		if (verCirculos)
		{
			Gizmos.color = Color.yellow;
			Gizmos.DrawWireSphere(transform.position, radioVecinos);  // Dibuja un radio de influencia de vecinos
		}

	}
#endif
}

public class PriorityQueue<T>
{
    private List<KeyValuePair<T, float>> elements = new List<KeyValuePair<T, float>>();

    public int Count
    {
        get { return elements.Count; }
    }

    // Agrega un elemento a la cola con una prioridad asociada
    public void Enqueue(T item, float priority)
    {
        elements.Add(new KeyValuePair<T, float>(item, priority));
    }

    // Remueve y retorna el elemento con la menor prioridad (costo)
    public T Dequeue()
    {
        int bestIndex = 0;

        // Encontrar el elemento con la menor prioridad
        for (int i = 0; i < elements.Count; i++)
        {
            if (elements[i].Value < elements[bestIndex].Value)
            {
                bestIndex = i;
            }
        }

        T bestItem = elements[bestIndex].Key;
        elements.RemoveAt(bestIndex); // Eliminar el mejor elemento de la cola
        return bestItem;
    }
}