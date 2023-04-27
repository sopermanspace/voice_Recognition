using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlendshapeController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public List<string> blendshapeNames = new List<string>();
    public float blendshapeSpeed = 1.0f;

    private List<float> blendshapeWeights = new List<float>();
    private float targetBlendshapeWeight = 0.0f;
    private int blendshapeIndex = 0;
    private bool isBlinking = false;
    private bool isSmiling = false;

    void Start()
    {
        // Initialize blendshape names and weights
        for (int i = 0; i < skinnedMeshRenderer.sharedMesh.blendShapeCount; i++)
        {
            blendshapeNames.Add(skinnedMeshRenderer.sharedMesh.GetBlendShapeName(i));
            blendshapeWeights.Add(0.0f);
        }
    }

    IEnumerator Blink()
    {
        // Set eye blendshape index
        int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendshapeNames[16]);

        // Set blendshape weight to 100
        blendshapeWeights[16] = 100f;
        skinnedMeshRenderer.SetBlendShapeWeight(index, blendshapeWeights[16]);

        // Wait for a random duration between 0.1 and 0.5 seconds
        float blinkDuration = Random.Range(0.1f, 0.5f);
        yield return new WaitForSeconds(blinkDuration);

        // Set blendshape weight back to 0
        blendshapeWeights[16] = 0f;
        skinnedMeshRenderer.SetBlendShapeWeight(index, blendshapeWeights[16]);

        // Reset flag
        isBlinking = false;
    }

    IEnumerator Smile()
    {
        // Set smile blendshape index
        int index = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(blendshapeNames[33]);

        // Set blendshape weight to 100
        blendshapeWeights[33] = 100f;
        skinnedMeshRenderer.SetBlendShapeWeight(index, blendshapeWeights[33]);

        // Wait for a random duration between 0.5 and 1.5 seconds
        float smileDuration = Random.Range(0.5f, 1.5f);
        yield return new WaitForSeconds(smileDuration);

        // Set blendshape weight back to 0
        blendshapeWeights[33] = 0f;
        skinnedMeshRenderer.SetBlendShapeWeight(index, blendshapeWeights[33]);

        // Reset flag
        isSmiling = false;
    }

    void Update()
    {
        // Check if blinking coroutine is running
        if (!isBlinking)
        {
            // Start blinking coroutine every 5 seconds
            StartCoroutine(Blink());
            isBlinking = true;
            Invoke("ResetBlink", 5.0f);
        }

        // Check if smiling coroutine is running
        if (!isSmiling)
        {
            // Start smiling coroutine every 3 seconds
            StartCoroutine(Smile());
            isSmiling = true;
            Invoke("ResetSmile", 3.0f);
        }
    }

    void ResetBlink()
    {
        isBlinking = false;
    }

    void ResetSmile()
    {
        isSmiling = false;
    }
}





//16  Eye Blink
//30 
//33 mouth smile
//42 mouth widen
//55 
//60 mouth down
//67 mouth open
//104 mouth close

