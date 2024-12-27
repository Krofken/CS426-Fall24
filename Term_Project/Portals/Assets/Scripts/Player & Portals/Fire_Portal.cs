using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_Portal : MonoBehaviour
{

    [SerializeField]
    private PortalPair portals;

    [SerializeField]
    private LayerMask layerMask;

    public Camera playerCamera;

    [SerializeField] private AudioClip portalOpen; // Sound to play on interaction


    [SerializeField] private AudioSource audioSource;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button click
        {
            FirePortal(0, transform.position, transform.forward, 250.0f);

        }else if (Input.GetMouseButtonDown(1)) // Right mouse button click
        {
            FirePortal(1, transform.position, transform.forward, 250.0f);
        }
    }

    private void FirePortal(int portalID, Vector3 pos, Vector3 dir, float distance)
    {
        RaycastHit hit;
        Physics.Raycast(pos, dir, out hit, distance, layerMask);

        if(hit.collider != null)
        {

            // Orient the portal according to camera look direction and surface direction.
            var cameraRotation = playerCamera.transform.rotation;
            var portalRight = cameraRotation * Vector3.right;
            
            if(Mathf.Abs(portalRight.x) >= Mathf.Abs(portalRight.z))
            {
                portalRight = (portalRight.x >= 0) ? Vector3.right : -Vector3.right;
            }
            else
            {
                portalRight = (portalRight.z >= 0) ? Vector3.forward : -Vector3.forward;
            }

            var portalForward = -hit.normal;
            var portalUp = -Vector3.Cross(portalRight, portalForward);

            var portalRotation = Quaternion.LookRotation(portalForward, portalUp);
            
            // Attempt to place the portal.
            bool wasPlaced = portals.Portals[portalID].PlacePortal(hit.collider, hit.point, portalRotation);

            if(wasPlaced)
            {
                audioSource.PlayOneShot(portalOpen);
            }
        }
    }
}
