using lib;

using System;
using System.IO;
using System.Collections.Generic;

using CommandLine;
using CommandLine.Text;

namespace generator
{
    class Options
    {
        [Option("stdout", Default = false, HelpText = "Write to stdout")]
        public bool stdout { get; set; }

        [Option('n', "name", HelpText = "Output file name")]
        public string fileName { get; set; }

        [Usage(ApplicationAlias = "generator.exe")]
        public static IEnumerable<Example> Examples 
        {
            get 
            {
                yield return new Example("generate products to file", UnParserSettings.WithGroupSwitchesOnly(), new Options{ fileName = "products.dat" });
                yield return new Example("generate products to stdout", UnParserSettings.WithGroupSwitchesOnly(), new Options{ stdout = true });
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                    {
                        if (o.stdout)
                            WriteToStdout();
                        else
                            WriteToFile(o.fileName);
                    }
                );
        }

        static void WriteToStdout()
        {
            var separateChar = ';';
            var generator = new Generator(separateChar);
            
            using(Stream stream = Console.OpenStandardOutput())
            {
                generator.WriteRandomData(stream);
            }
        }

        static void WriteToFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty", nameof(fileName));

            var filePath = System.IO.Path.Combine(Environment.CurrentDirectory, fileName);
            var separateChar = ';';
            var generator = new Generator(separateChar);

            using(var fileStream = new FileStream(filePath, FileMode.Create))
            {
                generator.WriteRandomData(fileStream);
            }

        }
    }
}
