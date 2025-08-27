using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

public class ResourceManager : SingleTon<ResourceManager>
{
    private SpriteAtlas atlas;
    public SpriteAtlas GetAtlas { get { return atlas; } }

    //메니저 인스턴스 생성시 실행되는 함수
    protected override void Init()
    {
        base.Init();
        LoadAsync<SpriteAtlas>("Atlas", (result) =>
        {
            atlas = result;
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">리턴타입</typeparam>
    /// <param name="key">타겟 키값</param>
    /// <param name="callback">(obj)=>{targetInstance = obj}</param>
    public void LoadAsync<T>(string key, Action<T> callback, bool isCaching = false)
    {
        if (key.Contains(".sprite"))
        {
            key = $"{key}[{key.Replace(".sprite", "")}]";
        }
        AsyncOperationHandle<T> infoAsyncOP = Addressables.LoadAssetAsync<T>(key);
        infoAsyncOP.Completed += (op) =>
        {

            callback?.Invoke(infoAsyncOP.Result);
            if (isCaching) Addressables.Release(infoAsyncOP);
        };
    }

    private string GetSavePath() => Application.persistentDataPath;

    public bool SaveJSonData<T>(T data, string fileName, bool isOverride, bool isSaveFile)
    {
        string path = isSaveFile ? Path.Combine(GetSavePath(), fileName) : Path.Combine("./Assets/DataSheets/", fileName);

        Debug.Log(path);
        if (File.Exists(path))
        {
            if (isOverride == true)
            {

                File.WriteAllText(path, JsonUtility.ToJson(data));
                return true;
            }
        }
        else
        {
            if (File.Exists(GetSavePath()))
            {
                StreamWriter file = File.CreateText(path);
                file.WriteLine(JsonUtility.ToJson(data));
            }
            else
            {
                Directory.CreateDirectory(GetSavePath());
                StreamWriter file = File.CreateText(path);
                file.WriteLine(JsonUtility.ToJson(data));

            }
            return true;
        }
        return false;
    }
    public T LoadJSonData<T>(string fileName, bool isSaveFile) where T : new()
    {
        string path = isSaveFile ? Path.Combine(GetSavePath(), fileName) : Path.Combine("./Assets/DataSheets/", fileName);

        if (File.Exists(path))
        {
            T result;
            try
            {
                result = JsonUtility.FromJson<T>(File.ReadAllText(path));
            }
            catch
            {
                //파싱 실패시
                result = default(T);
                Debug.LogError("제이슨 파싱 실패");
            }
            return result;
        }
        else
        {
            Directory.CreateDirectory(GetSavePath());
            return default(T);
        }
    }
    public void XMLSave<T>(T obj, string dataName)
    {
        string resultPath = Path.Combine("./Assets/DataSheets/", dataName);
        XmlSerializer ser = new XmlSerializer(typeof(T));
        using (FileStream stream = new FileStream(resultPath, FileMode.Create))
        {
            ser.Serialize(stream, obj);
        }

        AssetDatabase.Refresh();
    }
    public T XMLLoad<T>(string dataName)
    {
        string resultPath = Path.Combine("./Assets/DataSheets/", dataName);
        T result = default(T);
        try
        {
            if (File.Exists(resultPath))
            {
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (FileStream stream = new FileStream(resultPath, FileMode.Open))
                {
                    result = (T)ser.Deserialize(stream);
                }
                AssetDatabase.Refresh();
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"XML Parser Load오류발생{e.Message}");
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">리턴타입</typeparam>
    /// <param name="label">타겟 키값</param>
    /// <param name="callback">(obj)=>{targetInstance = obj}</param>
    public void LoadAsyncAll<T>(string label, Action<(string, T)[]> callback, bool isCaching = true)
    {
        var labelKeys = Addressables.LoadResourceLocationsAsync(label, typeof(T));
        //label의 T타입인 오브젝트들의 키값을 가져온다
        labelKeys.WaitForCompletion();
        //resource를 전부 load할때까지 대기

        Debug.Log(labelKeys.Result);
        if (labelKeys.Result.Count == 0) { Debug.LogError($"{label}라벨이 비어있습니다."); callback.Invoke(null); }//해당하는 키가 없을경우 null을 리턴

        int doneCount = 0;

        (string, T)[] tempT = new (string, T)[labelKeys.Result.Count];
        for (int i = 0; i < tempT.Length; i++)
        {
            int curIndex = i;
            string curKey = labelKeys.Result[i].PrimaryKey; //콜백을 동시에 실행시 클로저 이슈가 생길 수 있기에 분리(다른 루프의 스택메모리를 참조하는 현상)
            LoadAsync<T>(labelKeys.Result[i].PrimaryKey, (result) =>
            {
                tempT[curIndex].Item1 = curKey;
                tempT[curIndex].Item2 = result;
                doneCount++;
                if (doneCount == labelKeys.Result.Count)
                {
                    callback?.Invoke(tempT);
                    if (isCaching) Addressables.Release(labelKeys);
                }
            }, isCaching);
        }
    }
}
