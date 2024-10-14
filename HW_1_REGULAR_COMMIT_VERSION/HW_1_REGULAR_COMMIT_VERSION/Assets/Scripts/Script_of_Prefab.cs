using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_of_Prefab : MonoBehaviour {

    public GameObject objectPrefab;

    public Vector3 spawnOffset = new Vector3(0, 5, 0);


    void Update() {

        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnObject();
        }
    }


    void SpawnObject() {
        if (objectPrefab != null) {

            Instantiate(objectPrefab, transform.position + spawnOffset, Quaternion.identity);
        }
        else {
            Debug.LogWarning("No prefab assigned to the objectPrefab variable!");
        }
    }
}