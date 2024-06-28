using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownManager : MonoBehaviour
{
    public GameObject targetObject; // Reference to the parent GameObject
    public Dropdown dropdown; // Reference to the Dropdown UI component

    private List<Transform> childTransforms; // List to store child Transforms

    void Start()
    {
        Debug.Log("Start method called.");
        PopulateDropdown(); // Populate dropdown options on start
        ShowSelectedChild(); // Show selected child initially
    }

    void PopulateDropdown()
    {
        if (targetObject != null && dropdown != null)
        {
            Debug.Log("Populating dropdown with child objects of targetObject.");

            // Clear existing options
            dropdown.ClearOptions();
            Debug.Log("Dropdown options cleared.");

            // Get all immediate child objects
            childTransforms = new List<Transform>();
            for (int i = 0; i < targetObject.transform.childCount; i++)
            {
                Transform child = targetObject.transform.GetChild(i);
                childTransforms.Add(child);
                Debug.Log("Added child to list: " + child.name);

                // Add child name to dropdown options
                dropdown.options.Add(new Dropdown.OptionData(child.name));
                Debug.Log("Added child name to dropdown options: " + child.name);
            }

            // Add "Show All" option at the beginning of the dropdown
            dropdown.options.Insert(0, new Dropdown.OptionData("Show All"));
            Debug.Log("Added 'Show All' option to dropdown.");

            // Add listener to dropdown
            dropdown.onValueChanged.AddListener(delegate {
                DropdownValueChanged(dropdown);
            });
            Debug.Log("Added listener to dropdown.");

            // Refresh dropdown
            dropdown.RefreshShownValue();
            Debug.Log("Dropdown options refreshed.");
        }
        else
        {
            Debug.LogWarning("Target object or dropdown not assigned.");
        }
    }

    void DropdownValueChanged(Dropdown dropdown)
    {
        Debug.Log("Dropdown value changed. New value: " + dropdown.value);
        ShowSelectedChild();
    }

    void ShowSelectedChild()
    {
        if (targetObject != null && dropdown != null)
        {
            int selectedIndex = dropdown.value;
            Debug.Log("Selected index: " + selectedIndex);

            // Check if "Show All" option is selected
            if (selectedIndex == 0)
            {
                // Enable all child objects
                foreach (Transform child in childTransforms)
                {
                    child.gameObject.SetActive(true);
                    Debug.Log("Enabled child object: " + child.name);
                }
            }
            else
            {
                // Disable all child objects
                foreach (Transform child in childTransforms)
                {
                    child.gameObject.SetActive(false);
                    Debug.Log("Disabled child object: " + child.name);
                }

                // Enable the selected child object
                if (selectedIndex > 0 && selectedIndex <= childTransforms.Count)
                {
                    Transform selectedChild = childTransforms[selectedIndex - 1]; // Adjust index for "Show All"
                    selectedChild.gameObject.SetActive(true);
                    Debug.Log("Enabled selected child object: " + selectedChild.name);
                }
                else
                {
                    Debug.LogWarning("Selected index out of range: " + selectedIndex);
                }
            }
        }
        else
        {
            Debug.LogWarning("Target object or dropdown not assigned.");
        }
    }
}
