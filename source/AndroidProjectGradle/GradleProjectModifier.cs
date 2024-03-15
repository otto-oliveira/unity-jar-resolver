using System;
using System.IO;
using UnityEngine;
using UnityEditor.Android;
using Unity.Android.Gradle;

namespace Google
{
    public class GradleProjectModifier : AndroidProjectFilesModifier
    {
        public override void OnModifyAndroidProjectFiles(AndroidProjectFiles projectFiles)
        {
            Debug.Log("OnModifyAndroidProjectFiles");
        }
    }
}