using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    PlayerInfo playerInfo;

    [SerializeField] GameObject cam1GO;
    [SerializeField] GameObject cam2GO;
    [SerializeField] GameObject cam3GO;
    [SerializeField] GameObject cam4GO;
    [SerializeField] GameObject cam5GO;

    [SerializeField] GameObject cam1Controller;
    [SerializeField] GameObject cam2Controller;
    [SerializeField] GameObject cam3Controller;
    [SerializeField] GameObject cam4Controller;
    [SerializeField] GameObject cam5Controller;

    Camera cam1;
    Camera cam2;
    Camera cam3;
    Camera cam4;
    Camera cam5;

    Cinemachine.CinemachineVirtualCamera cam1Virtual;
    Cinemachine.CinemachineVirtualCamera cam2Virtual;
    Cinemachine.CinemachineVirtualCamera cam3Virtual;
    Cinemachine.CinemachineVirtualCamera cam4Virtual;
    Cinemachine.CinemachineVirtualCamera cam5Virtual;

    Rect fullScreen = new Rect (0f,0f,1f,1f);
    Rect leftHalf = new Rect(-0.5f, 0f, 1f, 1f);
    Rect rightHalf = new Rect(0.5f, 0f, 1f, 1f);
    Rect topLeft = new Rect(-0.5f, 0.5f, 1f, 1f);
    Rect topRight = new Rect(0.5f, 0.5f, 1f, 1f);
    Rect bottomLeft = new Rect(-0.5f, -0.5f, 1f, 1f);
    Rect bottomRight = new Rect(0.5f, -0.5f, 1f, 1f);

    GameObject player1;
    GameObject player2;
    GameObject player3;
    GameObject player4;

    int playerCount;

    void Awake()
    {
        playerInfo = GameObject.Find("PlayerInfo").GetComponent<PlayerInfo>();
        playerCount = playerInfo.GetNumberOfPlayers();

        cam1 = cam1GO.GetComponent<Camera>();
        cam2 = cam2GO.GetComponent<Camera>();
        cam3 = cam3GO.GetComponent<Camera>();
        cam4 = cam4GO.GetComponent<Camera>();

        cam1Virtual = cam1Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cam2Virtual = cam1Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cam3Virtual = cam1Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();
        cam4Virtual = cam1Controller.GetComponent<Cinemachine.CinemachineVirtualCamera>();
    }

    void Start ()
    {
        if (playerCount == 1)
            OneCamera();
        if (playerCount == 2)
            TwoCameras();
        if (playerCount == 3)
            ThreeCameras();
        if (playerCount == 4)
            FourCameras();
    }

    void OneCamera()
    {
        cam1.rect = fullScreen;

        //cam1Virtual.Follow = player1.transform;

        cam2GO.SetActive(false);
        cam3GO.SetActive(false);
        cam4GO.SetActive(false);
        cam5GO.SetActive(false);
    }

    void TwoCameras()
    {
        cam1.rect = leftHalf;
        cam2.rect = rightHalf;

        //cam1Virtual.Follow = player1.transform;
        //cam2Virtual.Follow = player2.transform;

        cam3GO.SetActive(false);
        cam4GO.SetActive(false);
        cam5GO.SetActive(false);
    }

    void ThreeCameras()
    {
        cam1.rect = topLeft;
        cam2.rect = topRight;
        cam3.rect = bottomLeft;
        cam5.rect = bottomRight;

        //cam1Virtual.Follow = player1.transform;
        //cam2Virtual.Follow = player2.transform;
        //cam3Virtual.Follow = player3.transform;

        cam4GO.SetActive(false);
    }

    void FourCameras()
    {
        cam1.rect = topLeft;
        cam2.rect = topRight;
        cam3.rect = bottomLeft;
        cam4.rect = bottomRight;

        //cam1Virtual.Follow = player1.transform;
        //cam2Virtual.Follow = player2.transform;
        //cam3Virtual.Follow = player3.transform;
        //cam4Virtual.Follow = player4.transform;

        cam5GO.SetActive(false);
    }
}
