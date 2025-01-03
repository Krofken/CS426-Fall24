﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PortalableObject : MonoBehaviour
{
    private GameObject cloneObject;

    private int inPortalCount = 0;

    private Portal inPortal;
    private Portal outPortal;

    private new Rigidbody rigidbody;
    private new Collider collider;

    private static readonly Quaternion halfTurn = Quaternion.Euler(0.0f, 180.0f, 0.0f);

    protected virtual void Awake()
    {
        // Initialize clone object for visual representation
        cloneObject = new GameObject("CloneObject");
        cloneObject.SetActive(false);
        var meshFilter = cloneObject.AddComponent<MeshFilter>();
        var meshRenderer = cloneObject.AddComponent<MeshRenderer>();

        meshFilter.mesh = GetComponent<MeshFilter>().mesh;
        meshRenderer.materials = GetComponent<MeshRenderer>().materials;
        cloneObject.transform.localScale = transform.localScale;

        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    private void LateUpdate()
    {
        if (inPortal == null || outPortal == null)
        {
            return;
        }

        if (cloneObject.activeSelf && inPortal.IsPlaced && outPortal.IsPlaced)
        {
            var inTransform = inPortal.transform;
            var outTransform = outPortal.transform;

            // Update position of clone
            Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
            relativePos = halfTurn * relativePos;
            cloneObject.transform.position = outTransform.TransformPoint(relativePos);

            // Update rotation of clone
            Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
            relativeRot = halfTurn * relativeRot;
            cloneObject.transform.rotation = outTransform.rotation * relativeRot;
        }
        else
        {
            // Move the clone far away when inactive
            cloneObject.transform.position = new Vector3(-1000.0f, 1000.0f, -1000.0f);
        }
    }

    public void SetIsInPortal(Portal inPortal, Portal outPortal, Collider wallCollider)
    {
        this.inPortal = inPortal;
        this.outPortal = outPortal;

        Physics.IgnoreCollision(collider, wallCollider);

        // Activate the clone object
        cloneObject.SetActive(true);

        ++inPortalCount;
    }

    public void ExitPortal(Collider wallCollider)
    {
        Physics.IgnoreCollision(collider, wallCollider, false);
        --inPortalCount;

        if (inPortalCount <= 0)
        {
            inPortalCount = 0;
            cloneObject.SetActive(false);
            inPortal = null;
            outPortal = null;
        }
    }

private bool isCooldownActive = false;
private float cooldownTime = 0.1f; // Adjust as needed

public virtual void Warp()
{
    if (isCooldownActive || inPortal == null || outPortal == null)
    {
        Debug.LogWarning("Warping failed: Missing portal references or cooldown active.");
        return;
    }

    var inTransform = inPortal.transform;
    var outTransform = outPortal.transform;

    // Update position of object
    Vector3 relativePos = inTransform.InverseTransformPoint(transform.position);
    relativePos = halfTurn * relativePos;
    transform.position = outTransform.TransformPoint(relativePos);

    // Update rotation of object
    Quaternion relativeRot = Quaternion.Inverse(inTransform.rotation) * transform.rotation;
    relativeRot = halfTurn * relativeRot;
    transform.rotation = outTransform.rotation * relativeRot;

    // Update velocity of rigidbody
    Vector3 relativeVel = inTransform.InverseTransformDirection(rigidbody.velocity);
    relativeVel = halfTurn * relativeVel;
    rigidbody.velocity = outTransform.TransformDirection(relativeVel);

    // Log for debugging
    Debug.Log($"Warped object from {inPortal.name} to {outPortal.name}");

    // Swap portal references
    var tmp = inPortal;
    inPortal = outPortal;
    outPortal = tmp;

    // Start cooldown
    StartCoroutine(WarpCooldown());
}

private IEnumerator WarpCooldown()
{
    isCooldownActive = true;
    yield return new WaitForSeconds(cooldownTime);
    isCooldownActive = false;
}
}

