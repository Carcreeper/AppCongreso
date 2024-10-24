using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaestroDeOrquesta : MonoBehaviour
{
    // Clase interna que representa el evento con su tiempo
    [System.Serializable]
    public class EventoConTiempo
    {
        public float tiempo; // Tiempo en segundos en el que se activará el evento
        public UnityEvent evento; // UnityEvent que se activará
    }

    // Lista de eventos con su respectivo tiempo
    public List<EventoConTiempo> eventosConTiempo = new List<EventoConTiempo>();
    private float tiempoActual = 0f;

    // Empezamos la rutina en el Start
    private void Start()
    {
        StartCoroutine(ActualizarEventos());
    }
	// Rutina que actualiza el tiempo y activa los eventos en su momento
	IEnumerator ActualizarEventos()
    {
        // Iteramos sobre la lista
        foreach (EventoConTiempo evento in eventosConTiempo)
        {
            // Esperamos hasta que el tiempo actual alcance el tiempo del evento
            yield return new WaitForSeconds(evento.tiempo - tiempoActual);

            // Activamos el evento
            evento.evento.Invoke();

            // Actualizamos el tiempo actual
            tiempoActual = evento.tiempo;
        }
    }
}
