/*
 * Jacob Vanderwill
 * Created: 3/11/2025
 * Last Altered: 3/11/2025
 * Create a script that will give the camera edge a collider to keep the player within the camera view
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    // Thickness of the colliders
    public float thickness = 1f;
    private Camera cam;
    private GameObject leftBorder, rightBorder, topBorder, bottomBorder;

    void Start()
    {
        cam = Camera.main;
        if (cam == null)
        {
            Debug.LogError("Main Camera not found!");
            return;
        }

        // Ensure correct position at start
        CreateBorders();
        UpdateBorders(); 
    }

    void LateUpdate()
    {
        // Keep borders following the camera
        UpdateBorders(); 
    }

    void CreateBorders()
    {
        leftBorder = CreateCollider("Left Border");
        rightBorder = CreateCollider("Right Border");
        topBorder = CreateCollider("Top Border");
        bottomBorder = CreateCollider("Bottom Border");
    }

    void UpdateBorders()
    {
        float screenHeight = 2f * cam.orthographicSize;
        float screenWidth = screenHeight * cam.aspect;
        Vector2 cameraPos = cam.transform.position;

        leftBorder.transform.position = new Vector2(cameraPos.x - screenWidth / 2 - thickness / 2, cameraPos.y);
        rightBorder.transform.position = new Vector2(cameraPos.x + screenWidth / 2 + thickness / 2, cameraPos.y);
        topBorder.transform.position = new Vector2(cameraPos.x, cameraPos.y + screenHeight / 2 + thickness / 2);
        bottomBorder.transform.position = new Vector2(cameraPos.x, cameraPos.y - screenHeight / 2 - thickness / 2);

        leftBorder.GetComponent<BoxCollider2D>().size = new Vector2(thickness, screenHeight);
        rightBorder.GetComponent<BoxCollider2D>().size = new Vector2(thickness, screenHeight);
        topBorder.GetComponent<BoxCollider2D>().size = new Vector2(screenWidth, thickness);
        bottomBorder.GetComponent<BoxCollider2D>().size = new Vector2(screenWidth, thickness);
    }

    GameObject CreateCollider(string name)
    {
        GameObject border = new GameObject(name);
        border.transform.parent = transform; // Make sure the borders dont clog the hierarchy
        BoxCollider2D collider = border.AddComponent<BoxCollider2D>();
        return border;
    }
}
