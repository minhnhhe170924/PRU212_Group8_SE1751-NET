using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem {
    public class DialogueLine : DialogueBase {
        private TextMeshProUGUI textHolder;

        [Header ("Text Option")]
        [SerializeField] private string input;
        [SerializeField ]private Color textColor;

        [Header ("Time parameters")]
        [SerializeField] private float delay;

        [Header("Character Info")]
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Image imageHolder;
        [SerializeField] private TextMeshProUGUI characterName;

        private void Awake() {
            textHolder = GetComponent<TextMeshProUGUI>();
            Debug.Log("textHolder: " + textHolder.text);
            //Debug.Log("textHolder.text: " + textHolder.text);
            //imageHolder.sprite = characterSprite;
        }

        private void Start() {
            StartCoroutine(WriteText(input, textHolder, textColor, delay));
        }

    }
}

