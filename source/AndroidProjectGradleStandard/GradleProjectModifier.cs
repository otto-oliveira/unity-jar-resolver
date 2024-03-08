using System;
using System.IO;
using UnityEngine;
using UnityEditor.Android;
using Unity.Android.Gradle;

namespace Google
{
    public class GradleProjectModifier : AndroidProjectFilesModifier
    {
        internal readonly string SrcAARExtension = ".srcaar";
        internal readonly string POMExtension = ".pom";

        [Serializable]
        class Data
        {
            public bool Enabled;
            public string[] Repositories;
            public string[] Dependencies;
            public string[] PomFiles;
        }

        private static string CalculateDestinationPath(string sourcePath)
        {
            var root = Application.dataPath;
            return Path.Combine(Constants.LocalRepository, sourcePath.Substring(root.Length + 1)); ;
        }

        
        public override void OnModifyAndroidProjectFiles(AndroidProjectFiles projectFiles)
        {
            Debug.Log("OnModifyAndroidProjectFiles");
            var data = projectFiles.GetData<Data>(nameof(Data));
            if (!data.Enabled)
                return;
            foreach (var dependency in data.Dependencies)
                projectFiles.UnityLibraryBuildGradle.Dependencies.AddDependencyImplementationByName(dependency);

            var gradleRepositories = projectFiles.GradleSettings.DependencyResolutionManagement.Repositories;
            foreach (var repository in data.Repositories)
            {
                var block = new Block("maven");
                gradleRepositories.AddElement(block);
                block.AddElement(new Element($"url \"{repository}\""));
                // TALK TO RYTIS
            }
            // TODO: mavenLocal ?

            foreach (var file in data.PomFiles)
            {
                var contents = File.ReadAllText(file);
                contents = contents.Replace("srcaar", "aar");
                projectFiles.SetFileContents(CalculateDestinationPath(file), contents);
            }

        }
    }
}