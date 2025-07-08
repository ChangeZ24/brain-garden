using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guacaipiao : MonoBehaviour
{   
	public Camera rtCamera;
	public Transform brush;
	RenderTexture renderTexture;
	public Material renderMaterial;

	void Start()
	{
		renderTexture = rtCamera.targetTexture;
		renderMaterial.SetTexture("BlitTex", renderTexture);
		renderMaterial.SetMatrix("paintCameraVP", rtCamera.nonJitteredProjectionMatrix * rtCamera.worldToCameraMatrix);
	}

	/*void OnMouseDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		if (Physics.Raycast(ray, out hitInfo))
		{
			brush.position = hitInfo.point;
			rtCamera.Render();
		}
	}*/

	public Transform playerT;
	private void Update()
	{
		brush.position = playerT.position;
		rtCamera.Render();
	}
}
 