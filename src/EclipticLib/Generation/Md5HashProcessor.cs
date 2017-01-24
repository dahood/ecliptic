using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using EclipticLib.Extensions;
using EclipticLib.Generation.Domain;
using EclipticLib.Generation.GherkinParsers;

namespace EclipticLib.Generation
{
    /// <summary>
    /// This Hash Processor will create and compare Md5 Hashs of the resulting feature file from the Excel to see if it has changed from what was previously generated.
    /// Only if hashes have changed do we persist the new copy of the feature file and generate a feature class file.
    /// </summary>
    public class Md5HashProcessor : IMd5HashProcessor
    {
        private readonly string md5HashFile;
        private readonly List<Md5HashForFile> md5Hashes;

        private Md5HashProcessor(string md5HashFile)
        {
            this.md5HashFile = md5HashFile;
            md5Hashes = ReadMd5HashesForSpreadsheetFiles();
        }

        private List<Md5HashForFile> ReadMd5HashesForSpreadsheetFiles()
        {
            if (md5HashFile.IsEmpty() || !File.Exists(md5HashFile))
            {
                return new List<Md5HashForFile>();
            }
            string[] readAllLines = File.ReadAllLines(md5HashFile, Encoding.UTF8);
            return readAllLines.Where(line => line.IsNotEmpty() && line.Split('\t').Length == 2).Select(line =>
            {
                string[] items = line.Split('\t');
                {
                    return new Md5HashForFile(items[0], items[1]);
                }
            }).ToList();
        }

        private string CreateMd5Hash(Feature feature)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(GetBytes(feature.ToString()));

            var sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public bool HasChanged(IExcelFeatureParser featureParser, string spreadsheetFile)
        {
            var hashForFile = md5Hashes.SingleOrDefault(h => string.Equals(h.SpreadSheetpath, spreadsheetFile));
            if (hashForFile == null)
            {
                return true;
            }
            var feature = featureParser.Parse(spreadsheetFile);

            var hasChanged = CreateMd5Hash(feature) != hashForFile.Md5Value;
            if (!hasChanged)
            {
                Console.WriteLine("Skipping un-changed file:  " + spreadsheetFile);
            }
            return hasChanged;
        }

        public void UpdateHash(string spreadsheetPath, Feature feature)
        {
            if (md5HashFile.IsEmpty())
                return;

            var hashForFile = md5Hashes.FirstOrDefault(h => string.Equals(h.SpreadSheetpath, spreadsheetPath));

            var md5Value = CreateMd5Hash(feature);
            if (hashForFile == null)
            {
                md5Hashes.Add(new Md5HashForFile(spreadsheetPath, md5Value));
            }
            else
            {
                hashForFile.Md5Value = md5Value;
            }
        }

        private class Md5HashForFile
        {
            public string Md5Value { get; set; }
            public string SpreadSheetpath { get; private set; }

            public Md5HashForFile(string spreadSheetpath, string md5Value)
            {
                Md5Value = md5Value;
                SpreadSheetpath = spreadSheetpath;
            }
        }

        public void UpdateHashStorageFile()
        {
            if (md5HashFile.IsEmpty())
                return;

            // Re-wrte the Md5 Hash file based on the new contents
            File.WriteAllLines(md5HashFile, md5Hashes.Select(x => string.Join("\t", x.SpreadSheetpath, x.Md5Value)), Encoding.UTF8);
        }

        public static IMd5HashProcessor Create(EclipticProperties properties)
        {
            var hashFile = properties.Md5HashFile;
            if (hashFile.IsEmpty())
            {
                return new NullHashProcessor();
            }
            Console.WriteLine("Found Ecliptic Hash File...");
            return new Md5HashProcessor(hashFile);
        }
    }
}