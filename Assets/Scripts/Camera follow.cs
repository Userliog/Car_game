using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Camerafollow : MonoBehaviour
{
    public GameObject player;
    public Transform followTarget;
    private CinemachineVirtualCamera camera;

    void Start()
    {
        var camera = GetComponent<CinemachineVirtualCamera>();

        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            print("Fond player");
        }
        followTarget = player.transform;
        camera.LookAt = followTarget;
        camera.Follow = followTarget;
    }
}
