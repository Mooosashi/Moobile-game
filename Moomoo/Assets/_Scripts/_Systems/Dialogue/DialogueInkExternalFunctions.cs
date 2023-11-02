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

        currentStory.BindExternalFunction("PlayVFX", (string vfxName) => {
            Debug.Log(vfxName);
        });

        currentStory.BindExternalFunction("PlayAnimation", (string animationName) => {
            Debug.Log(animationName);
        });





    }

    public void Unbind (Story currentStory)
    {
        currentStory.UnbindExternalFunction("PlaySound");
        currentStory.UnbindExternalFunction("PlayVFX");
        currentStory.UnbindExternalFunction("PlayAnimation");
    }
}
