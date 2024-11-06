using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITextSkill : MonoBehaviour
{
    [SerializeField]
    private Image imageControl;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(HideImage());
    }

    private IEnumerator HideImage()
    {
        yield return new WaitForSeconds(2f); // Wait for 2 minutes
        imageControl.gameObject.SetActive(false);
    }
}
