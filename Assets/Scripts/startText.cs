using UnityEngine;
using System.Collections;

public class startText : MonoBehaviour
{
    //remove the text after 8 seconds
    public float displayDuration = 8f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(displayDuration);

        gameObject.SetActive(false);
    }
}
