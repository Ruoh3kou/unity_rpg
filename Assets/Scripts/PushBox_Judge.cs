using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox_Judge : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "glass")
        {
            Mission_PushBox.Instance.cover++;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "glass")
        {
            Mission_PushBox.Instance.cover--;
        }
    }
}
