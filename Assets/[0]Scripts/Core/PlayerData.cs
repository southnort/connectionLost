using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;


namespace ConnectionLost.Core
{
    public sealed class PlayerData
    {
        private static PlayerData _currData;
        private const string _key = "playerdata";

        public float BaseHp => 90f;
        public float BaseDmg => 25f;
        public GridDifficult CurrentDifficult => GridDifficult.Easy;

        public static PlayerData CurrentData
        {
            get
            {
                if (_currData == null)
                {
                    LoadData();
                }
                return _currData;
            }
        }

        private static void LoadData()
        {
            var str = PlayerPrefs.GetString(_key);

            if (str != null && str.Length > 0)
            {
                var json = JsonConvert.DeserializeObject<PlayerData>(str);

                if (json == null)
                {
                    _currData = new PlayerData();
                    _currData.SaveData();
                }
                else
                {
                    _currData = (json);
                }
            }
            else
            {
                _currData = new PlayerData();
                _currData.SaveData();
            }
        }

        public void SaveData()
        {
            string json = JsonConvert.SerializeObject(this);
            PlayerPrefs.SetString(_key, json);
            PlayerPrefs.Save();
        }
    }
}
