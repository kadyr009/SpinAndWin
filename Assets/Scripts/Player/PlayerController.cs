using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float rotationAngle = 120f;
    public float rotationDuration = 0.5f;

    private bool isRotating = false;
    private Coroutine currentRotationCoroutine;

    private void OnEnable() 
    {
    	ShapeManager.OnShapeChanged += CalculateRotationAngle;
    }
    
    private void OnDisable() 
    {
    	ShapeManager.OnShapeChanged -= CalculateRotationAngle;
    }
    
    private void CalculateRotationAngle()
    {
        if (currentRotationCoroutine != null)
        {
            StopCoroutine(currentRotationCoroutine);
            currentRotationCoroutine = null;
            isRotating = false;
        }

        transform.rotation = Quaternion.identity;

    	MakePoints[] allPoints = gameObject.GetComponentsInChildren<MakePoints>();

        foreach (var point in allPoints)
        {
            if (point.gameObject.activeInHierarchy)
            {
                rotationAngle = 360f / point.points.Length; 
                break; 
            } 
        }
    }
   
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            StartRotate("Left"); 
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) 
        {
            StartRotate("Right"); 
        }
    }

    public void StartRotate(string direction)
    {
        if (direction == "Left"  && !isRotating)
            currentRotationCoroutine = StartCoroutine(Rotate(Vector3.down));
        else if (direction == "Right"  && !isRotating)
            currentRotationCoroutine = StartCoroutine(Rotate(Vector3.up));

    }

    private IEnumerator Rotate(Vector3 direction)
    {
        isRotating = true;

        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(direction * rotationAngle);
        
        float timeElapsed = 0f;

        while (timeElapsed < rotationDuration)
        {
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, timeElapsed / rotationDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }
}
