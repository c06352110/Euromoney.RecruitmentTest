using System.Collections.Generic;
using ContentConsole.Data;
using ContentConsole.Filter;
using Moq;
using NUnit.Framework;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class FilterBySplittingWordsStrategyTests
    {
        private Mock<INegativeWordRepository> _negativeWordsRepositoryMock;
        private FilterBySplittingWordsStrategy _filterBySplittingWordsStrategy;
        private int _negativeWordsCount;
        private string _negativeWords;
        private bool _disabledFiltering;

        [TestCase("Scholes was such a bad player. He had poor technique and was reckless.",3)]
        [TestCase("Gerrard was awful at defending and immature at times", 2)]
        [TestCase("Insane, lousy and ugly are characteristics I associate with Beckham", 3)]
        [Test]
        public void Correct_count_of_bad_words_is_given(string contentToFilter, int expectedNegativeWordCount)
        {
            Given_that_there_is_a_list_of_negative_words();
            And_filtering_is_not_enabled();
            And_splitting_by_words_strategy_has_been_chosen();
            When_there_is_a_request_to_count_negative_words_from(contentToFilter);
            Assert.That(_negativeWordsCount, Is.EqualTo(expectedNegativeWordCount));
        }      

        [TestCase("Scholes was such a bad player. He had poor technique and was reckless.", "Scholes was such a b#d player. He had p##r technique and was r######s.")]
        [TestCase("Testing non alphabet characters bad, let's see.", "Testing non alphabet characters b#d, let's see.")]
        [Test]
        public void With_filtering_enabled_negative_words_middle_should_be_replaced_by_hashes(string contentToFilter, string expectedOutput)
        {
            Given_that_there_is_a_list_of_negative_words();
            And_filtering_is_not_enabled();
            And_splitting_by_words_strategy_has_been_chosen();
            When_there_is_a_request_to_filter_negative_words_from(contentToFilter);
            Assert.That(_negativeWords, Is.EqualTo(expectedOutput));
        }

        [TestCase("Scholes was such a bad player. He had poor technique and was reckless.", "Scholes was such a bad player. He had poor technique and was reckless.")]
        [TestCase("Testing non alphabet characters bad, let's see.", "Testing non alphabet characters bad, let's see.")]
        public void With_filtering_disabled_the_original_content_is_returned(string contentToFilter, string expectedOutput)
        {
            Given_that_there_is_a_list_of_negative_words();
            And_filtering_has_been_disabled();
            And_splitting_by_words_strategy_has_been_chosen();
            When_there_is_a_request_to_filter_negative_words_from(contentToFilter);
            Assert.That(_negativeWords, Is.EqualTo(expectedOutput));
        }

        private void And_filtering_has_been_disabled()
        {
            _disabledFiltering = true;
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

        private void When_there_is_a_request_to_count_negative_words_from(string contentToFilter)
        {
            _negativeWordsCount = _filterBySplittingWordsStrategy.CountNegativeWords(contentToFilter);
        }

        private void And_filtering_is_not_enabled()
        {
            _disabledFiltering = false;
           
        }

        private void When_there_is_a_request_to_filter_negative_words_from(string contentToFilter)
        {
            _negativeWords = _filterBySplittingWordsStrategy.FilterNegativeWords(contentToFilter, _disabledFiltering);
        }

        private void And_splitting_by_words_strategy_has_been_chosen()
        {
            _filterBySplittingWordsStrategy = new FilterBySplittingWordsStrategy(_negativeWordsRepositoryMock.Object);
        }
    }
}
