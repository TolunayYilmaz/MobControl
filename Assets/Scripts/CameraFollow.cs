using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float cameraDistance = 5.0f;
    public float cameraHeight = 2.0f;
    public float cameraRotationDamping = 3.0f;
    public float cameraHeightDamping = 2.0f;

    void LateUpdate()
    {
        if (target)
        {
            // Hedef cismi takip etmek i�in pozisyonu hesapla
            Vector3 targetPosition = target.position - target.forward * cameraDistance;
            targetPosition.y += cameraHeight;

            // Kameran�n pozisyonunu yumu�at�lm�� bir lerp ile g�ncelle
            Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * cameraHeightDamping);
            transform.position = newPosition;

            // Kameran�n rotasyonunu takip edilen cismin rotasyonuna g�re g�ncelle
            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position, target.up);
            Quaternion newRotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * cameraRotationDamping);
            transform.rotation = newRotation;
        }
    }

}

