using System.Collections.Generic;
using ContentConsole.Data;
using ContentConsole.Filter;
using Moq;
using NUnit.Framework;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class CountAndFilterNegativeWords
    {
        private Mock<INegativeWordRepository> _negativeWordsRepositoryMock;

        [TestCase("Scholes was such a bad player. He had poor technique and was reckless.",3)]
        [TestCase("Gerrard was awful at defending and immature at times", 2)]
        [TestCase("Insane, lousy and ugly are characteristics I associate with Beckham", 3)]
        [Test]
        public void Correct_count_of_bad_words_is_given(string contentToFilter, int expectedNegativeWordCount)
        {
            Given_that_there_is_a_list_of_negative_words();
            var manageContentsNegativeWords = new ManageContent(new FilterBySplittingWordsStrategy(_negativeWordsRepositoryMock.Object));
            Assert.That(manageContentsNegativeWords.CountNegativeWords(contentToFilter),Is.EqualTo(expectedNegativeWordCount));
        }

        [TestCase("Scholes was such a bad player. He had poor technique and was reckless.", "Scholes was such a b#d player. He had p##r technique and was r######s.")]
        [TestCase("Testing non alphabet characters bad, let's see.", "Testing non alphabet characters b#d, let's see.")]
        [Test]
        public void Negative_words_middle_should_be_replaced_with_filtering_enabled(string contentToFilter, string expectedOutput)
        {
            Given_that_there_is_a_list_of_negative_words();
            var manageContentsNegativeWords = new ManageContent(new FilterBySplittingWordsStrategy(_negativeWordsRepositoryMock.Object));
            Assert.That(manageContentsNegativeWords.FilterNegativeWords(contentToFilter), Is.EqualTo(expectedOutput));
        }

        [TestCase("Scholes was such a bad player. He had poor technique and was reckless.", "Scholes was such a b#d player. He had p##r technique and was r######s.")]
        [TestCase("Testing non alphabet characters bad, let's see.", "Testing non alphabet characters b#d, let's see.")]
        public void With_filtering_disabled_the_original_content_is_returned(string contentToFilter, string expectedOutput)
        {
            Given_that_there_is_a_list_of_negative_words();
            var manageContentsNegativeWords = new ManageContent(new FilterBySplittingWordsStrategy(_negativeWordsRepositoryMock.Object, true));
            Assert.That(manageContentsNegativeWords.FilterNegativeWords(contentToFilter), Is.EqualTo(expectedOutput));
        }

        private void Given_that_there_is_a_list_of_negative_words()
        {
            _negativeWordsRepositoryMock = new Mock<INegativeWordRepository>();
            _negativeWordsRepositoryMock.Setup(x => x.GetListOfNegativeWords()).Returns(new List<string>
            {
                "Bad",
                "Poor",
                "Reckless",
                "Awful",
                "Immature",
                "Insane",
                "Lousy",
                "Ugly"
            });
        }
    }
}
