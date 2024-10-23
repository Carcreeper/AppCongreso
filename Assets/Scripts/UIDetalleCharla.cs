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

    public ScObjCharla[] charlas;
    public GameObject prCharla;
    public Transform padre;

    private void Awake()
	{
        singleton = this;
	}

	private void Start()
	{
		for (int i = 0; i < charlas.Length; i++)
		{
            GameObject go = Instantiate(prCharla, padre);
            go.GetComponent<Charla>().Inicializar(charlas[i]);
		}
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
