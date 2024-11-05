using UnityEngine;
using UnityEngine.SceneManagement;

public class Escenas : MonoBehaviour
{
    public void CambiarEscena(string nombre)
    {
        SceneManager.LoadScene(nombre);
    }
    public void Salir ()
    {
        Application.Quit();
    }

    public void ArbrirURL(string url)
	{
        Application.OpenURL(url);
	}
}
