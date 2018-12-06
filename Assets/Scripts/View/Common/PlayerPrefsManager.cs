using UnityEditor;
using UnityEngine;

namespace Krk.Bum.View.Common
{
    public class PlayerPrefsManager : MonoBehaviour
    {
        [MenuItem("Krk/Clear Prefs")]
        static void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}
