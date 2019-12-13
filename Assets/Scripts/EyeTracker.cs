using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTracker : MonoBehaviour
{

    public IEyeTracker eyeTracker;

    private static GazeDataEventArgs gaze;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            eyeTracker = EyeTrackingOperations.FindAllEyeTrackers()[0];
            eyeTracker.GazeDataReceived += EyeTracker_GazeDataReceived;
        }
        catch
        {
            Debug.LogError("Eye tracker not found!");
        }
    }

    public float GetLeftX()
    {
        return gaze.LeftEye.GazePoint.PositionOnDisplayArea.X;
    }

    public float GetLeftY()
    {
        return gaze.LeftEye.GazePoint.PositionOnDisplayArea.Y;
    }

    public float GetLeftPupil()
    {
        return gaze.LeftEye.Pupil.PupilDiameter;
    }

    public float GetRightX()
    {
        return gaze.RightEye.GazePoint.PositionOnDisplayArea.X;
    }

    public float GetRightY()
    {
        return gaze.RightEye.GazePoint.PositionOnDisplayArea.Y;
    }

    public float GetRightPupil()
    {
        return gaze.RightEye.Pupil.PupilDiameter;
    }

    private void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
    {
        gaze = e;
    }

    private void OnApplicationQuit()
    {
        try
        {
            Debug.Log("Terminating eye tracker operation.");
            eyeTracker.GazeDataReceived -= EyeTracker_GazeDataReceived;
            EyeTrackingOperations.Terminate();
        }
        catch{}
    }

}
