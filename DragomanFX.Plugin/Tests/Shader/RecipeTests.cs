using System;
using System.IO;
using DragomanFX.Plugin.Shader;
using DragomanFX.Plugin.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Shader
{
    [TestClass()]
    public class RecipeTests
    {
        [TestMethod()]
        public void RecipeTest()
        {
            Logger.LogToFile = false;
            Recipe mainRecipe = new Recipe(Path.Combine(Environment.CurrentDirectory, "MasterEffect.h"));
            Logger.LogLine(mainRecipe.ToString());
        }
    }
}