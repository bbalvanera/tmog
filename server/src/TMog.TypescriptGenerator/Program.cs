using System;
using System.Configuration;
using System.IO;
using TMog.WebApi.Models;
using TypeLite;

namespace TMog.TypescriptGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var output = TypeScript.Definitions(new TMogTsGenerator())
                .For<Item>()
                .For<Slot>()
                .For<Source>()
                .For<TMogSet>()
                .For<Zone>()
                
                .WithModuleNameFormatter(module => "")
                .WithVisibility((tsClass, type) => true)
                .WithMemberFormatter(identifier => char.ToLower(identifier.Name[0]) + identifier.Name.Substring(1))
                .WithIndentation("  ")
                .Generate(TsGeneratorOutput.Enums | TsGeneratorOutput.Fields | TsGeneratorOutput.Properties);

            Console.WriteLine(output);
            WriteOutput(output);
        }

        static void WriteOutput(string output)
        {
            var target = ConfigurationManager.AppSettings.Get("outputPath");
            var path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.IndexOf("server"));

            target = Path.Combine(path, target, "models.ts");

           File.WriteAllText(target, output);
        }
    }
}
