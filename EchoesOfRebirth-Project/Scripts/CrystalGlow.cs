using UnityEngine;

public class CrystalGlow : MonoBehaviour
{
    public Transform player;
    public float minDistance = 2f;  // Max brightness distance
    public float maxDistance = 15f; // Glow turns off at this distance
    public float maxIntensity = 5f; // How bright the glow is

    private Material crystalMat;
    private Color baseEmissionColor;

    void Start()
    {
        // Get the material from the renderer
        crystalMat = GetComponent<Renderer>().material;
        
        // Store the starting emission color
        baseEmissionColor = crystalMat.GetColor("_EmissionColor");

        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        // Calculate a 0 to 1 value (1 is close, 0 is far)
        float lerpFactor = 1f - Mathf.InverseLerp(minDistance, maxDistance, distance);

        // Apply intensity to the color
        Color finalColor = baseEmissionColor * Mathf.LinearToGammaSpace(lerpFactor * maxIntensity);
        
        // Update the material
        crystalMat.SetColor("_EmissionColor", finalColor);
        
        // Tells Unity to update the lighting in the scene
        DynamicGI.SetEmissive(GetComponent<Renderer>(), finalColor);
    }
}