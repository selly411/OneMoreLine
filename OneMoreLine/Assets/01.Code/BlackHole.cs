using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public Transform pWhiteHole;

    void OnTriggerEnter2D(Collider2D pCollider)
    {
        Debug.Log("Trigger");
        PlayerController pController = pCollider.GetComponent<PlayerController>();
        if (pController == null)
            return;

        //Time.timeScale = 0.2f;
        pController.transform.position = pWhiteHole.position;
        pController.transform.rotation = pWhiteHole.rotation;
    }
}
