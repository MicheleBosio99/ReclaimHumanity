using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraMovementTutorial : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera pixelPerfectCamera;
    [SerializeField] private Vector2 targetPosition;
    [SerializeField] private float movementDuration = 5.0f;
    
    private Vector3 initialCameraPosition;

    void Start() { 
        if (pixelPerfectCamera != null) { initialCameraPosition = pixelPerfectCamera.transform.position; }
        if (pixelPerfectCamera != null) { Debug.Log("HERE"); StartCoroutine(MoveCameraSmoothly()); }
    }

    IEnumerator MoveCameraSmoothly() {
        
        var elapsedTime = 0.0f;
        var startingPos = initialCameraPosition;

        while (elapsedTime < movementDuration) {
            pixelPerfectCamera.transform.position = Vector3.Lerp(startingPos, targetPosition, (elapsedTime / movementDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        pixelPerfectCamera.transform.position = targetPosition;

        // Move back after a delay (for tutorial purposes)
        yield return new WaitForSeconds(2.0f);

        elapsedTime = 0;
        while (elapsedTime < movementDuration) {
            pixelPerfectCamera.transform.position = Vector3.Lerp(targetPosition, startingPos, (elapsedTime / movementDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        pixelPerfectCamera.transform.position = startingPos;
    }
}