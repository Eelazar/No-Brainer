using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDialogue : MonoBehaviour {

    [Header("Punctuation")]
    [Tooltip("The delay after normal letters")]
    public float delay;
    [Tooltip("The delay after commas")]
    public float commaDelay;
    [Tooltip("The delay after dots, question marks, and exclamation marks")]
    public float dotDelay;
    [Tooltip("How long the dialogue stays on screen when it is done")]
    public float displayTime;

    [Header("Audio Assets")]
    public AudioClip[] alphabet = new AudioClip[26];

    //Object references
    private AudioSource source;
    private Text dialogueBox;
    private Image background;

    //The letters that will get skipped when reading
    private char[] nonLetters;
    
    //bools
    [HideInInspector]
    public bool cd;

    // Use this for initialization
    void Start ()
    {
        dialogueBox = gameObject.GetComponent<Text>();
        background = gameObject.GetComponentInChildren<Image>();
        background.enabled = false;
        source = GetComponent<AudioSource>();

        //Assign symbols to be ignored
        nonLetters = new Char[] { '.', ',', '?', '!', '\'', ':', ';', '-', '(', ')', '"', ' ' };
    }

    // Update is called once per frame
    void Update () {
		
	}

    public IEnumerator Speak(string message, float initialDelay, float cooldown)
    {
        //If speaking isn't on cooldown
        if (cd == false)
        {
            //Set it on cooldown, reset the text box, enable the background
            cd = true;
            yield return new WaitForSeconds(initialDelay);
            dialogueBox.text = String.Empty;
            background.enabled = true;

            //For every letter in the text
            for (int i = 0; i < message.Length; i++)
            {
                //If a backlash is found, reset the text box
                if(message[i] == '/')
                {
                    dialogueBox.text = String.Empty;
                }
                else
                {
                    //Add the letter to the textbox
                    dialogueBox.text += message[i];

                    //Check if the letter can be pronounced
                    if (CheckIfLetter(message[i]) == true)
                    {
                        //if it can, play the appropriate audio clip
                        char c = message[i];
                        source.clip = alphabet[GetIndexInAlphabet(c)];
                        source.PlayOneShot(source.clip);
                    }

                    //Assign the correct delays acording to punctuation
                    if (message[i].ToString() == "." || message[i].ToString() == "!" || message[i].ToString() == "?")
                    {
                        yield return new WaitForSeconds(dotDelay);
                    }
                    else if (message[i].ToString() == ",")
                    {
                        yield return new WaitForSeconds(commaDelay);
                    }
                    else
                    {
                        yield return new WaitForSeconds(delay);
                    }
                }
            }

            //Reset the dialogue box and set cooldowns
            yield return new WaitForSeconds(displayTime);
            dialogueBox.text = String.Empty;
            background.enabled = false;
            StartCoroutine(Cooldown(cooldown));
        }
    }

    private static int GetIndexInAlphabet(char value)
    {
        //Check the chars position in thge alphabet
        char upper = char.ToUpper(value);
        if (upper < 'A' || upper > 'Z')
        {
            Debug.Log("Wrong symbol : (" + upper + ")");
            throw new ArgumentOutOfRangeException("value", "This method only accepts standard Latin characters.");
        }

        return (int)upper - (int)'A';
    }

    private bool CheckIfLetter(char letter)
    {
        //Check if the char is one of the non-letters we defined earlier
        bool b = true;
        foreach(char c in nonLetters)
        {
            if(letter == c)
            {
                b = false;
            }
        }
        return b;
    }


    IEnumerator Cooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        cd = false;
    }
}
