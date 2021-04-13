using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCanvas : MonoBehaviour
{
	public Canvas canvas;

	private RectTransform rect;
	private void Start()
	{
		rect = GetComponent<RectTransform>();
		rect.sizeDelta = new Vector2(CameraControl.CamWidth(), 1000);
	}
}
