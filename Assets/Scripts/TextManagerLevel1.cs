using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TextManagerLevel1 : MonoBehaviour {
    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool isFirstDialogue = true;

    public float typeSpeed;

    public Image faceSetController;
    public Animator faceSetAnimator;
    public Animator escavelAnimator;
    public GameObject escavel;

    private Vector3 posAEscavel;
    private Vector3 posBEscavel;

    public float speed;

    private int[] linesEscavelTalks = { 0,4,5,6,7 };
    private int[] linesReysTalks = { 1, 2,3 };
    private int linePersuasiveEscavel = 7;

    [SerializeField]
    private Transform escavelTransform;

    [SerializeField]
    private Transform escavelTransformB;

    void Start()
    {
        posAEscavel = escavelTransform.localPosition;
        posBEscavel = escavelTransformB.localPosition;

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if (endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    textBox.SetActive(false);
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));

                    if (currentLine == linePersuasiveEscavel)
                    {
                        escavelTransform.localPosition = Vector3.MoveTowards(escavelTransform.localPosition, posBEscavel, -speed * Time.deltaTime);
                    }
                   
                }
            }
            else if (isTyping && !cancelTyping)
            {
                cancelTyping = true;
            }
        }
        else if (isFirstDialogue)
        {
            StartCoroutine(TextScroll(textLines[currentLine]));
            isFirstDialogue = false;
        }
    }

    private IEnumerator TextScroll(string lineOfText)
    {
        if(Array.Exists(linesEscavelTalks, element => element == currentLine)) {
            faceSetAnimator.SetBool("isEscavelTalking", true);
            faceSetAnimator.SetBool("isReysTalking", false);
        }
        else if(Array.Exists(linesReysTalks, element => element == currentLine))
        {
            faceSetAnimator.SetBool("isReysTalking", true);
            faceSetAnimator.SetBool("isEscavelTalking", false);
        }

        if(currentLine == linePersuasiveEscavel)
        {
            escavelAnimator.SetTrigger("persuasiveDialogue");
        }
        
        int letter = 0;
        theText.text = "";
        isTyping = true;
        cancelTyping = false;

        while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1))
        {
            theText.text += lineOfText[letter];
            letter += 1;
            yield return new WaitForSeconds(typeSpeed);
        }

        theText.text = lineOfText;
        
        if(currentLine == linePersuasiveEscavel)
        {
            escavelAnimator.SetTrigger("endOfDialogue");
        }
        isTyping = false;
        cancelTyping = false;
    }
}
