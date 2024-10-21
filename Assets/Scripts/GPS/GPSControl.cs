using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GPSControl : MonoBehaviour
{
    public Text txtGPS;
    IEnumerator Start()
    {
        // Check if the user has location service enabled.
        if (!Input.location.isEnabledByUser)
            txtGPS.text = ("No se puede acceder al GPS, dice deshabilitado o no le ha dado permisos a la app");

        // Starts the location service.
        Input.location.Start();

		while (true)
		{
            yield return new WaitForSeconds(10);
            // Waits until the location service initializes
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            // If the service didn't initialize in 20 seconds this cancels location service use.
            if (maxWait < 1)
            {
                txtGPS.text = ("Mucho tiempo y sin respuesta... :(");
                yield break;
            }

            // If the connection failed this cancels location service use.
            if (Input.location.status == LocationServiceStatus.Failed)
            {
                txtGPS.text = ("No encuentro ubicación, mire que sí esté metido en la matriz...");
                yield break;
            }
            else
            {
                // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
                txtGPS.text = ("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            }

        }
    }
}
