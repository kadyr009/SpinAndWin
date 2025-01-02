using UnityEngine;

public class MakePoints : MonoBehaviour
{
    public Transform[] points = new Transform[3]; 
    public float distanceFromShape = 2f; 

    private void Awake()
    {
        CreatePointsAroundShape();
    }

    private void CreatePointsAroundShape()
    {
        float angleOffset = gameObject.tag == "Plane" ? 0f : -90;
        
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = new GameObject("Point" + (i + 1)).transform;

            float angle = angleOffset * Mathf.Deg2Rad;
            Vector3 pointPosition = transform.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distanceFromShape;
            points[i].position = pointPosition;
            
            angleOffset += 360f / points.Length; 
        }
    }
}

