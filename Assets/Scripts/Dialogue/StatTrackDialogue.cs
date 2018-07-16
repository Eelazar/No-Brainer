using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTrackDialogue : MonoBehaviour
{

    public enum Stat { Time, Deaths }

    [Tooltip("The reference to the dialogue master, found in the canvas")]
    public GameObject dialogueMaster;
    [Tooltip("The message you want spoken")]
    [TextArea(1, 5)]
    public string message;
    [Space]
    [Header("Conditions")]
    [Tooltip("Which stat should be tracked by the script")]
    public Stat trackedStat;
    [Tooltip("Amount of stat counts after which dialogue gets triggered")]
    public float statAmount;

    private float statCounter;
    private bool triggered;

    void Update()
    {
        if(triggered == false)
        {
            switch (trackedStat)
            {
                case Stat.Time:
                    statCounter += 1 * Time.deltaTime;
                    break;
                case Stat.Deaths:
                    statCounter = PlayerPrefs.GetFloat("Deaths");
                    break;
                default:
                    break;
            }
        }
        

        if (statCounter >= statAmount && triggered == false)
        {
            Trigger();
        }
    }

    void Trigger()
    {
        //Start the Speak coroutine in the DialogueMaster
        StartCoroutine(dialogueMaster.GetComponent<ScreenDialogue>().Speak(message, 0));
        triggered = true;
    }
}
