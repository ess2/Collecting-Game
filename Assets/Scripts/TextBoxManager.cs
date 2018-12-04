using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextBoxManager : MonoBehaviour {

    public GameObject textBox;

    public Text theText;

    public TextAsset textFile;
    public string[] textLines;

    public int currentLine;
    public int endAtLine;

    public CharacterController2D player;

    private bool isTyping = false;
    private bool cancelTyping = false;
    private bool isFirstDialogue = true;

    public float typeSpeed;

    public Animator transitionAnim;
    public Animator soundAnim;
    public string sceneName;

    void Start()
    {
        player = FindObjectOfType<CharacterController2D>();

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }

        if(endAtLine == 0)
        {
            endAtLine = textLines.Length - 1;
        }
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!isTyping)
            {
                currentLine += 1;

                if (currentLine > endAtLine)
                {
                    textBox.SetActive(false);

                    StartCoroutine(LoadScene());
                }
                else
                {
                    StartCoroutine(TextScroll(textLines[currentLine]));
                }
            }
            else if(isTyping && !cancelTyping)
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

    private IEnumerator LoadScene()
    {
        transitionAnim.SetTrigger("end");
        soundAnim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator TextScroll(string lineOfText)
    {

        if(isFirstDialogue)
        {
            yield return new WaitForSeconds(4f);
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
        isTyping = false;
        cancelTyping = false;
    }
}
