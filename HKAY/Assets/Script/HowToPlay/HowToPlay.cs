using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour
{
    public GameObject HowToPlaySayfa , HowToPlayRules;


    void Start()
    {
        
    }

  
    void Update()
    {
        
    }

    public void NextPage()
    {
        HowToPlaySayfa.SetActive(false);
        HowToPlayRules.SetActive(true);
    }

    public void HowToPlayGeri()
    {
        HowToPlaySayfa.SetActive(true);
        HowToPlayRules.SetActive(false);
    }
    public void HowToPlayStart()
    {
        SceneChanger.SceneChange(1);
    }
    public void AnemenuDon_B()
    {
        SceneChanger.SceneChange(0);
    }
}
