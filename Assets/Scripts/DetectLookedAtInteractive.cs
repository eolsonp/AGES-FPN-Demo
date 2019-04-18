﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Interactable object deterction system
/// </summary>
public class DetectLookedAtInteractive : MonoBehaviour
{
    [Tooltip("Starting point of raycast used to detect interactives.")]
    [SerializeField]
    private Transform raycastOrigin;

    [Tooltip("How far from the raycastOrigin we will search for interactive elements")]
    [SerializeField]
    private float maxRange = 5.0f;

    /// <summary>
    /// Event raised when the player looks at a different IInteractive
    /// </summary>
    public static event Action<IInteractive> LookedAtInteractiveChanged;

    public IInteractive LookedAtInteractive
    {
        get { return lookedAtInteractive; }
        private set
        {
            bool isInteractiveChanged = value != lookedAtInteractive;
            if (isInteractiveChanged)
            {
                lookedAtInteractive = value;
                LookedAtInteractiveChanged?.Invoke(lookedAtInteractive);

            }
        }
    }

    private IInteractive lookedAtInteractive;

    private void FixedUpdate()
    {
        GetLookedAtInteractive();

    }

    private IInteractive GetLookedAtInteractive()
    {
        Debug.DrawRay(raycastOrigin.position, raycastOrigin.forward * maxRange, Color.red);
        RaycastHit hitInfo;
        bool objectWasDetected = Physics.Raycast(raycastOrigin.position, raycastOrigin.forward, out hitInfo, maxRange);

        IInteractive interactive = null;

        LookedAtInteractive = interactive;

        if (objectWasDetected)
        {
            //Debug.Log($"You are looking at:  {hitInfo.collider.gameObject.name}");
            interactive = hitInfo.collider.gameObject.GetComponent<IInteractive>();
        }

        if (interactive != null)
            lookedAtInteractive = interactive;

        return interactive;
    }
}
