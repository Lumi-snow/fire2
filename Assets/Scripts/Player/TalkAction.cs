using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkAction : MonoBehaviour
{
    public GameObject targetNpc;
    public GameObject targetItem;

    void Update()
    {
        // ÚG’†‚ÉƒL[“ü—Í
        if (targetNpc != null && Input.GetKeyDown(KeyCode.Return))
        {
            if (targetNpc.CompareTag("NPC"))
            {
                Debug.Log("NPC‚ÉÚG‚µ‚Ä‘Î˜b‚ğ‚İ‚½");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter: " + other.name);

        if (other.CompareTag("NPC"))
        {
            targetNpc = other.gameObject;
            Debug.Log(other.gameObject + "‚ÆÚG’†");
        }
    }
}