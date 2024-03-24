using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSentence : MonoBehaviour
{
    public string[] sentences;

    public GameManager gameManager;
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Dialogue" && gameManager.BossStart == 0)
        {
            if (DialogueManager.instance.dialoguegroup.alpha == 0)
                DialogueManager.instance.Ondialogue(sentences);
        }

    }
}

    /*private void OnMouseDown()
    {
        DialogueManager.instance.Ondialogue(sentences);
    }
}*/
