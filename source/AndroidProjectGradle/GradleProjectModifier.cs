using System.Collections.Generic;
using Google.JarResolver;
using GooglePlayServices;
using UnityEngine;
using UnityEditor.Android;
using UnityEditor;

namespace Google
{
    [InitializeOnLoad]
    public class GradleProjectModifier : AndroidProjectFilesModifier
    {
        private static ICollection<Dependency> _dependencies;

        static GradleProjectModifier()
        {
            Debug.Log("GradleProjectModifier Ctor");
            GradleTemplateResolver.OnResolve -= OnResolve;
            GradleTemplateResolver.OnResolve += OnResolve;
        }
        public override void OnModifyAndroidProjectFiles(AndroidProjectFiles projectFiles)
        {
            Debug.Log("OnModifyAndroidProjectFiles");
            Debug.Log(_dependencies.Count);
            foreach (var dependency in _dependencies)
                projectFiles.UnityLibraryBuildGradle.Dependencies.AddDependencyImplementationByName(dependency.Key);
        }

        private static void OnResolve(ICollection<Dependency> obj)
        {
            Debug.Log($"Caching the dependencies  {obj.Count}");
            _dependencies = obj;
        }
    }
}