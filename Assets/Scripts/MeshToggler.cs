using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshToggler : MonoBehaviour
{
    public GameObject targetObject; // Reference to the root GameObject
    public Material wireframeMaterial; // Reference to the wireframe material
    public Button toggleButton; // Reference to the UI button
    public Color onColor = Color.green; // Color when XRay is on
    public Color offColor = Color.red; // Color when XRay is off
    private bool xRayToggle;
    private Dictionary<Renderer, Material[]> originalMaterialsDict; // Dictionary to store original materials

    void Start()
    {
        Debug.Log("Start method called.");
        xRayToggle = false;
        originalMaterialsDict = new Dictionary<Renderer, Material[]>(); // Initialize dictionary
        UpdateButtonColor(); // Set initial button color
    }

    public void XRayView()
    {
        Debug.Log("Xray toggle value: " + xRayToggle);
        xRayToggle = !xRayToggle;

        if (xRayToggle)
        {
            ToggleWireframe(); // Toggle wireframe on
        }
        else
        {
            RestoreOriginalMaterials(); // Toggle wireframe off and restore original materials
        }

        UpdateButtonColor(); // Update button color based on toggle value
    }

    private void ToggleWireframe()
    {
        if (targetObject != null)
        {
            Debug.Log("Target object found.");

            // Get all immediate child objects
            Transform[] children = new Transform[targetObject.transform.childCount];
            for (int i = 0; i < targetObject.transform.childCount; i++)
            {
                children[i] = targetObject.transform.GetChild(i);
            }

            // Apply wireframe material to all immediate child renderers
            foreach (Transform child in children)
            {
                Renderer renderer = child.GetComponentInChildren<Renderer>();
                if (renderer != null)
                {
                    Debug.Log("Renderer found on child object. Applying wireframe material: " + renderer.name);

                    // Store original materials before applying wireframe material
                    if (!originalMaterialsDict.ContainsKey(renderer))
                    {
                        originalMaterialsDict.Add(renderer, renderer.materials);
                    }

                    // Apply wireframe material
                    Material[] materials = new Material[renderer.materials.Length];
                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i] = wireframeMaterial;
                    }
                    renderer.materials = materials;
                }
                else
                {
                    Debug.Log("Renderer not found on child object: " + child.name);
                }
            }
        }
        else
        {
            Debug.Log("Target object not assigned.");
        }
    }

    private void RestoreOriginalMaterials()
    {
        if (targetObject != null)
        {
            Debug.Log("Restoring original materials.");

            // Restore original materials for all renderers
            foreach (KeyValuePair<Renderer, Material[]> entry in originalMaterialsDict)
            {
                Renderer renderer = entry.Key;
                Material[] originalMaterials = entry.Value;

                if (renderer != null && originalMaterials != null)
                {
                    renderer.materials = originalMaterials; // Restore original materials
                }
            }

            originalMaterialsDict.Clear(); // Clear dictionary after restoration
        }
        else
        {
            Debug.Log("Target object not assigned.");
        }
    }

    private void UpdateButtonColor()
    {
        if (toggleButton != null)
        {
            ColorBlock colorBlock = toggleButton.colors;
            colorBlock.normalColor = xRayToggle ? onColor : offColor;
            colorBlock.highlightedColor = xRayToggle ? onColor : offColor;
            toggleButton.colors = colorBlock;
            Debug.Log("Button color updated to: " + (xRayToggle ? "onColor" : "offColor"));
        }
        else
        {
            Debug.LogWarning("Toggle button not assigned.");
        }
    }
}
