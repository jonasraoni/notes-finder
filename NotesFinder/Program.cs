/*
 * Notes Finder: Given a set of notes and a target value, tries to retrieve the right notes to avoid change.
 * Jonas Raoni Soares da Silva <http://raoni.org>
 * https://github.com/jonasraoni/notes-finder
 */
 
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NF {
	class Program {
		const char SEPARATOR = ';';

		static async Task Main(string[] args) {
			if (args.Length < 2) {
				Console.WriteLine($"Usage:\n{AppDomain.CurrentDomain.FriendlyName} inFilePath outFilePath");
				Environment.Exit(2);
			}
			try {
				using (var reader = File.OpenText(args[0])) {
					using (var writer = File.CreateText(args[1])) {
						for (string line; (line = await reader.ReadLineAsync()) != null;) {
							var input = line.TrimEnd(SEPARATOR).Split(new[] { SEPARATOR });
							var targetValue = uint.Parse(input[0]);
							var finder = new NotesFinder();
							for (var i = 0; ++i < input.Length;) {
								var note = uint.Parse(input[i]);
								//discard notes higher than the targetValue
								if (note > targetValue)
									continue;
								finder.AddNote(note);
								//exact note found, discard the remaining notes
								if (note == targetValue)
									break;
							}

							var notes = finder.Find(targetValue);
							if (notes == null)
								await writer.WriteLineAsync("NO");
							else {
								var sb = new StringBuilder();
								foreach (var note in notes)
									sb.Append(new StringBuilder().Insert(0, note.Note + "" + SEPARATOR, (int)note.Quantity));
								sb.Remove(sb.Length - 1, 1);
								await writer.WriteLineAsync(sb.ToString());
							}
						}
					}
				}
			}
			catch (Exception e) {
				Console.Error.WriteLine("An exception has occurred: " + e);
				Environment.Exit(1);
			}
		}
	}
}
