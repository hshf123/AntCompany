using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    static Managers s_Instance;
    static Managers Instance { get { Init(); return s_Instance; } }

    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    SoundManager _sound = new SoundManager();
    Scene_Manager _scene = new Scene_Manager();
    UIManager _ui = new UIManager();

    public static PoolManager Pool { get { return s_Instance._pool; } }
    public static ResourceManager Resource { get { return s_Instance._resource; } }
    public static SoundManager Sound { get { return s_Instance._sound; } }
    public static Scene_Manager Scene { get { return s_Instance._scene; } }
    public static UIManager UI { get { return s_Instance._ui; } }

    void Start()
    {
        GameObject go = GameObject.Find("@Managers");
        Managers mg = go.GetComponent<Managers>();
    }

    static void Init()
    {
        if (Instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_Instance = go.GetComponent<Managers>();
        }
    }

    void Update()
    {

    }

    public static void Clear()
    {
        Sound.Clear();
        Scene.Clear();
        Pool.Clear();
    }
}
