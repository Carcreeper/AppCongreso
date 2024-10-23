using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDetalleCharla : MonoBehaviour
{
    public Image imPerfil;
    public Text txtTitulo;
    public Text txtDescripcion;
    public Text txtHora;
    public Text txtLugar;
    public GameObject gmDetalle;
    public ScObjCharla objetoActual;

    public static UIDetalleCharla singleton;

	private void Awake()
	{
        singleton = this;
	}

	public void Previsualizar(ScObjCharla cual)
	{
        objetoActual = cual;
        imPerfil.sprite = cual.imagen;
        txtTitulo.text = cual.nombre;
        txtDescripcion.text = cual.descipcion;
        txtHora.text = cual.hora;
        txtLugar.text = cual.Lugar;
        gmDetalle.SetActive(true);
	}
}
