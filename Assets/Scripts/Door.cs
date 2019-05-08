﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class Door : InteractiveObject
{
    #region Serializefields
    
    [Tooltip("Assigning a key here will lock the door. If the player has the key in their inventory, they can open the locked door")]
    [SerializeField]
    private InventoryObject key;

    [Tooltip("If this is checked, the associated key will be removed from the player's inventory when the door is unlocked.")]
    [SerializeField]
    private bool consumesKey;
   
    [Tooltip("The text that displays when the player looks at the door while its locked.")]
    [SerializeField]
    private string lockedDisplayText = "Locked";

    [Tooltip("Play this audio clip when the player interacts with a locked dorr without having the key.")]
    [SerializeField]
    private AudioClip lockedAudioClip;

    [Tooltip("Play this audio clip when the player opens the door.")]
    [SerializeField]
    private AudioClip openAudioClip;

    [Tooltip("Loads next scene when the player interacts with this door")]
    [SerializeField]
    private bool willLoadNextScene = false;
    #endregion
    public override string DisplayText
    {
        get
        {
            string toReturn;

            if (isLocked)
                toReturn = HasKey ? $"Use {key.ObjectName}" : lockedDisplayText;
            else
                toReturn = base.DisplayText;

            return toReturn;
        }
    }

    private bool HasKey => PlayerInventory.InventoryObjects.Contains(key);
    private Animator animator;
    private bool isOpen = false;
    private bool isLocked;

    /// <summary>
    /// Using a constructor here to initialize displayText in the editor
    /// </summary>
    public Door()
    {
        displayText = nameof(Door);
    }

    protected override void Awake()
    {
        {
            base.Awake();
            animator = GetComponent<Animator>();
            InitializeIsLocked();
        }
    }

    private void InitializeIsLocked()
    {
        if (key != null)
            isLocked = true;
    }

    public override void InteractWith()
    {
        if(!isOpen)
        {
            if(isLocked && !HasKey)
            {
                audioSource.clip = lockedAudioClip;
            }
            else //not locked or if it's locked and we have the key..
            {
                audioSource.clip = openAudioClip;
                animator.SetBool("shouldOpen", true);
                displayText = string.Empty;
                isOpen = true;
                UnlockDoor();
                if (willLoadNextScene)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex -1);
            }
            base.InteractWith(); //This plays a sound effect
        }       
    }

    private void UnlockDoor()
    {
        isLocked = false;
        if (key != null && consumesKey)
            PlayerInventory.InventoryObjects.Remove(key);
    }
}
