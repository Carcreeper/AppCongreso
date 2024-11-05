using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Charla", menuName = "Morion/Charla", order = 1)]
public class ScObjCharla : ScriptableObject
{
    public Sprite imagen;
    public string nombre;
    public string descipcion;
    public string hora;
    public string Lugar;
    public TipoCharla tipo;
    public int dia;
}


public enum TipoCharla
{
    Charla,
    Workshop
}

[System.Serializable]
public class Agenda
{
    public List<ScObjCharla> charlas1 = new List<ScObjCharla>();
    public List<ScObjCharla> charlas2 = new List<ScObjCharla>();
    public List<ScObjCharla> charlas3 = new List<ScObjCharla>();

    public List<ScObjCharla> workshops1 = new List<ScObjCharla>();
    public List<ScObjCharla> workshops2 = new List<ScObjCharla>();
    public List<ScObjCharla> workshops3 = new List<ScObjCharla>();


    public void Agregar(ScObjCharla objc, int dia)
	{
		switch (objc.tipo)
		{
			case TipoCharla.Charla:
				if (dia == 1)
				{
                    charlas1.Add(objc);
				}else if (dia == 2)
                {
                    charlas2.Add(objc);
                }
                else
				{
                    charlas3.Add(objc);
                }
                break;
			case TipoCharla.Workshop:
                if (dia == 1)
                {
                    workshops1.Add(objc);
                }
                else if (dia == 2)
                {
                    workshops2.Add(objc);
                }
                else
                {
                    workshops3.Add(objc);
                }
                break;
			default:
				break;
		}
	}
}