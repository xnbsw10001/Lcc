﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LccModel
{
    public class ConfigManager : Singleton<ConfigManager>
    {
        public Hashtable configs = new Hashtable();
        public void InitManager()
        {
            foreach (Type item in Manager.Instance.types.Values)
            {
                if (item.IsAbstract) continue;
                ConfigAttribute[] configAttributes = (ConfigAttribute[])item.GetCustomAttributes(typeof(ConfigAttribute), false);
                if (configAttributes.Length > 0)
                {
                    IConfigTable iConfigTable = (IConfigTable)Activator.CreateInstance(item);
                    iConfigTable.InitConfigTable();
                    configs.Add(iConfigTable.ConfigType, iConfigTable);
                }
            }
        }
        public T GetConfig<T>(int id) where T : IConfig
        {
            Type type = typeof(T);
            if (configs.ContainsKey(type))
            {
                AConfigTable<T> aConfigTable = (AConfigTable<T>)configs[type];
                return aConfigTable.GetConfig(id);
            }
            else
            {
                Debug.Log($"Config不存在{type.Name}");
                return default;
            }
        }
        public Dictionary<int, T> GetConfigs<T>() where T : IConfig
        {
            Type type = typeof(T);
            if (configs.ContainsKey(type))
            {
                AConfigTable<T> aConfigTable = (AConfigTable<T>)configs[type];
                return aConfigTable.GetConfigs();
            }
            else
            {
                Debug.Log($"Config不存在{type.Name}");
                return default;
            }
        }
    }
}