using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _rules, _main;

    public void SwitchMenu(bool isMain)
    {
        if (isMain)
        {
            _rules.SetActive(true);
            _main.SetActive(false);
        }
        else
        {
            _rules.SetActive(false);
            _main.SetActive(true);
        }
        
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
}
