using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem {
    public class DialogueBase : MonoBehaviour {
        public bool finished { get; private set; }
        public IEnumerator WriteText(string input, TextMeshProUGUI textHolder, Color textColor, float delay) {
            //textHolder.color = textColor;
            for (int i = 0; i < input.Length; i++) {
                textHolder.text += input[i];
                yield return new WaitForSeconds(delay);
            }

            yield return new WaitUntil(() => Input.GetMouseButton(0));
            finished = true;

        }
    }
}

