using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public TMP_Dropdown playerCountDropdown;

    public void SpawnSelectedPlayer()
    {
        int selectedPlayerCount = playerCountDropdown.value + 1;

        for (int i = 0; i < selectedPlayerCount; i++)
        {
            GameObject selectedPlayerPrefab = playerPrefabs[i];
            Instantiate(selectedPlayerPrefab);
        }
        
    }
}
