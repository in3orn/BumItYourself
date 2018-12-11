using UnityEditor;
using UnityEngine;

namespace Krk.Bum.Editor
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
