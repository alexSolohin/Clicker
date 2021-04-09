﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    // [SerializeField] private CaveData data;
    public float speed = 10.0f;

    private float _offsetX;
    private float _offsetY;
    private float _offsetZ;
    private float _maxY;
    
    private void Awake()
    {
        Camera.main.orthographicSize = (float)Screen.height / (float)Screen.width * 10f;
        
        float screenAspect = (float) Screen.width / (float) Screen.height;
        float camHalfHeight = Camera.main.orthographicSize;
        float camHalfWidth = screenAspect * camHalfHeight;
        GameManager.Instance.camWidth = 2.0f * camHalfWidth;
    }

    private void Start()
    {
        _offsetZ = transform.position.z;
        _offsetX = transform.position.x;
        _offsetY = transform.position.y;
        _maxY = 1000; // change later
        // _maxY = data.cavePrefabs.Count * data.iteration * 7.4f;
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