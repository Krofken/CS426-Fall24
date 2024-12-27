using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalPair : MonoBehaviour
{
    public Portal[] Portals { private set; get; }

    private void Awake()
    {
        Portals = GetComponentsInChildren<Portal>();
    }
}
