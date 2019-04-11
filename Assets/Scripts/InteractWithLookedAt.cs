using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Detects when the player presses the Interact button while looking at an IInteractive,
/// and then calls that IInteractive's InteractWith method.
/// </summary>

public class InteractWithLookedAt : MonoBehaviour
{

    [SerializeField]
    private DetectLookedAtInteractive detectLookedAtInteractive;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && detectLookedAtInteractive.lookedAtInteractive != null)
        {
            Debug.Log("Player pressed the Interact button");
            detectLookedAtInteractive.lookedAtInteractive.InteractWith();
        }
    }
}
