using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShapeAnimator : MonoBehaviour
{
    public Image triangle;
    public Image pentagon;

    public float rotationSpeed = 180f;  
    public int triangleRepetitions = 3; 
    public int pentagonRepetitions = 5; 

    private void Start()
    {
        StartCoroutine(CycleRotation());
    }

    private IEnumerator CycleRotation()
    {
        while (true)
        {
            yield return RotateAndPause(triangle, 120, triangleRepetitions);
            yield return RotateAndPause(triangle, -120, triangleRepetitions);

            yield return ChangeSize(triangle, pentagon);

            yield return RotateAndPause(pentagon, 72, pentagonRepetitions);
            yield return RotateAndPause(pentagon, -72, pentagonRepetitions);

            yield return ChangeSize(pentagon, triangle);
        }
    }

    private IEnumerator RotateAndPause(Image shape, float angleStep, int repetitions)
    {
        for (int i = 0; i < repetitions; i++)
        {
            float initialRotation = shape.rectTransform.eulerAngles.z;
            float targetRotation = initialRotation + angleStep;

            while (Mathf.Abs(Mathf.DeltaAngle(shape.rectTransform.eulerAngles.z, targetRotation)) > 0.1f)
            {
                float newAngle = Mathf.MoveTowardsAngle(
                    shape.rectTransform.eulerAngles.z,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
                shape.rectTransform.rotation = Quaternion.Euler(0, 0, newAngle);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ChangeSize(Image shrinking, Image growing)
    {
        float duration = 1f; 
        float time = 0f;

        Vector3 initialShrinkScale = shrinking.rectTransform.localScale;
        Vector3 targetShrinkScale = Vector3.zero;

        Vector3 initialGrowScale = Vector3.zero;
        Vector3 targetGrowScale = Vector3.one;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            shrinking.rectTransform.localScale = Vector3.Lerp(initialShrinkScale, targetShrinkScale, t);
            growing.rectTransform.localScale = Vector3.Lerp(initialGrowScale, targetGrowScale, t);

            yield return null;
        }
    }
}