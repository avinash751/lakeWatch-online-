using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiManager : MonoBehaviour
{
    [Header("Ui info")]
    public TextMeshProUGUI CurrentObjectHoveredText;
    public TextMeshProUGUI textOr;
    public SceneManager sceneManagerScript;
    public bool uiEnable = true;

    public Image leftClickImage;
    public Image RightClickimage;

    [Header("Color Text info")]
    public Color clickedTextColor;
    public Color whiteColor;
    public bool changeColor;
    public float delayTime;
   

    
    // Start is called before the first frame update
    void Start()
    {
        CurrentObjectHoveredText.enabled = true;

       
    }
    // Update is called once per frame
    void Update()
    {
        HoveringUi();
        EnableOrDisableUi();

    }
    public void HoveringUi()
    {
        if(uiEnable == true)
        {
            CurrentObjectHoveredText.text = sceneManagerScript.gameObjectName; // setting the text mesh pro text componant to the object name that the raycast got by clciking
            ChangeColorWhenClicked();

        }
    }

    // function to disable or enable hovering ui when the key D is clicked
    public void EnableOrDisableUi()
    {

        if (Input.GetKeyDown(KeyCode.D) && uiEnable == true)
        {
            CurrentObjectHoveredText.enabled = false;
            textOr.enabled = false;

            uiEnable = false;

            leftClickImage.enabled = false;
            RightClickimage.enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.D) && uiEnable == false)
        {
            CurrentObjectHoveredText.enabled = true;
            textOr.enabled = true;
            uiEnable = true;
            
            leftClickImage.enabled = true;
            RightClickimage.enabled = true;
        }
    }

    // function chnage color of text when clicked
    public void ChangeColorWhenClicked()
    { 
        if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1) && changeColor ==true)
        {
            CurrentObjectHoveredText.color = clickedTextColor; //  // chnaging hoverung ui text to orange , when clicked
            changeColor = false;
        }
        else if(changeColor == false)
        {
               StartCoroutine(ColorDelay()); // calling the delay timer function
        }
    }

    // timer delay  for color to chnage back to white
    public IEnumerator ColorDelay()
    {
        yield return new WaitForSeconds(delayTime);  // syntax to make the timer
        CurrentObjectHoveredText.color = whiteColor;  // chnaging hoverung ui text back to white
        changeColor = true;
    }
    
}
