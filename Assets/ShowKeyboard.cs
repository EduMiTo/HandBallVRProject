using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;


public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    public void OpenKeyboard()
    {
        CloseNumpad(canvas);
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        SetCaretColorAlpha(1);

        NonNativeKeyboard.Instance.OnClosed += Instance_OnClosed;

    }

    public void Instance_OnClosed(object sender, System.EventArgs e){
        SetCaretColorAlpha(0);
        NonNativeKeyboard.Instance.OnClosed -= Instance_OnClosed;
    }
    

    public void SetCaretColorAlpha(float value){
        inputField.customCaretColor = true;
        Color caretColor = inputField.caretColor;
        caretColor.a = value;
        inputField.caretColor = caretColor;
    }

    public void CloseNumpad(Canvas canvasNumpad)
    {
        canvasNumpad.gameObject.SetActive(false);
    }
}
