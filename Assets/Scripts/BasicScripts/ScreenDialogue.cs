using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenDialogue : MonoBehaviour {

    public Text dialogueBox;
    public float delay;
    public float commaDelay;
    public float dotDelay;
    public float displayTime;

    public AudioClip[] alphabet = new AudioClip[26];

    private AudioSource source;
    private char[] nonLetters;
    private bool cd;

    // Use this for initialization
    void Start ()
    {
        dialogueBox.gameObject.SetActive(false);
        source = GetComponent<AudioSource>();

        nonLetters = new Char[] { '.', ',', '?', '!', '\'', ':', ';', '-', '(', ')', '"', ' ' };
    }

    // Update is called once per frame
    void Update () {
		
	}

    public IEnumerator Speak(string message, float cooldown)
    {
        if (cd == false)
        {
            cd = true;
            dialogueBox.text = "";
            dialogueBox.gameObject.SetActive(true);
            for (int i = 0; i < message.Length; i++)
            {
                if(message[i] == '/')
                {
                    dialogueBox.text = "";
                }
                else
                {
                    dialogueBox.text += message[i];

                    if (CheckIfLetter(message[i]) == true)
                    {
                        char c = message[i];
                        source.clip = alphabet[GetIndexInAlphabet(c)];
                        source.PlayOneShot(source.clip);
                    }

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

            yield return new WaitForSeconds(displayTime);
            dialogueBox.text = "";
            dialogueBox.gameObject.SetActive(false);
            StartCoroutine(Cooldown(cooldown));
        }
    }

    private static int GetIndexInAlphabet(char value)
    {
        // Uses the uppercase character unicode code point. 'A' = U+0042 = 65, 'Z' = U+005A = 90
        char upper = char.ToUpper(value);
        if (upper < 'A' || upper > 'Z')
        {
            throw new ArgumentOutOfRangeException("value", "This method only accepts standard Latin characters.");
        }

        return (int)upper - (int)'A';
    }

    private bool CheckIfLetter(char letter)
    {
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
