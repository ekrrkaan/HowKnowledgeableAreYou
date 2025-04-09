using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yonetim : MonoBehaviour
{
    public GameObject Register, anamenu_P;
    
    
    void Start()
    {
        if (!PlayerPrefs.HasKey("kayitDurumu"))
        {
            Register.SetActive(true);
            anamenu_P.SetActive(false);
            

        }
        else
        {
            Register.SetActive(false);
            anamenu_P.SetActive(true);
        }

    }

    public void devam_B()
    {
        PlayerPrefs.SetInt("kayitDurumu", 1);
        Register.SetActive(false);
        anamenu_P.SetActive(true);
    }

    public void basla_B()
    {
        SceneChanger.SceneChange(3);
    }

    public void Leaderboard_B()
    {
        SceneChanger.SceneChange(2);
    }
    public void GiveMeQuestion_B()
    {
        SceneChanger.SceneChange(4);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
