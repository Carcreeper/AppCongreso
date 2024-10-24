using UnityEngine;
using UnityEngine.UI;

public class Charla : MonoBehaviour
{
    public Image imPrevia;
    public Text txtTitulo;
    public Text txtDescripcion;
    public ScObjCharla datos;

	private void Start()
	{
		Inicializar();
	}

	public void Inicializar(ScObjCharla _datos)
	{
        datos = _datos;
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
		UIDetalleCharla.singleton.Previsualizar(datos); 
	}
}
