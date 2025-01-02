using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct ShapeImagePair
{
	public string shapeName;
	public Sprite image;
}

public class SchemeManager : MonoBehaviour
{
	public List<ShapeImagePair> shapeImages;
	
    private Dictionary<string, Sprite> shapeImageMap = new Dictionary<string, Sprite>();
    
    public Image displayImage;
    public Image fullScreenImage;
    public Sprite SimpleSprite;
    public float fadeDuration = 1f;
    
    private void OnEnable() 
    {
    	ShapeManager.OnTagChanged += UpdateImage;
    	GameManager.OnSimpleModeChanged += SetImageActive;
    }
    
    private void OnDisable() 
    {
    	ShapeManager.OnTagChanged -= UpdateImage;
    	GameManager.OnSimpleModeChanged -= SetImageActive;
    }
    
    private void Start() 
    {
    	foreach (ShapeImagePair pair in shapeImages)
    		shapeImageMap[pair.shapeName] = pair.image;
    			
    	fullScreenImage.gameObject.SetActive(false);
    }
    
    private void UpdateImage(string shapeKey)
    {
    	if (shapeImageMap.ContainsKey(shapeKey))
    	{
    		displayImage.sprite = shapeImageMap[shapeKey];
    		displayImage.SetNativeSize();
    	}
    }
    
    private void SetImageActive(bool isActive)
    {
    	displayImage.enabled = !isActive;
    	
    	if (isActive)
    		fullScreenImage.sprite = SimpleSprite;
        else 
            fullScreenImage.sprite = displayImage.sprite;
    	
    	StartCoroutine(FadeInAndStayCoroutine());
    }

    private IEnumerator FadeInAndStayCoroutine()
    {    			
    	fullScreenImage.gameObject.SetActive(true);
     
        yield return StartCoroutine(FadeImage(0f, 1f));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(FadeImage(1f, 0f));

        fullScreenImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeImage(float startAlpha, float endAlpha)
    {
        Color color = fullScreenImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            fullScreenImage.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        fullScreenImage.color = color;
    }
}
