using ContentConsole.Filter;
using Moq;
using NUnit.Framework;

namespace ContentConsole.Test.Unit
{
    [TestFixture]
    public class FilterContextTests
    {
        private readonly Mock<IFilterStrategy> _filterStrategy = new Mock<IFilterStrategy>();
        private string _someContentToFilter;
        private bool _disableFiltering;
        private string _requestedContent;
        private bool _requestedDisableFiltering;

        [TestCase("Some content",true)]
        [TestCase("Some content",false)]
        public void Correct_request_is_sent_to_be_filtered(string content, bool disableFiltering)
        {
            Given_that_there_is_content_to_filter(content);
            And_filtering_has_been_disabled(disableFiltering);
            When_a_request_is_sent_to_filter_content();
            Assert.That(_requestedContent,Is.EqualTo(_someContentToFilter));
            Assert.That(_requestedDisableFiltering, Is.EqualTo(_disableFiltering));
        }

        [TestCase("Some content")]
        public void Correct_request_is_sent_to_be_filtered(string content)
        {
            Given_that_there_is_content_to_filter(content);
            When_a_request_is_sent_to_count_negative_words();
            Assert.That(_requestedContent, Is.EqualTo(_someContentToFilter));
        }

        #region Setup
        [SetUp]
        public void SetUp()
        {
            _filterStrategy.Setup(x => x.FilterNegativeWords(It.IsAny<string>(), It.IsAny<bool>()))
                .Callback<string, bool>((s, b) =>
                {
                    _requestedContent = s;
                    _requestedDisableFiltering = b;
                });
            _filterStrategy.Setup(x => x.CountNegativeWords(It.IsAny<string>()))
                .Callback<string>((s) =>
                {
                    _requestedContent = s;
                });
        }
        #endregion

        private void And_filtering_has_been_disabled(bool disableFiltering)
        {
            _disableFiltering = disableFiltering;
        }

        private void When_a_request_is_sent_to_filter_content()
        {
            var filterContext = new FilterContext(_filterStrategy.Object);
            filterContext.FilterNegativeWords(_someContentToFilter, _disableFiltering);
        }

        private void When_a_request_is_sent_to_count_negative_words()
        {
            var filterContext = new FilterContext(_filterStrategy.Object);
            filterContext.CountNegativeWords(_someContentToFilter);
        }

        private void Given_that_there_is_content_to_filter(string content)
        {
            _someContentToFilter = content;
        }
    }
}
