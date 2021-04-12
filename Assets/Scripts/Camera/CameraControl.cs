using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float speed = 10.0f;

    private float _offsetX;
    private float _offsetY;
    private float _offsetZ;
    private float _maxY;

    private GameSaved gameSaved;
    
    private void Awake()
    {
        Camera.main.orthographicSize = (float) Screen.height / (float) Screen.width * 10f;
    }

    private void Start()
    {
        gameSaved = GameObject.Find("GameManager").GetComponent<GameManager>().gameSaved;
        _offsetZ = transform.position.z;
        _offsetX = transform.position.x;
        _offsetY = transform.position.y;
        _maxY = gameSaved.countLevelsOpen * 7.4f; // change later
    }

    public static float CamWidth()
    {
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        float camWidth = 2.0f * camHalfWidth;
        return camWidth;
    }
    
    private void LateUpdate()
    {
        // _maxY = data.cavesCounter * 7.4f - 6f;
        if (transform.position.y <= _maxY && transform.position.y >= _offsetY)
            _offsetZ = transform.position.z;
        transform.position = new Vector3(_offsetX, transform.position.y, _offsetZ);
        if (transform.position.y >= _maxY)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(_offsetX, _maxY, _offsetZ), 0.2f);
        }
        if (transform.position.y <= _offsetY)
        {
            transform.position = new Vector3(_offsetX, _offsetY, _offsetZ);
        }
    }
}

