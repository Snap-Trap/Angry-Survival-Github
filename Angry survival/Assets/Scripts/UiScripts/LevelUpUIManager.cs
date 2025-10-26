using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpUIManager : MonoBehaviour
{
    // UI Elements
    // This is the panel itself
    public GameObject levelUpPanel;

    public Button optionButton1, optionButton2, optionButton3;

    public TextMeshProUGUI optionText1, optionText2, optionText3;

    // Registered weapons available for upgrades
    private List<BaseWeapon> availableWeapons = new List<BaseWeapon>();

    //public void Start()
    //{
    //    levelUpPanel.SetActive(false);
    //}

    public void Awake()
    {
        levelUpPanel.SetActive(false);
    }

    public void RegisterWeapon(BaseWeapon weapon)
    {
        // Simple null check
        if (weapon == null) return;

        // Only registers a weapon if it isn't already registered
        if (!availableWeapons.Contains(weapon))
        {
            availableWeapons.Add(weapon);
        }
    }

    // Same thing as the one above but for unregistering can you not read you dumb fuck
    public void UnregisterWeapon(BaseWeapon weapon)
    {
        if (weapon == null) return;

        if (availableWeapons.Contains(weapon))
        {
            availableWeapons.Remove(weapon);
        }
    }

    public void ShowLevelUpChoices()
    {
        // If there are no available weapons, do nothing
        if (availableWeapons.Count == 0) return;

        // Pauses le game whilst showing the panel
        Time.timeScale = 0f;
        levelUpPanel.SetActive(true);

        // Grabs a copy of all the registered weapons so I won't fuck up the original list
        List<BaseWeapon> shuffled = new List<BaseWeapon>(availableWeapons);

        // So... this is apparently called a Fisher-Yates shuffle?
        // I had to google a lot to figure this out, but what this does is randomize the order of the weapons
        // Later on, in the int optionsToShow I use this shuffle to determine that only the first 3 wi
        for (int i = 0; i < shuffled.Count; i++)
        {
            var tmp = shuffled[i];
            int rand = Random.Range(i, shuffled.Count);
            shuffled[i] = shuffled[rand];
            shuffled[rand] = tmp;
        }

        // Determine how many options to show (max 3)
        int optionsToShow = Mathf.Min(3, shuffled.Count);

        // Makes it so the clicky stuff is going to be deleted when not needed
        optionButton1.onClick.RemoveAllListeners();
        optionButton2.onClick.RemoveAllListeners();
        optionButton3.onClick.RemoveAllListeners();

        optionButton1.gameObject.SetActive(false);
        optionButton2.gameObject.SetActive(false);
        optionButton3.gameObject.SetActive(false);

        // Decides how many buttons to show based on how many weapons are left
        if (optionsToShow >= 1)
        {
            var weaponOption = shuffled[0];
            optionText1.text = $"{weaponOption.weaponData.weaponName} Upgrade";
            optionButton1.gameObject.SetActive(true);
            optionButton1.onClick.AddListener(() => ChooseWeapon(weaponOption));
        }

        if (optionsToShow >= 2)
        {
            var weaponOption = shuffled[1];
            optionText2.text = $"{weaponOption.weaponData.weaponName} Upgrade";
            optionButton2.gameObject.SetActive(true);
            optionButton2.onClick.AddListener(() => ChooseWeapon(weaponOption));
        }

        if (optionsToShow >= 3)
        {
            var weaponOption = shuffled[2];
            optionText3.text = $"{weaponOption.weaponData.weaponName} Upgrade";
            optionButton3.gameObject.SetActive(true);
            optionButton3.onClick.AddListener(() => ChooseWeapon(weaponOption));
        }

        // L: If future me wants to add more weapons you just need another one of each variables and copy paste this shit but add 1 to every number
    }

    private void ChooseWeapon(BaseWeapon weapon)
    {
        // Simple null check
        if (weapon == null)
        {
            ClosePanel();
            return;
        }

        // Upgras weapon based on weaponname
        weapon.Upgrade();

        ClosePanel();
    }

    private void ClosePanel()
    {
        // Don't have to explain this
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
