using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class conectiondb : MonoBehaviour
{
    private DateTime currentDateTime;
    public InputField inpUsuario;
    public InputField inpComentario;
    public int idCharla;
    public void Enviar()
    {
        //se llama a la corutina
        StartCoroutine(Upload());
    }
    //se define la corutina

    IEnumerator Upload()
    {
        currentDateTime = DateTime.Now;
        WWWForm form1 = new WWWForm();
        form1.AddField("id_charla",  idCharla.ToString());
        form1.AddField("usuario",    inpUsuario.text);
        form1.AddField("comentario", inpComentario.text);
        form1.AddField("fechayhora", ""+currentDateTime);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/WebCongreso/insertintocoment.php", form1);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
