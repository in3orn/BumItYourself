using UnityEngine;

namespace Krk.Bum.Model.Utils
{
    public class PrefsWrapper
    {
        private const string PropertyFormat = "{0}-{1}";


        public bool GetBool(string id, string property)
        {
            return GetBool(GetKey(id, property));
        }

        public int GetInt(string id, string property)
        {
            return GetInt(GetKey(id, property));
        }

        public float GetFloat(string id, string property)
        {
            return GetFloat(GetKey(id, property));
        }

        public string GetString(string id, string property)
        {
            return GetString(GetKey(id, property));
        }


        public bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) > 0;
        }

        public int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }



        public void SetBool(string id, string property, bool value)
        {
            SetBool(GetKey(id, property), value);
        }

        public void SetInt(string id, string property, int value)
        {
            SetInt(GetKey(id, property), value);
        }

        public void SetFloat(string id, string property, float value)
        {
            SetFloat(GetKey(id, property), value);
        }

        public void SetString(string id, string property, string value)
        {
            SetString(GetKey(id, property), value);
        }


        public void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        private string GetKey(string id, string property)
        {
            return string.Format(PropertyFormat, id, property);
        }
    }
}
