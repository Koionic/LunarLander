using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour {

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene("Main Menu");
    }
}
