using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class StartGame : MonoBehaviour
{

    public void toMain()
    {
        SceneManager.LoadScene(1);
    }
    public void toStart()
    {
        SceneManager.LoadScene(0);
    }

}
