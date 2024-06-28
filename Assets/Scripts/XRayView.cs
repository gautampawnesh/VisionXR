using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRayView : MonoBehaviour
{
    public GameObject targetObject; // Reference to the root GameObject
    public Material transparentMaterial; // Reference to the transparent material

    void Start()
    {
        Debug.Log("Start method called.");
        ToggleMaterial();
    }

    public void ToggleMaterial()
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

            // Apply transparent material to all immediate child renderers
            foreach (Transform child in children)
            {
                Debug.Log("Found" + child.name);
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer == null)
                {
                    // If no Renderer component is found on the target object, check its children
                    renderer = child.GetComponentInChildren<Renderer>();
                }
                if (renderer != null)
                {
                    Debug.Log("Renderer found on child object. Changing material to transparent." + renderer.name);
                    renderer.material = transparentMaterial;
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
}
