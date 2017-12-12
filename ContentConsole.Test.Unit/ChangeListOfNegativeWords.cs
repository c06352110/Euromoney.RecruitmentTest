using System.Collections.Generic;
using NUnit.Framework;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class ChangeListOfNegativeWords
    {
        [Test]
        public void Add_a_word_to_list_of_negative_words()
        {
            var repo = new NegativeWordRepository(new List<string>());
            repo.AddAWord("Bad");
            var returned = repo.GetListOfNegativeWords();
            Assert.That(returned.Contains("Bad"),Is.EqualTo(true));
        }

        [Test]
        public void Remove_a_word_from_list_of_negative_words()
        {
            var repo = new NegativeWordRepository(new List<string>());
            repo.AddAWord("Bad");
            repo.RemoveAWord("Bad");
            var returned = repo.GetListOfNegativeWords();
            Assert.That(returned.Contains("Bad"), Is.EqualTo(false));
        }
    }
}
