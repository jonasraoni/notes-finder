/*
 * Notes Finder: Given a set of notes and a target value, tries to retrieve the right notes to avoid change.
 * Jonas Raoni Soares da Silva <http://raoni.org>
 * https://github.com/jonasraoni/notes-finder
 */
 
using System;
using System.Collections.Generic;

namespace NF {
	/// <summary>
	/// Groups a set of similar notes. It's used internally and also to represent the return of the NotesFinder.Find method
	/// </summary>
	public class NoteItem {
		public uint Note;
		public uint Quantity;
	}
}