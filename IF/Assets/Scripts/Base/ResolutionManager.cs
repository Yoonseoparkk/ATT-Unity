using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public void SetResolution(string resolutionString)
    {
        string[] resolution = resolutionString.Split(',');
        if (resolution.Length == 2)
        {
            int width = int.Parse(resolution[0]);
            int height = int.Parse(resolution[1]);
            Screen.SetResolution(width, height, false);
        }
    }
}