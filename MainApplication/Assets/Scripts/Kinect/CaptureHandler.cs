using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.Azure.Kinect.Sensor;

public class CaptureHandler : MonoBehaviour
{
    private RawImage rawImage;
    private Texture2D texture;

    private void Awake()
    {
        rawImage = GetComponent<RawImage>();
        texture = new Texture2D(Common.screenWidth, Common.screenHeight, TextureFormat.BGRA32, false);
    }

    public void updateTracker(BackgroundData trackerFrameData)
    {
        texture.LoadRawTextureData(trackerFrameData.ColorImage);
        texture.Apply();
        rawImage.texture = texture;
    }
}
