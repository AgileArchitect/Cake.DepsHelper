using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.IO;
using NUnit.Framework;

namespace Cake.DepsHelper.Test
{
    [TestFixture]
    public class TestDepsHelper
    {
        [Test]
        public void TestPackageDependencies()
        {
            var dir = Directory.GetParent(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).FullName).FullName;

            var packages = System.IO.Path.Combine(dir, "packages.config");

            var deps = DepsHelperAliases.PackageDependencies(null, new FilePath(packages)).ToList();

            Assert.IsNotEmpty(deps);
            Assert.That(deps.Any(d => d.Name == "Cake.Core"));
            Assert.That(deps.Any(d => d.Name == "NUnit"));
        }
    }
}
