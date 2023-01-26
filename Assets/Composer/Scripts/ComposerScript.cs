using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class ComposerScript : MonoBehaviour


{

    public AudioSource a4, 
                       a4Sharp, 
                       b4, 
                       c4, 
                       c4Sharp, 
                       d4, 
                       d4Sharp, 
                       e4,  
                       f4, 
                       f4Sharp, 
                       g4, 
                       g4Sharp;

    public TMP_Text compositionText;
               
    public List<AudioSource> notesInComposition = new List<AudioSource>();

    private IEnumerator PlaySong()
    {
        
        for (int i = 0; i < notesInComposition.Count; i++)
        {
            notesInComposition[i].Play();
            
            Debug.Log(notesInComposition[i].name);

            while(notesInComposition[i].isPlaying == true)
            {
                yield return null;
            }
                
        }
    }

    public void AddNoteToSong(AudioSource note)
    {
        notesInComposition.Add(note);

        string output = "";

        for (int i = 0; i < notesInComposition.Count; i++)
        {
            output += notesInComposition[i].name;
            output += " ";
        }

        compositionText.text = output;
        
        //foreach (var item in notesInComposition)
        //{
        //    Debug.Log(item.name);
        //}
    }

    //public void AddNoteName(string notename)
    //{
    //    compositionText.text = compositionText.text + " " + notename;
    //}

    public void RemoveNoteFromSong()
    {
        notesInComposition.Remove(notesInComposition[notesInComposition.Count - 1]);

        string output = "";

        for (int i = 0; i < notesInComposition.Count; i++)
        {
            output += notesInComposition[i].name;
            output += " ";
        }

        compositionText.text = output;
    }
    
    public void StartSong()
    {
        StartCoroutine(PlaySong());
    }

}
