using UnityEngine;
using UnityEngine.UI;

public class Charla : MonoBehaviour
{
    public Image imPrevia;
    public Text txtTitulo;
    public Text txtDescripcion;

    public void Inicializar(Sprite previa, string titulo, string descripcion)
	{
        txtDescripcion.text = descripcion;
        txtTitulo.text = titulo;

        imPrevia.sprite = previa;
	}
}
