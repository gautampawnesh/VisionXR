using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultiSelectDropdown : MonoBehaviour
{
    public Dropdown dropdown; // Reference to the Dropdown UI component
    private List<Dropdown.OptionData> options; // List to store the dropdown options
    private List<int> selectedIndices; // List to store selected indices

    void Start()
    {
        options = new List<Dropdown.OptionData>(dropdown.options); // Initialize options list
        selectedIndices = new List<int>(); // Initialize selected indices list

        // Add listener to dropdown
        dropdown.onValueChanged.AddListener(delegate {
            OnDropdownValueChanged(dropdown);
        });
    }

    void OnDropdownValueChanged(Dropdown dropdown)
    {
        int selectedIndex = dropdown.value;

        // Toggle selection state
        if (selectedIndices.Contains(selectedIndex))
        {
            selectedIndices.Remove(selectedIndex);
        }
        else
        {
            selectedIndices.Add(selectedIndex);
        }

        UpdateDropdownLabel();
    }

    void UpdateDropdownLabel()
    {
        string label = "Selected: ";
        foreach (int index in selectedIndices)
        {
            label += options[index].text + ", ";
        }
        label = label.TrimEnd(',', ' ');
        dropdown.captionText.text = label;
    }

    public List<int> GetSelectedIndices()
    {
        return new List<int>(selectedIndices);
    }
}
