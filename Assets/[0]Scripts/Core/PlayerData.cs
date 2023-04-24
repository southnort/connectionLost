using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;


namespace ConnectionLost.Core
{
    public sealed class PlayerData
    {
        private static PlayerData _currentData;
        private const string Key = "playerdata";


        public static PlayerData CurrentData
        {
            get
            {
                if (_currentData == null)
                {
                    LoadData();
                }
                return _currentData;
            }
        }

        public float BaseHp => 90f;
        public float BaseDmg => 25f;
        public GridDifficult CurrentDifficult => GridDifficult.Hard;


        private static void LoadData()
        {
            var str = PlayerPrefs.GetString(Key);

            if (str is { Length: > 0 })
            {
                var json = JsonConvert.DeserializeObject<PlayerData>(str);

                if (json == null)
                {
                    _currentData = new PlayerData();
                    _currentData.SaveData();
                }
                else
                {
                    _currentData = (json);
                }
            }
            else
            {
                _currentData = new PlayerData();
                _currentData.SaveData();
            }
        }

        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(this);
            PlayerPrefs.SetString(Key, json);
            PlayerPrefs.Save();
        }
    }
}
