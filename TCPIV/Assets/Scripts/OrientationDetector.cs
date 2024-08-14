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
            // Orientação paisagem
            orientationText.text = "Dispositivo deitado (Paisagem)";
        }
        else
        {
            // Orientação retrato
            orientationText.text = "Dispositivo em pé (Retrato)";
        }
    }

    void UpdateOrientation()
    {
        DeviceOrientation orientation = Input.deviceOrientation;

        switch (orientation)
        {
            case DeviceOrientation.Portrait:
            case DeviceOrientation.PortraitUpsideDown:
                orientationText.text = "Dispositivo em pé (Retrato)";
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




