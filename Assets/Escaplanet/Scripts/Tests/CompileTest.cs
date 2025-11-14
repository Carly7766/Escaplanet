using System.IO;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.Build.Player;
using Assert = UnityEngine.Assertions.Assert;

namespace Escaplanet.Scripts.Tests
{
    public class CompileTest
    {
        private static readonly string OutputPath = $"Temp/{nameof(CompileTest)}";

        [Test]
        public void CheckCompileWebGL() => CheckCompile(BuildTarget.WebGL);

        private static void CheckCompile(BuildTarget target)
        {
            var input = new ScriptCompilationSettings
            {
                target = target,
                group = BuildPipeline.GetBuildTargetGroup(target),
            };

            var result = PlayerBuildInterface.CompilePlayerScripts(input, OutputPath);
            var assemblies = result.assemblies;
            var isPassed = assemblies is { Count: > 0 } && result.typeDB != null;

            if (Directory.Exists(OutputPath)) Directory.Delete(OutputPath, true);

            Assert.IsTrue(isPassed);
        }
    }
}