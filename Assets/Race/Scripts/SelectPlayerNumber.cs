using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SelectPlayerNumber : MonoBehaviour
{
    public GameObject[] players;
    public TMP_Dropdown playerNumberDropdown;
    public TMP_Dropdown playerDropdown;

    private List<GameObject> instantiatedPlayers;

    void Start()
    {
        PopulatePlayerNumberDropdown();
    }

    void PopulatePlayerNumberDropdown()
    {
        playerNumberDropdown.ClearOptions();
        playerNumberDropdown.AddOptions(GetPlayerNumberOptions());
    }

    List<string> GetPlayerNumberOptions()
    {
        List<string> playerNumberOptions = new List<string>();
        for (int i = 1; i <= 4; i++)
        {
            playerNumberOptions.Add(i.ToString());
        }
        return playerNumberOptions;
    }

    public void OnPlayerNumberDropdownValueChanged(int value)
    {
        int numberOfPlayers = value + 1;
        instantiatedPlayers = new List<GameObject>();
        for (int i = 0; i < numberOfPlayers; i++)
        {
            GameObject player = Instantiate(players[i]);
            player.name = "Player " + (i + 1);
            instantiatedPlayers.Add(player);
        }
        PopulatePlayerDropdown();
    }

    void PopulatePlayerDropdown()
    {
        playerDropdown.ClearOptions();
        playerDropdown.AddOptions(GetPlayerNames());
    }

    List<string> GetPlayerNames()
    {
        List<string> playerNames = new List<string>();
        foreach (GameObject player in instantiatedPlayers)
        {
            playerNames.Add(player.name);
        }
        return playerNames;
    }

    public void OnPlayerDropdownValueChanged(int value)
    {
        GameObject selectedPlayer = instantiatedPlayers[value];
        // You can add code here to do something with the selected player, such as setting it as the player character or controlling it in some way.
    }

   
}
