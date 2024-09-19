using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset; // Offset from the player's position

    private void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
        transform.LookAt(playerTransform); // Keep the camera looking at the player
    }
}
