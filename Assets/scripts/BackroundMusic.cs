using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackroundMusic : MonoBehaviour
{
    private static BackroundMusic backroundMusic;
    void Awake()
    {
        if (backroundMusic == null)
        {
            backroundMusic = this;
            DontDestroyOnLoad(backroundMusic);

        }

        else
        {
            Destroy(gameObject);
        }
    }
}
