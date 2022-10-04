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

    public static PoolManager Pool { get { Init(); return s_Instance._pool; } }
    public static ResourceManager Resource { get { Init(); return s_Instance._resource; } }
    public static SoundManager Sound { get { Init(); return s_Instance._sound; } }
    public static Scene_Manager Scene { get { Init(); return s_Instance._scene; } }
    public static UIManager UI { get { Init(); return s_Instance._ui; } }

    void Start()
    {
        Init();
    }

    static void Init()
    {
        if (s_Instance == null)
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

        s_Instance._pool.Init();
        s_Instance._sound.Init();
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
