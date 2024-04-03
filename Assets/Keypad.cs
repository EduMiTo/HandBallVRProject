using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject button6;
    public GameObject button7;
    public GameObject button8;
    public GameObject button9;
    public GameObject button0;
    public GameObject buttonClear;
    public GameObject buttonClose;
    public GameObject buttonEnter;

    public Canvas canvas;

    public void b1(){
        inputField.text = inputField.text + "1";
    }

    public void b2(){
        inputField.text = inputField.text + "2";
    }

    public void b3(){
        inputField.text = inputField.text + "3";
    }
    
    public void b4(){
        inputField.text = inputField.text + "4";
    }

    public void b5(){
        inputField.text = inputField.text + "5";
    }

    public void b6(){
        inputField.text = inputField.text + "6";
    }
    
    public void b7(){
        inputField.text = inputField.text + "7";
    }

    public void b8(){
        inputField.text = inputField.text + "8";
    }

    public void b9(){
        inputField.text = inputField.text + "9";
    }

    public void b0(){
        inputField.text = inputField.text + "0";
    }

    public void clear(){
        inputField.text = null;
    }

    public void enter(){
        SetFalse(canvas);
    }

    public void close(){
        Debug.Log("aaa");
        inputField.text = null;
        SetFalse(canvas);
    }

    public void CloseKeyboard(Canvas canvasKeyboard)
    {
        canvasKeyboard.gameObject.SetActive(false);
    }

    public void OpenNumpad(Canvas canvasNumpad)
    {
        canvasNumpad.gameObject.SetActive(true);
    }

    public void SetFalse(Canvas canvas)
    {
        canvas.gameObject.SetActive(false);
    }
}
