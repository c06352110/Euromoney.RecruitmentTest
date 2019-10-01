using ContentConsole.Data;
using NUnit.Framework;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class NegativeWordsRepositoryTests
    {
        [Test]
        public void Add_a_word_to_list_of_negative_words()
        {
            var repo = new NegativeWordRepository();
            repo.AddAWord("SomeNewBadWord");
            var returned = repo.GetListOfNegativeWords();
            Assert.That(returned.Contains("SomeNewBadWord"),Is.EqualTo(true));
        }

        [Test]
        public void Remove_a_word_from_list_of_negative_words()
        {
            var repo = new NegativeWordRepository();
            repo.AddAWord("SomeNewBadWord");
            repo.RemoveAWord("SomeNewBadWord");
            var returned = repo.GetListOfNegativeWords();
            Assert.That(returned.Contains("SomeNewBadWord"), Is.EqualTo(false));
        }
    }
}
