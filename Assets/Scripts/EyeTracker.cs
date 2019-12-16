using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research;

public class EyeTracker : MonoBehaviour
{

    private IEyeTracker eyeTracker;

    private static GazeDataEventArgs gaze;

    private GazePoint left;
    private GazePoint right;
    private PupilData lPupil;
    private PupilData rPupil;

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
        if(left.Validity == Validity.Valid)
            return left.PositionOnDisplayArea.X;
        else
            return float.NaN;
    }

    public float GetLeftY()
    {
        if(left.Validity == Validity.Valid)
            return left.PositionOnDisplayArea.Y;
        else
            return float.NaN;
    }

    public float GetLeftPupil()
    {
        if(lPupil.Validity == Validity.Valid)
            return lPupil.PupilDiameter;
        else
            return float.NaN;
    }

    public float GetRightX()
    {
        if(right.Validity == Validity.Valid)
            return right.PositionOnDisplayArea.X;
        else
            return float.NaN;
    }

    public float GetRightY()
    {
        if(right.Validity == Validity.Valid)
            return right.PositionOnDisplayArea.Y;
        else
            return float.NaN;
    }

    public float GetRightPupil()
    {
        if(rPupil.Validity == Validity.Valid)
            return rPupil.PupilDiameter;
        else
            return float.NaN;
    }

    private void EyeTracker_GazeDataReceived(object sender, GazeDataEventArgs e)
    {
        gaze = e;
        left = gaze.LeftEye.GazePoint;
        right = gaze.RightEye.GazePoint;
        lPupil = gaze.LeftEye.Pupil;
        rPupil = gaze.RightEye.Pupil;
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
