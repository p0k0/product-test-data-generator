using lib;

using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

using CommandLine;
using CommandLine.Text;



namespace separator
{
    class Options
    {
        [Option("stdin", Default = false, HelpText = "Read from stdin")]
        public bool stdin { get; set; }

        [Option('i', "in", HelpText = "Input file name")]
        public string inputFile { get; set; }

        [Option('o', "out", HelpText = "Output files",
                Default = new string[] {
                    "products-0.dat",
                    "products-1.dat",
                    "products-2.dat",
                    "products-3.dat",
                    "products-4.dat",
                    "products-5.dat",
                    "products-6.dat",
                    "products-7.dat",
                    "products-8.dat",
                    "products-9.dat",
                })]
        public IEnumerable<string> outputFiles { get; set; }

        [Usage(ApplicationAlias = "separator.exe")]
        public static IEnumerable<Example> Examples 
        {
            get 
            {
                yield return new Example("separate (by price) generated products from file to files", 
                                         UnParserSettings.WithGroupSwitchesOnly(), 
                                         new Options{ 
                                             inputFile = "products.dat",
                                             outputFiles = new string[] {"product-0.dat", "product-1.dat", "product-2.dat", "...", "product-9.dat"}
                                         });
                yield return new Example("separate (by price) generated products from stdin to default files",
                                         UnParserSettings.WithGroupSwitchesOnly(),
                                         new Options { 
                                             stdin = true
                                         });
            }
        }
    }

    class Program
    {
        
        // 1 create separator instance 
        // 2 create bucketManager instance
        // 3 register separator at bucketManager
        // 4 run separator
        // 5 bucketManager store collected data
        static void Main(string[] args)
        {
            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed<Options>(o =>
                    {
                        if (o.stdin)
                            FromStdin(o.outputFiles);
                        else
                            FromFile(o.inputFile, o.outputFiles);
                    }
                );
        }

        private static void FromFile(string inputFileName, IEnumerable<string> outputFiles)
        {
            if (string.IsNullOrEmpty(inputFileName))
            {
                throw new ArgumentException($"'{nameof(inputFileName)}' cannot be null or empty", nameof(inputFileName));
            }

            var separatorChar = ';';
            var inputFilePath = System.IO.Path.Combine(Environment.CurrentDirectory, inputFileName);
            var outputFilesPaths = outputFiles.Select(outputfile => System.IO.Path.Combine(Environment.CurrentDirectory, outputfile)).ToArray();
            
            var separator = new Separator(separatorChar);
            var bucketManager = new BucketManager();
            
            bucketManager.RegisterProductPublisher(separator);
            using (var stream = new FileStream(inputFilePath, FileMode.Open))
            {
                separator.Visit(stream);
            }

            bucketManager.PersistCollectedData(outputFilesPaths, separatorChar);
        }

        private static void FromStdin(IEnumerable<string> outputFiles)
        {
            var separatorChar = ';';
            var outputFilesPaths = outputFiles.Select(outputfile => System.IO.Path.Combine(Environment.CurrentDirectory, outputfile)).ToArray();
            
            var separator = new Separator(separatorChar);
            var bucketManager = new BucketManager();
            
            bucketManager.RegisterProductPublisher(separator);
            using(Stream stream = Console.OpenStandardInput())
            {
                separator.Visit(stream);
            }

            bucketManager.PersistCollectedData(outputFilesPaths, separatorChar);
        }
    }
}
