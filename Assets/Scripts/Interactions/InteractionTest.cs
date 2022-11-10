using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTest : Interactable
{

    protected override void Interact()
    {
        BuyScript.isPaused = true;
        Debug.Log("Interacted with" + gameObject.name);
    }
}
