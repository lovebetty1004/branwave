﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Looxid.Link
{
    public class BarChart : MonoBehaviour
    {
        public int Width = 1000;
        public int Height = 240;

        public Color chartColor = Color.white;

        public float TimeLength = 4.0f;
        private int numGraph = 40;
        private int barSpace = 5;

        private GameObject[] bars;


        void OnEnable()
        {
            numGraph = Mathf.FloorToInt(TimeLength * 10.0f);
            if (bars == null)
            {
                float barWidth = ((float)Width / (float)numGraph) - barSpace;
                bars = new GameObject[numGraph];
                for (int i = 0; i < numGraph; i++)
                {
                    bars[i] = new GameObject("Bar");
                    bars[i].transform.parent = this.transform;
                    bars[i].transform.localPosition = Vector3.zero;
                    bars[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

                    Image barImage = bars[i].AddComponent<Image>();
                    barImage.color = chartColor;

                    RectTransform barRect = bars[i].GetComponent<RectTransform>();
                    barRect.anchorMin = new Vector2(0.0f, 0.0f);
                    barRect.anchorMax = new Vector2(0.0f, 0.0f);
                    barRect.pivot = new Vector2(0.0f, 0.0f);
                    barRect.anchoredPosition = new Vector2((float)i * (barWidth + (float)barSpace), 0.0f);
                    barRect.sizeDelta = new Vector2(barWidth, 0.0f);
                }
            }
        }

        public void SetValue(List<double> datalist)
        {
            if (bars == null) return;

            int numData = 0;
            for (int x = numGraph - 1; x >= 0; x--)
            {
                int dataHeight = 0;
                if (datalist.Count > numData)
                {
                    dataHeight = Mathf.FloorToInt((float)datalist[numData] * (float)Height);
                }

                RectTransform barRect = bars[x].GetComponent<RectTransform>();
                barRect.sizeDelta = new Vector2(barRect.sizeDelta.x, dataHeight);
                numData++;
            }
        }
    }
}