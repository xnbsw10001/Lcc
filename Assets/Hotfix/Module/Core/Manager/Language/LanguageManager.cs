﻿using System.Collections;
using UnityEngine;

namespace LccHotfix
{
    public class LanguageManager : Singleton<LanguageManager>
    {
        public Hashtable languages = new Hashtable();
        public void ChangeLanguage(LanguageType type)
        {
            TextAsset asset = LccModel.AssetManager.Instance.LoadAssetData<TextAsset>(type.ToString(), ".txt", false, true, AssetType.Game);
            foreach (string item in asset.text.Split('\n'))
            {
                if (string.IsNullOrEmpty(item))
                {
                    continue;
                }
                string[] KeyValue = item.Split('=');
                if (languages.ContainsKey(KeyValue[0])) return;
                languages.Add(KeyValue[0], KeyValue[1]);
            }
        }
        public string GetValue(string key)
        {
            if (!languages.ContainsKey(key)) return string.Empty;
            string value = (string)languages[key];
            return value;
        }
    }
}