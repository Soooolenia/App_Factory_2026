using UnityEngine;

public class Chapter00 : MonoBehaviour
{
    [SerializeField] private CameraControl cameraControl;
    private void Start()
    {
        cameraControl.enabled = false;
    }
    public void StartGame()
    {
        cameraControl.enabled = true;
    }
}
