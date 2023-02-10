using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerSpawner : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public TMP_Dropdown playerCountDropdown;

    public TMP_Text listOfSelectedPlayers;

    public List<GameObject> playersInGame = new List<GameObject>();

    public void SpawnSelectedPlayer()
    {
        int selectedPlayerCount = playerCountDropdown.value + 1;


        for (int i = 0; i < selectedPlayerCount; i++)
        {
            GameObject selectedPlayerPrefabs = playerPrefabs[i];

            Instantiate(selectedPlayerPrefabs);

            
            
            

            //// Add Players to list and display names

            //playersInGame.Add(selectedPlayerPrefabs);

            //string playerList = "";

            //for(int j = 0; j < playersInGame.Count; j++)
            //{
            //    playerList += playersInGame[j].name;
            //    playerList += " ";
            //}

            //listOfSelectedPlayers.text = playerList;
        }
        
    }
}
