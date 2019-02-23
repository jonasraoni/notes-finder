/*
 * Notes Finder: Given a set of notes and a target value, tries to retrieve the right notes to avoid change.
 * Jonas Raoni Soares da Silva <http://raoni.org>
 * https://github.com/jonasraoni/notes-finder
 */
 
using System;
using System.Collections.Generic;

namespace NF {
	/// <summary>
	/// Given a set of notes and a target value, tries to retrieve the right notes to avoid change
	/// </summary>
	public class NotesFinder {
		private SortedDictionary<uint, uint> notesQuantity = new SortedDictionary<uint, uint>();
		/// <summary>
		/// Adds a note to the pile
		/// </summary>
		/// <param name="note">The note</param>
		public void AddNote(uint note) {
			if (notesQuantity.ContainsKey(note))
				++notesQuantity[note];
			else
				notesQuantity[note] = 1;
			isPrepared = false;
		}
		/// <summary>
		/// Tries to find the exact set of notes given a target value
		/// </summary>
		/// <param name="targetValue">The desired value</param>
		/// <returns>A stack with the notes on success, null on failure</returns>
		public Stack<NoteItem> Find(uint targetValue) {
			Prepare();
			//the initial index will be always notes.Length, since I'm discarding bigger notes on the Program.Main. But I've left it for correctess.
			var index = Array.BinarySearch(notesValues, targetValue);
			if (index < 0)
				index = ~index;
			else
				++index;

			for (var stack = new Stack<NoteItem>(); ;) {
				//loop through the notes from biggest to smallest
				while (index-- > 0) {
					var note = notesValues[index];
					//quit if the quantity of this note + smaller notes isn't enough
					if (notesSum[index] < targetValue)
						break;
					//skips the current note if it's bigger than needed
					if (note > targetValue)
						continue;
					//finds the maximum quantity of notes
					uint quantity = Math.Min(targetValue / note, notesQuantity[note]);
					//quit if it's the last note and no success
					if (index == 0 && quantity * note != targetValue)
						break;
					//add the notes to the pile 
					stack.Push(new NoteItem {
						Note = note,
						Quantity = quantity
					});
					//success achieved, return it
					if ((targetValue -= quantity * note) == 0)
						return stack;
				}
				//if there's notes on the pile, we can try another combination
				if (stack.Count > 0) {
					var current = stack.Peek();
					//remove one note from the stack and try again
					targetValue += current.Note;
					index = notesIndex[current.Note];
					//if there's no more notes of this type, release it
					if (--current.Quantity == 0)
						stack.Pop();
				}
				else
					break;
			}
			return null;
		}

		/// <summary>
		/// Keeps a simple array with the notes
		/// </summary>
		private uint[] notesValues;
		/// <summary>
		/// Maps a note to it's index
		/// </summary>
		private Dictionary<uint, int> notesIndex = new Dictionary<uint, int>();
		/// <summary>
		/// "Prefix sums" of notes from biggest to smallest
		/// </summary>
		private uint[] notesSum;
		/// <summary>
		/// Used to invalidate the cache
		/// </summary>
		private bool isPrepared;

		/// <summary>
		/// Caches helper variables
		/// </summary>
		private void Prepare() {
			if (isPrepared)
				return;
			notesValues = new uint[notesQuantity.Count];
			notesSum = new uint[notesQuantity.Count];
			notesQuantity.Keys.CopyTo(notesValues, 0);
			notesIndex.Clear();
			uint sum = 0;
			for (var i = -1; ++i < notesValues.Length;) {
				notesSum[i] = sum += notesValues[i] * notesQuantity[notesValues[i]];
				notesIndex[notesValues[i]] = i;
			}
			isPrepared = true;
		}
	}
}