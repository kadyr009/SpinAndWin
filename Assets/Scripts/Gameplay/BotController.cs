using UnityEngine;
using System.Collections;
using System;

public class BotController : MonoBehaviour
{
    private Transform shape; 
    private Transform[] points; 
    private Transform targetPoint; 
    
    public static float speed = 3f; 
    public static float speedTemp = 0f;
    
    public static event Action OnStartNormalize;
    
    private void OnEnable() 
    {
    	FreezeBuff.OnFreezeBuffApplied += Freeze;
    	AccelerationDeBuff.OnAccelerationDeBuffApplied += Accelerate;
    }
    
    private void OnDisable() 
    {
    	FreezeBuff.OnFreezeBuffApplied -= Freeze;
    	AccelerationDeBuff.OnAccelerationDeBuffApplied -= Accelerate;
    }
    
    private void Start()
    {
        GameObject player = GameObject.Find("P_Player(Clone)");

        if (player != null)
        {
            shape = player.transform;
            
            MakePoints[] allShapePoints = shape.GetComponentsInChildren<MakePoints>();
 
            foreach (var shapePoint in allShapePoints)
            {
                if (shapePoint.gameObject.activeInHierarchy)
                {
                    points = shapePoint.points; 
                    break; 
                }
            }

            SetClosestPointAsTarget();
            SetRotation();
        }
    }

    private void Update()
    {
        if (targetPoint != null)
        {
            MoveTowardsTarget();
        }
    }

    private void SetClosestPointAsTarget()
    {
        if (points == null || points.Length == 0) return;

        float closestDistance = Mathf.Infinity;

        foreach (Transform point in points)
        {
            float distance = Vector3.Distance(transform.position, point.position);
            
            if (distance < closestDistance)
            {
                closestDistance = distance;
                targetPoint = point;
            }
        }
    }
    
    private void SetRotation()
    {
        if (points == null || points.Length == 0) return;

    	Vector3 direction = (targetPoint.position - transform.position).normalized;
        Quaternion rotationToTarget = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotationToTarget;
    }

    private void MoveTowardsTarget()
    {
        // Двигаем бота к целевой точке
        Vector3 direction = (targetPoint.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Если бот достиг точки, остановить движение
        if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
        {
            targetPoint = null; // Останавливаем движение
        }
    }
    
    private void Freeze()
    {
        if (speedTemp == 0f)
    	    speedTemp = speed;

    	speed = Math.Max(1f, speed / 2f);
 
    	OnStartNormalize?.Invoke();
    }
    
    private void Accelerate()
    {
        if (speedTemp == 0f)
    	    speedTemp = speed;

    	speed = Math.Max(1f, speed * 1.5f);
    	
    	OnStartNormalize?.Invoke();
    }
}

