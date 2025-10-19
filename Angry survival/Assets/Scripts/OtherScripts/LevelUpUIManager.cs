using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUIManager : MonoBehaviour
{
    // UI Elements
    // This is the panel itself
    public GameObject levelUpPanel;

    // These are the buttons and texts for the (currently two) weapon options
    public Button optionButton1, optionButton2;
    public TextMeshProUGUI optionText1, optionText2;

    // Makes a list of all the weapons using the BaseWeapon script, which is literally every weapon
    private List<BaseWeapon> availableWeapons = new List<BaseWeapon>();
    private BaseWeapon chosenWeapon1, chosenWeapon2;

    void Start()
    {
        // Turns off the panel at start
        levelUpPanel.SetActive(false);
    }

    public void RegisterWeapon(BaseWeapon weapon)
    {
        // Makes it so there are no duplicates
        if (!availableWeapons.Contains(weapon))
        {
            availableWeapons.Add(weapon);
        }
    }

    public void ShowLevelUpChoices()
    {
        // At the moment you need at least 2 weapons, I can update it to three later but can't rn because I only have 3 weapons and need to test if one can be left out
        if (availableWeapons.Count < 2)
        {
            return;
        }

        // ZA WARUDO!!
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);

        // Pick 2 unique random weapons
        int first = Random.Range(0, availableWeapons.Count);
        int second;
        do { second = Random.Range(0, availableWeapons.Count); } while (second == first);

        chosenWeapon1 = availableWeapons[first];
        chosenWeapon2 = availableWeapons[second];

        // Set UI texts
        optionText1.text = $"{chosenWeapon1.weaponData.weaponName} Upgrade";
        optionText2.text = $"{chosenWeapon2.weaponData.weaponName} Upgrade";

        // Makes it so the buttons do the right thing when clicked
        optionButton1.onClick.RemoveAllListeners();
        optionButton2.onClick.RemoveAllListeners();

        optionButton1.onClick.AddListener(() => ChooseWeapon(chosenWeapon1));
        optionButton2.onClick.AddListener(() => ChooseWeapon(chosenWeapon2));
    }

    private void ChooseWeapon(BaseWeapon weapon)
    {
        weapon.Upgrade();
        ClosePanel();
    }

    private void ClosePanel()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
