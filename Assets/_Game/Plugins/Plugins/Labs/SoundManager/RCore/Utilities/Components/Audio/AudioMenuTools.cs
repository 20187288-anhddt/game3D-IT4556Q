#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Utilities.Components
{
    public class AudioMenuTools : Editor
    {
        [MenuItem("RUtilities/Audio/Add Audio Manager")]
        private static void AddAudioManager()
        {
            var manager = GameObject.FindObjectOfType<AudioManager>();
            if (manager == null)
            {
                var obj = new GameObject("AudioManager");
                obj.AddComponent<AudioManager>();
            }
        }

        [MenuItem("RUtilities/Audio/Open Audio Collection")]
        private static void OpenAudioCollection()
        {
            Selection.activeObject = AudioCollection.Instance;
        }
    }
}
#endif