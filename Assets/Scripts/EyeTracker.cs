using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;

public class EyeTracker : MonoBehaviour
{

    private IEyeTracker eyeTracker;

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
        GazePoint left = gaze.LeftEye.GazePoint;
        if(left.Validity == Validity.Valid)
            return left.PositionOnDisplayArea.X;
        else
            return float.NaN;
    }

    public float GetLeftY()
    {
        GazePoint left = gaze.LeftEye.GazePoint;
        if(left.Validity == Validity.Valid)
            return left.PositionOnDisplayArea.Y;
        else
            return float.NaN;
    }

    public float GetLeftPupil()
    {
        PupilData lPupil = gaze.LeftEye.Pupil;
        if(lPupil.Validity == Validity.Valid)
            return lPupil.PupilDiameter;
        else
            return float.NaN;
    }

    public float GetRightX()
    {
        GazePoint right = gaze.RightEye.GazePoint;
        if(right.Validity == Validity.Valid)
            return right.PositionOnDisplayArea.X;
        else
            return float.NaN;
    }

    public float GetRightY()
    {
        GazePoint right = gaze.RightEye.GazePoint;
        if(right.Validity == Validity.Valid)
            return right.PositionOnDisplayArea.Y;
        else
            return float.NaN;
    }

    public float GetRightPupil()
    {
        PupilData rPupil = gaze.RightEye.Pupil;
        if(rPupil.Validity == Validity.Valid)
            return rPupil.PupilDiameter;
        else
            return float.NaN;
    }

    private void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
    {
        gaze = e;
        Debug.Log("Recieved gaze data.");
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
