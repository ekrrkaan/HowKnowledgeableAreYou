using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
   public static void SceneChange(int sahne_id)
    {
        SceneManager.LoadScene(sahne_id);
    }


}
