using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject bola;

    public static UIDetalleCharla singleton;
    public ScAgendaEvento charlas;
    public GameObject prCharla;
    public GameObject prDia;
    public Transform padre;
    public bool esAgenda = false;

    public Agenda agenda;

    int idActual;

    private void Awake()
	{
        singleton = this;
	}

	private void Start()
    {
        if (PlayerPrefs.GetString("agenda").Length > 2)
            agenda = JsonUtility.FromJson<Agenda>(PlayerPrefs.GetString("agenda"));

        GameObject go = Instantiate(prDia, padre);
        go.GetComponent<Charla>().Inicializar(imPerfil.sprite, "Dia 1", "");
		if (!esAgenda)
		{
            for (int i = 0; i < charlas.charlasDia1.Length; i++)
		    {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(charlas.charlasDia1[i], 1, i);
		    }
		}
		else
		{
            for (int i = 0; i < agenda.charlas1.Count; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(agenda.charlas1[i], 1, i);
            }
            for (int i = 0; i < agenda.workshops1.Count; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(agenda.workshops1[i], 1, i);
            }
        }
        
        go = Instantiate(prDia, padre);
        go.GetComponent<Charla>().Inicializar(imPerfil.sprite, "Dia 2", "");
        if (!esAgenda)
        {
            for (int i = 0; i < charlas.charlasDia2.Length; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(charlas.charlasDia2[i], 1, i);
            }
        }
        else
        {
            for (int i = 0; i < agenda.charlas2.Count; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(agenda.charlas2[i], 1, i);
            }
            for (int i = 0; i < agenda.workshops2.Count; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(agenda.workshops2[i], 1, i);
            }
        }

        go = Instantiate(prDia, padre);
        go.GetComponent<Charla>().Inicializar(imPerfil.sprite, "Dia 3", "");
        if (!esAgenda)
        {
            for (int i = 0; i < charlas.charlasDia2.Length; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(charlas.charlasDia2[i], 1, i);
            }
        }
        else
        {
            for (int i = 0; i < agenda.charlas3.Count; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(agenda.charlas3[i], 1, i);
            }
            for (int i = 0; i < agenda.workshops3.Count; i++)
            {
                go = Instantiate(prCharla, padre);
                go.GetComponent<Charla>().Inicializar(agenda.workshops3[i], 1, i);
            }
        }

    }

	public void Previsualizar(ScObjCharla cual, int _id)
	{
        objetoActual = cual;
        imPerfil.sprite = cual.imagen;
        txtTitulo.text = cual.nombre;
        txtDescripcion.text = cual.descipcion;
        bola.SetActive(cual.hora.Length > 2);
        txtHora.text = cual.hora;
        txtLugar.text = cual.Lugar;
        gmDetalle.SetActive(true);
        idActual = _id;
	}


    public void Agendar()
    {
        agenda.Agregar(objetoActual, objetoActual.dia);
        PlayerPrefs.SetString("agenda", JsonUtility.ToJson(agenda));
    }
    public void Desagendar()
    {
		switch (objetoActual.tipo)
		{
			case TipoCharla.Charla:
                if (objetoActual.dia == 1)
                {
                    agenda.charlas1.RemoveAt(idActual);
                }
                else if(objetoActual.dia == 2)

                {
                    agenda.charlas2.RemoveAt(idActual);
                }else 
                {
                    agenda.charlas3.RemoveAt(idActual);
                }
                break;
			case TipoCharla.Workshop:
                if (objetoActual.dia == 1)
                {
                    agenda.workshops1.RemoveAt(idActual);
                }
                else if (objetoActual.dia == 2)

                {
                    agenda.workshops2.RemoveAt(idActual);
                }
                else
                {
                    agenda.workshops3.RemoveAt(idActual);
                }
                break;
			default:
				break;
		}

        PlayerPrefs.SetString("agenda", JsonUtility.ToJson(agenda));
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
