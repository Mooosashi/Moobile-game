using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;

public class DialogueInkExternalFunctions
{
    public void Bind(Story currentStory)
    {
        // Ink external functions go here

        currentStory.BindExternalFunction("PlaySound", (string soundName) => {
            Debug.Log(soundName);
        });





    }

    public void Unbind (Story currentStory)
    {
        currentStory.UnbindExternalFunction("PlaySound");
    }
}
