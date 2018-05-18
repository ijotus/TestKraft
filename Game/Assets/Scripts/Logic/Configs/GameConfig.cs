using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game
{
    public class GameConfig : IGameConfig
    {
        public float Timer { get; private set; }
        public Color[] Colors { get; private set; }
        public GameConfig()
        {

        }

        public void Initialize()
        {
            var asset = Resources.Load<TextAsset>("Configs/GameConfig");
            Parse(asset.text);
        }
        
        void Parse(string text)
        {
            var lines = text.Split(new[] { Environment.NewLine },StringSplitOptions.RemoveEmptyEntries);
            try
            {
                var space = ' ';
                var timerData = lines[0].Split(new[] { space }, StringSplitOptions.RemoveEmptyEntries);
                Timer = Convert.ToInt32(timerData[0]);

                if (Timer <= 0)
                    throw new Exception("Invalid Timer value");

                var colorData = lines[1].Split(new[] { space }, StringSplitOptions.RemoveEmptyEntries);
                List<Color> colors = new List<Color>();
                Dictionary<string,int> dictionary = new Dictionary<string, int>();
                foreach(var v in colorData)
                {
                    if (!dictionary.ContainsKey(v))
                    {
                        dictionary.Add(v, 0);
                        var bytes = HexUtil.ToByteArray(v);
                        var c = new Color32(bytes[0], bytes[1], bytes[2], bytes[3]);
                        colors.Add(c);
                    }
                }

                if (colors.Count < 3)
                    throw new Exception("Colors count < 3");

                Colors = colors.ToArray();
               


            }catch (Exception ex)
            {
                Debug.LogErrorFormat("Error : {0}", ex.Message);
            }
        }
    }
}