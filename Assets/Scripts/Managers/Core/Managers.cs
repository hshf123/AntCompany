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
    #region Core
    PoolManager _pool = new PoolManager();
    ResourceManager _resource = new ResourceManager();
    Scene_Manager _scene = new Scene_Manager();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();

    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static Scene_Manager Scene { get { return Instance._scene; } }
    public static UIManager UI { get { return Instance._ui; } }
    #endregion
    #region Contents
    DataManager _data = new DataManager();
    Game_Manager _game = new Game_Manager();
    InventoryManager _inven = new InventoryManager();
    ObjectManager _object = new ObjectManager();
    SkillSlotManager _skill = new SkillSlotManager();

    public static DataManager Data { get { return Instance._data; } }
    public static Game_Manager Game { get { return Instance._game; } }
    public static InventoryManager Inven { get { return Instance._inven; } }
    public static ObjectManager Object { get { return Instance._object; } }
    public static SkillSlotManager Skill { get { return Instance._skill; } }
    #endregion
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

            s_Instance._data.Init();
            s_Instance._pool.Init();
            s_Instance._sound.Init();

            Application.targetFrameRate = 60; // 60«¡∑π¿”
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
