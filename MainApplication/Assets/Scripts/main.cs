using System.Collections.Generic;
using UnityEngine;
using Microsoft.Azure.Kinect.BodyTracking;
using UnityEngine.UI;
using Microsoft.Azure.Kinect.Sensor;

public class main : MonoBehaviour
{
    // Handler for SkeletalTracking thread.
    public GameObject m_tracker;
    public RawImage rawImage;
    private BackgroundDataProvider m_backgroundDataProvider;
    public BackgroundData m_lastFrameData = new BackgroundData();
    private Texture2D texture;

    void Start()
    {
        SkeletalTrackingProvider m_skeletalTrackingProvider = new SkeletalTrackingProvider();

        //tracker ids needed for when there are two trackers
        const int TRACKER_ID = 0;
        m_skeletalTrackingProvider.StartClientThread(TRACKER_ID);
        m_backgroundDataProvider = m_skeletalTrackingProvider;

        texture = new Texture2D(1280, 720, TextureFormat.BGRA32, false);
    }

    void Update()
    {
        if (m_backgroundDataProvider.IsRunning)
        {
            if (m_backgroundDataProvider.GetCurrentFrameData(ref m_lastFrameData))
            {
                if (m_lastFrameData.NumOfBodies != 0)
                {
                    m_tracker.GetComponent<TrackerHandler>().updateTracker(m_lastFrameData);

                    // get camera capture
                    texture.LoadRawTextureData(m_lastFrameData.ColorImage);
                    texture.Apply();
                    rawImage.texture = texture;
                }
            }
        }
    }

    void OnApplicationQuit()
    {
        // Stop background threads.
        if (m_backgroundDataProvider != null)
        {
            m_backgroundDataProvider.StopClientThread();
        }
    }
}
