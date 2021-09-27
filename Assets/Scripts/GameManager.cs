using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private List<string> levelFilePaths;
    public List<Level> levels = new List<Level>(10);

    private AsyncOperation async;
    public int index;
    public bool gameOver = false;

    void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = GetComponent<GameManager>();
        }
        else
        {
            Destroy(gameObject);
        }

        //if (gameOver)
        //{
        //    SceneManager.LoadScene("Animation", LoadSceneMode.Single);
        //}
    }

    void Start()
    {
        processLevelFiles();
    }

    void Update()
    {
        
    }

    public void processLevelFiles()
    {
        levelFilePaths = Directory.EnumerateFiles(@"Assets\Resources\Levels\", "*.*", SearchOption.AllDirectories)
            .Where(name => !name.EndsWith(".meta")).OrderBy(q => q).ToList();


        foreach (string path in levelFilePaths)
        {
            using (StreamReader sr = new StreamReader(path))
            {
                List<string> objList = new List<string>();

                for (int i = 0; i < 5; i++)
                {
                    string line = sr.ReadLine();
                    string input = line.Substring(line.LastIndexOf(':') + 2);
                    objList.Add(input);
                }
                levels.Add(new Level(int.Parse(objList[0]), int.Parse(objList[1]), int.Parse(objList[2]), int.Parse(objList[3]), objList[4].Split(',')));
                objList.Clear();
            }
        }
    }

    public void LoadScene(string sceneName)
    {
        instance.StartCoroutine(Load(sceneName));
    }

    IEnumerator Load(string sceneName)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = true;
        Debug.Log(async.allowSceneActivation);
        yield return async;
    }
}
