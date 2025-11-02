using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using TMPro;
public class InklyManager : MonoBehaviour
{
    public TextAsset _inkJavaThing;
    Story _inkStory;
    public TextMeshProUGUI uIText;
    public GameObject choiceOneButton;
    public GameObject choiceTwoButton;
    public TextMeshProUGUI uIchoiceOne;
    public TextMeshProUGUI uIchoiceTwo;
    // Start is called before the first frame update
    void Start()
    {
        choiceOneButton.SetActive(false);
        choiceTwoButton.SetActive(false);
        uIText.text = "";
        uIchoiceOne.text = "";
        uIchoiceTwo.text = "";
        _inkStory = new Story (_inkJavaThing.text);
        uIText.text = _inkStory.Continue();
    }

    // Update is called once per frame
    void Update()
    {
        if(_inkStory.canContinue && Input.GetKeyDown(KeyCode.P))
        {
            uIText.text = _inkStory.Continue();
        }

        if (_inkStory.currentChoices.Count > 0)
        {
            choiceOneButton.SetActive(true);
            choiceTwoButton.SetActive(true);
            Choice choiceOne = _inkStory.currentChoices[0];
            Choice choiceTwo = _inkStory.currentChoices[1];
            uIchoiceOne.text = "Choice 1. " + choiceOne.text;
            uIchoiceTwo.text = "Choice 2. " + choiceTwo.text;
        }
    }

    public void MakeChoiceOne()
    {
        _inkStory.ChooseChoiceIndex(0);
        uIText.text = _inkStory.Continue();
        choiceOneButton.SetActive(false);
        choiceTwoButton.SetActive(false);
    }
    public void MakeChoiceTwo()
    {
        _inkStory.ChooseChoiceIndex(1);
        uIText.text = _inkStory.Continue();
        choiceOneButton.SetActive(false);
        choiceTwoButton.SetActive(false);
    }
}
