using System;
using System.IO;
using AboditNLP.Verb;
using Dictionary.Dictionary;
using Microsoft.AspNetCore.Hosting;
using Syn.WordNet;

namespace synWordNetTest
{
    public class Program
    {
        public static string directory = Directory.GetCurrentDirectory();
        public static WordNetEngine wordNet = new WordNetEngine();

        public void load()
        {

            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adj")), PartOfSpeech.Adjective);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.adv")), PartOfSpeech.Adverb);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.noun")), PartOfSpeech.Noun);
            wordNet.AddDataSource(new StreamReader(Path.Combine(directory, "data.verb")), PartOfSpeech.Verb);

            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adj")), PartOfSpeech.Adjective);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.adv")), PartOfSpeech.Adverb);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.noun")), PartOfSpeech.Noun);
            wordNet.AddIndexSource(new StreamReader(Path.Combine(directory, "index.verb")), PartOfSpeech.Verb);

            Console.WriteLine("Loading data files...");
            wordNet.Load();
            Console.WriteLine("Files loaded successfully.");
        }
        public void run()
        {
            while (true)
            {
                Console.Write("\nType first word:");

                var word = Console.ReadLine();
                var synSetList = wordNet.GetSynSets(word);
				//System.Collections.Generic.List<SynSet> synSetList
                if (synSetList.Count == 0) Console.WriteLine($"No SynSet found for '{word}'");

                foreach (var synSet in synSetList)
                {
                    var words = string.Join(", ", synSet.Words);

                    Console.WriteLine($"\nWords: {words}");
                    Console.WriteLine($"Part Of Speech: {synSet.PartOfSpeech}");
                    Console.WriteLine($"Gloss: {synSet.Gloss}");
                }
            }
        }
        static void Main(string[] args)
        {
            Program prg = new Program();
            prg.load();
            prg.run();
        }
    }
}
