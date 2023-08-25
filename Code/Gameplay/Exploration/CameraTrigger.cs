using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera camera;

    private void Start()
    {
        camera.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        camera.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        camera.gameObject.SetActive(false);
    }
}
