﻿using System.Collections;
using UnityEngine;

namespace Model
{
    public class TipsWindowManager : MonoBehaviour
    {
        [Header("地图中的TipsWindow")]
        public Hashtable tipsWindows = new Hashtable();
        [Header("TipsWindow对象池")]
        public TipsWindowPool tipsWindowPool;
        public int tipsWindowId;
        public void InitManager(TipsWindowPool tipsWindowPool)
        {
            this.tipsWindowPool = tipsWindowPool;
            tipsWindowPool.InitPool();
        }

        public TipsWindow CreateTipsWindow(string title, string info, string confirm, string cancel, Transform parent = null)
        {
            TipsWindow tipsWindow = tipsWindowPool.Dequeue();
            tipsWindow.InitTipsWindow(title, info, confirm, cancel, parent);
            GenerateID(tipsWindow);
            tipsWindows.Add(tipsWindow.id, tipsWindow);
            return tipsWindow;
        }
        public TipsWindow GetTipsWindow(int id)
        {
            TipsWindow tipsWindow = tipsWindows[id] as TipsWindow;
            if (tipsWindow == null) return null;
            return tipsWindow;
        }
        public void DeleteTipsWindow(int id)
        {
            TipsWindow tipsWindow = GetTipsWindow(id);
            if (tipsWindow == null) return;
            tipsWindowPool.Enqueue(tipsWindow);
            tipsWindows.Remove(id);
        }
        public void GenerateID(TipsWindow tipsWindow)
        {
            if (tipsWindows.Count == 0)
            {
                tipsWindowId++;
                tipsWindow.id = tipsWindowId;
                return;
            }
            for (int i = 1; i <= tipsWindows.Count; i++)
            {
                if (!tipsWindows.ContainsKey(i))
                {
                    tipsWindow.id = i;
                    return;
                }
            }
            tipsWindowId++;
            tipsWindow.id = tipsWindowId;
        }
    }
}