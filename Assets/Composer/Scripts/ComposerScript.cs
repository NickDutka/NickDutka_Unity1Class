using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
                       
    private List<AudioSource> notesInComposition = new List<AudioSource>();

    private IEnumerator PlaySong()
    {
        //notesInComposition.Add(a4);
        //notesInComposition.Add(a4);
        //notesInComposition.Add(a4Sharp);
        //notesInComposition.Add(a4);
        
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

    public void Adda4NoteToSong()
    {
        notesInComposition.Add(a4);
        Debug.Log(a4);
    }
    public void Addasharp4NoteToSong()
    {
        notesInComposition.Add(a4Sharp);
        Debug.Log(a4Sharp);
    }

    public void StartSong()
    {
        StartCoroutine(PlaySong());
    }

    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            notesInComposition.Add(a4);
            a4.Play();
        }
        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            notesInComposition.Add(a4Sharp);
            a4Sharp.Play();
        }
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            notesInComposition.Add(b4);
            b4.Play();
        }
        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            notesInComposition.Add(c4);
            c4.Play();
        }
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            notesInComposition.Add(c4Sharp);
            c4Sharp.Play();
        }
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            notesInComposition.Add(d4);
            d4.Play();
        }
        if (Keyboard.current.tKey.wasPressedThisFrame)
        {
            notesInComposition.Add(d4Sharp);
            d4Sharp.Play();
        }
        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            notesInComposition.Add(e4);
            e4.Play();
        }
        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            notesInComposition.Add(f4);
            f4.Play();
        }
        if (Keyboard.current.uKey.wasPressedThisFrame)
        {
            notesInComposition.Add(f4Sharp);
            f4Sharp.Play();
        }
        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            notesInComposition.Add(g4);
            g4.Play();
        }
        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            notesInComposition.Add(g4Sharp);
            g4Sharp.Play();
        }
    }
}
