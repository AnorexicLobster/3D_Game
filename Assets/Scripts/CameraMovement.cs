using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private float distance = 5f;
    [SerializeField] private float height = 2f;
    [SerializeField] private float smoothSpeed = 5f;
    [SerializeField] private float rotationSpeed = 3f;
    [SerializeField] private float rotationAmount = 0f;
    [SerializeField] private float verticalAngle = 20f; 

    private float rotationY;


    void Start()
    {
        rotationY = player.eulerAngles.y;
    }

    void Update()
    {
        // Rotates the camera to a fixed vertical posistion.
        rotationY = player.eulerAngles.y + rotationAmount;
        Quaternion targetRotation = Quaternion.Euler(verticalAngle, rotationY, 0);

        // Allows the camera to follow the player.
        Vector3 targetPosition = player.position - (targetRotation * Vector3.forward * distance) + Vector3.up * height;

        // Ensures that the camera moves and rotates smoothly.
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
