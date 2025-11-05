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
    public GameObject camAnim;
    public GameObject gobloomba;
    public LevelManager levelChanger;
    // Start is called before the first frame update
    void Start()
    {
        levelChanger = FindAnyObjectByType<LevelManager>();
        gobloomba = GameObject.FindGameObjectWithTag("Enemy");
        gobloomba.SetActive(false);
        camAnim = GameObject.FindGameObjectWithTag("MainCamera");
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
        if(_inkStory.canContinue && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")))
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

        if ((string)_inkStory.variablesState["cameraPos"] == "AgentIn")
        {
            camAnim.GetComponent<CameraAnimation>().AgentIn();
            _inkStory.variablesState["cameraPos"] = "";
        }

        if ((string)_inkStory.variablesState["cameraPos"] == "AgentOut")
        {
            camAnim.GetComponent<CameraAnimation>().AgentOut();
            _inkStory.variablesState["cameraPos"] = "";
        }
        if ((string)_inkStory.variablesState["cameraPos"] == "AgentToPlayer")
        {
            camAnim.GetComponent<CameraAnimation>().AgentToPlayer();
            _inkStory.variablesState["cameraPos"] = "";
        }
        if ((string)_inkStory.variablesState["cameraPos"] == "PlayerToAgent")
        {
            camAnim.GetComponent<CameraAnimation>().PlayerToAgent();
            _inkStory.variablesState["cameraPos"] = "";
        }
        if ((bool)_inkStory.variablesState["gobloombaSpawn"] == true)
        {
            gobloomba.SetActive(true);
        }

        if ((bool)_inkStory.variablesState["nextScene"] == true)
        {
            levelChanger.changeScene();
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
