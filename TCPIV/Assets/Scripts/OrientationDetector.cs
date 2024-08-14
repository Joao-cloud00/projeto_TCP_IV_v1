using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrientationDetector : MonoBehaviour
{
    public Text orientationText;

    private void Update()
    {
        UpdateOrientation();
    }

    void UpdateOrientationStart()
    {
        float aspectRatio = (float)Screen.width / Screen.height;

        if (aspectRatio > 1.0f)
        {
            // Orienta��o paisagem
            orientationText.text = "Dispositivo deitado (Paisagem)";
        }
        else
        {
            // Orienta��o retrato
            orientationText.text = "Dispositivo em p� (Retrato)";
        }
    }

    void UpdateOrientation()
    {
        DeviceOrientation orientation = Input.deviceOrientation;

        switch (orientation)
        {
            case DeviceOrientation.Portrait:
            case DeviceOrientation.PortraitUpsideDown:
                orientationText.text = "Dispositivo em p� (Retrato)";
                break;
            case DeviceOrientation.LandscapeLeft:
            case DeviceOrientation.LandscapeRight:
                orientationText.text = "Dispositivo deitado (Paisagem)";
                break;
            default:
                UpdateOrientationStart();
                break;
        }
    }
}




