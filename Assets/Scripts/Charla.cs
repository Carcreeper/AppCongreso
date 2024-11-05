using UnityEngine;
using UnityEngine.UI;

public class Charla : MonoBehaviour
{
    public Image imPrevia;
    public Text txtTitulo;
    public Text txtDescripcion;
    public ScObjCharla datos;

	int dia, id;

	private void Start()
	{
		//Inicializar();
	}

	public void Inicializar(ScObjCharla _datos)
	{
        datos = _datos;
        Inicializar();
	}
	public void Inicializar(ScObjCharla _datos, int _dia, int _id)
	{
		datos = _datos;
		dia = _dia;
		id = _id;
		Inicializar();
	}


	void Inicializar()
	{
		if (datos != null)
		{
            Inicializar(datos.imagen, datos.nombre, datos.descipcion + "\n" + datos.hora + " / " + datos.Lugar);
		}
	}

    public void Inicializar(Sprite previa, string titulo, string descripcion)
	{
        txtDescripcion.text = descripcion;
        txtTitulo.text = titulo;

        imPrevia.sprite = previa;
	}

	public void CargarPreview()
	{
		UIDetalleCharla.singleton.Previsualizar(datos, id); 
	}
}
