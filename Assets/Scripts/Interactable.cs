using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string promptMessage;
    BuyScript buyScript;

    public void Start()
    {
        buyScript = GameObject.Find("Shopholder").GetComponent<BuyScript>();
    }

    void StartReferences()
    {
        buyScript = GameObject.Find("Shopholder").GetComponent<BuyScript>();
    }
    public void DefaultInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {

    }
}
