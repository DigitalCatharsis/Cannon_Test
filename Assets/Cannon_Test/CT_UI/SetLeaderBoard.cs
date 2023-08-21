using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;
using Zenject;
using TMPro;

namespace Cannon_Test
{
    public static class Extentions
    {
        public static string GetString<K, V>(this IDictionary<K, V> dict)
        {
            var sb = new StringBuilder();
            sb.Append("LeaderBoard:\n\n");
            foreach (var pair in dict)
            {
                sb.Append(String.Format("{0}\t:\t{1}\n", pair.Key, pair.Value));
            }
            return sb.ToString();
        }
    }

    public class SetLeaderBoard : MonoBehaviour
    {
        [Inject] ScoreData _scoreData;

        private void Start()
        {
            var dict = _scoreData.scoreDictionary;
            string str = dict.GetString();

            GetComponent<TextMeshProUGUI>().text = str;
        }
    }
}

