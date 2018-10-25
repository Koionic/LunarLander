using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    PlayerInfo playerInfo;

    [SerializeField] GameObject[] camObjs;

    [SerializeField] GameObject cam1Controller;
    [SerializeField] GameObject cam2Controller;
    [SerializeField] GameObject cam3Controller;
    [SerializeField] GameObject cam4Controller;
    [SerializeField] GameObject cam5Controller;

    Camera[] cams = new Camera[5];

    Camera playerCam1;
    Camera playerCam2;
    Camera playerCam3;
    Camera playerCam4;

    [SerializeField] Animator mainMenuAnimator;

    bool camera1 = false;
    bool camera2 = false;
    bool camera3 = false;
    bool camera4 = false;

    List<GameObject> rejectedCameras = new List<GameObject>(4);

    Cinemachine.CinemachineVirtualCamera cam1Virtual;
    Cinemachine.CinemachineVirtualCamera cam2Virtual;
    Cinemachine.CinemachineVirtualCamera cam3Virtual;
    Cinemachine.CinemachineVirtualCamera cam4Virtual;

    Rect fullScreen = new Rect (0f,0f,1f,1f);
    Rect topHalf = new Rect(0f, 0.5f, 1f, 1f);
    Rect bottomHalf = new Rect(0f, -0.5f, 1f, 1f);
    Rect topLeft = new Rect(-0.5f, 0.5f, 1f, 1f);
    Rect topRight = new Rect(0.5f, 0.5f, 1f, 1f);
    Rect bottomLeft = new Rect(-0.5f, -0.5f, 1f, 1f);
    Rect bottomRight = new Rect(0.5f, -0.5f, 1f, 1f);

    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;

    int playerCount;

    void Start ()
    {
        if (SceneManager.GetActiveScene().name != "Main Menu")
        {
            playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
            playerCount = playerInfo.GetNumberOfPlayers();


            for (int i = 0; i < 5; i++)
            {
                cams[i] = camObjs[i].GetComponent<Camera>();
            }

            cam1Virtual = cam1Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            cam2Virtual = cam2Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            cam3Virtual = cam3Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();
            cam4Virtual = cam4Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();

            AssignCameras();

            if (playerCount == 1)
                OneCamera();
            if (playerCount == 2)
                TwoCameras();
            if (playerCount == 3)
                ThreeCameras();
            if (playerCount == 4)
                FourCameras();

            ClearUnusedCameras();
        }
    }

    void AssignCameras()
    {
        for (int i = 0; i < 4; i++)
        {
            if (playerInfo.GetJoystick(i) == 0)
            {
                Debug.Log("Camera " + (i + 1) + " isnt needed");
                rejectedCameras.Add(camObjs[i]);
            }
            else
            {
                if (!camera1)
                {
                    Debug.Log("Camera 1 set by joystick " + playerInfo.GetJoystick(i));
                    playerCam1 = cams[i];
                    camera1 = true;
                }
                else if (!camera2)
                {
                    Debug.Log("Camera 2 set by joystick " + playerInfo.GetJoystick(i));
                    playerCam2 = cams[i];
                    camera2 = true;
                }
                else if (!camera3)
                {
                    Debug.Log("Camera 3 set by joystick " + playerInfo.GetJoystick(i));
                    playerCam3 = cams[i];
                    camera3 = true;
                }
                else if (!camera4)
                {
                    Debug.Log("Camera 4 set by joystick " + playerInfo.GetJoystick(i));
                    playerCam4 = cams[i];
                    camera4 = true;
                }
            }
                    
        }
    }

    void OneCamera()
    {
        playerCam1.rect = fullScreen;

        rejectedCameras.Add(camObjs[4]);
    }

    void TwoCameras()
    {
        playerCam1.rect = topHalf;
        playerCam2.rect = bottomHalf;

        rejectedCameras.Add(camObjs[4]);
    }

    void ThreeCameras()
    {
        playerCam1.rect = topLeft;
        playerCam2.rect = topRight;
        playerCam3.rect = bottomLeft;
        cams[4].rect = bottomRight;
    }

    void FourCameras()
    {
        playerCam1.rect = topLeft;
        playerCam2.rect = topRight;
        playerCam3.rect = bottomLeft;
        playerCam4.rect = bottomRight;

        rejectedCameras.Add(camObjs[4]);
    }

    void ClearUnusedCameras()
    {
        foreach (GameObject rejectedCamera in rejectedCameras)
        {
            rejectedCamera.SetActive(false);
        }
    }

    public void LobbyToMain()
    {
        mainMenuAnimator.Play("Lobby-Main");
    }
}
