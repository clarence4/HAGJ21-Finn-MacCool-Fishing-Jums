using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextUpdate : MonoBehaviour
{
    private UIManager UI;
    private TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        UI = FindObjectOfType<UIManager>();
        tmp = GetComponent<TextMeshProUGUI>();
    }


    // Update is called once per frame
    void Update()
    {
        tmp.text = UI.lines[UI.lineIndex];
    }
}
