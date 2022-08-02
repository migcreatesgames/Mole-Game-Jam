using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private static CameraManager _instance;

    private static CinemachineVirtualCamera _curCam;
    private static CinemachineVirtualCamera _main_VC;
    private static CinemachineVirtualCamera _intro_VC;

    public static CameraManager Instance { get => _instance; set => _instance = value; }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            InitCamManager();
            return;
        }
        Destroy(this);
    }

    private void InitCamManager()
    {
        _main_VC = GameObject.Find("CM vcam_Main").GetComponent<CinemachineVirtualCamera>();
        _intro_VC = GameObject.Find("CM vcam_IntroDolly").GetComponent<CinemachineVirtualCamera>();

        //EnableIntroCamera();
    }

    public void EnableMainCamera()
    {
        _intro_VC.Priority = 0;
        _curCam = _main_VC;
        _curCam.Priority = 1000;
    }

    public void EnableIntroCamera()
    {
        _main_VC.Priority = 0;
        _curCam = _intro_VC;
        _curCam.Priority = 250;
    }
}
