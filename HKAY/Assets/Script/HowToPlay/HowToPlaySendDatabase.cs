using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class HowToPlaySendDatabase : MonoBehaviour
{
    public TMP_InputField question;
    public TMP_InputField answer1;
    public TMP_InputField answer2;
    public TMP_InputField answer3;
    public TMP_InputField answer4;
    public TMP_InputField correctanswer;
    public TMP_InputField time;


    public TextMeshProUGUI hata_T;


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void send()
    {
        StartCoroutine(SendQuestion());
        


    }


    IEnumerator SendQuestion()
    {
        WWWForm form = new WWWForm();

        form.AddField("unity", "SendQuestion");
        form.AddField("question", question.text);
        form.AddField("answer1", answer1.text);
        form.AddField("answer2", answer2.text);
        form.AddField("answer3", answer3.text);
        form.AddField("answer4", answer4.text);
        form.AddField("correctanswer", int.Parse(correctanswer.text));
        form.AddField("time", int.Parse(time.text));


        if (question.text == "" && answer1.text == "" && answer2.text == "" && answer3.text == "" && answer4.text == "" && correctanswer.text == "" && time.text == "")
        {

            hata_T.text = "Please fill the inputs correctly !";

        }
        else
        {

            using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/HKAYstorage/database_controls.php", form))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {

                    Debug.Log(www.error);

                }
                else
                {

                    if (www.downloadHandler.text.Equals("Insert to table successfull"))
                    {
                        Debug.Log("Done");
                    }


                    hata_T.text = "Successfully added !";

                }
            }



        }


        
    }

    

}
