/*
 * Notes Finder: Given a set of notes and a target value, tries to retrieve the right notes to avoid change.
 * Jonas Raoni Soares da Silva <http://raoni.org>
 * https://github.com/jonasraoni/notes-finder
 */
 
using NF;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NFTests {
	[TestClass]
	public class NotesFinderTest {
		public NotesFinder CreateNotesFinder(params uint[] notes) {
			var nf = new NotesFinder();
			foreach (var note in notes)
				nf.AddNote(note);
			return nf;
		}
		public bool CheckSumOfNotes(uint target, NotesFinder nf) {
			if (nf.Find(target) is Stack<NoteItem> notes) {
				uint sum = 0;
				foreach (var note in notes)
					sum += note.Quantity * note.Note;
				return sum == target;
			}
			return false;
		}
		[TestMethod]
		[Description("Find should return null if it's not possible to give the right quantity of notes")]
		public void Find_Impossible_ReturnsNull() {
			Assert.IsNull(CreateNotesFinder(50, 70, 120, 150).Find(100));
			Assert.IsNull(CreateNotesFinder(1, 2).Find(30));
		}
		[TestMethod]
		[Description("Find should return the right amount of notes")]
		public void Find_Possible_ReturnsRightSum() {
			Assert.IsTrue(CheckSumOfNotes(100, CreateNotesFinder(100)));
			Assert.IsTrue(CheckSumOfNotes(12, CreateNotesFinder(1, 9, 7, 3, 5)));
			Assert.IsTrue(CheckSumOfNotes(30, CreateNotesFinder(60, 15, 9, 9, 9, 8, 8, 5, 5, 3, 3, 3, 3, 3, 2, 2, 2, 1)));
			Assert.IsTrue(CheckSumOfNotes(2147483647, CreateNotesFinder(2147483500, 100, 40, 5, 1, 1, 1, 1)));
			Assert.IsTrue(CheckSumOfNotes(100, CreateNotesFinder(100)));

			Assert.IsTrue(CheckSumOfNotes(13, CreateNotesFinder(9, 7, 5, 2, 1)));
			Assert.IsTrue(CheckSumOfNotes(130, CreateNotesFinder(50, 50, 40, 40)));
			Assert.IsTrue(CheckSumOfNotes(120, CreateNotesFinder(100, 51, 40, 40, 30, 30, 30, 30)));
			Assert.IsTrue(CheckSumOfNotes(4, CreateNotesFinder(3, 3, 2, 2, 2)));
			Assert.IsTrue(CheckSumOfNotes(9, CreateNotesFinder(3, 3, 2, 2, 2, 2, 1)));
			Assert.IsTrue(CheckSumOfNotes(73, CreateNotesFinder(40, 40, 30, 30, 2, 2, 1)));
		}
	}
}