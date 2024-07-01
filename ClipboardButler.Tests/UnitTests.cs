using System;
using Xunit;

namespace ClipboardButler.Tests
{
    public class UnitTests
    {
        [Theory]
        [InlineData("https://youtu.be/7aQ2VdV_S_Y?si=gx0Hcg3hF9fWKcKh", "https://youtu.be/7aQ2VdV_S_Y")]
        [InlineData("https://www.youtube.com/watch?v=7aQ2VdV_S_Y&ab_channel=nopara73", "https://www.youtube.com/watch?v=7aQ2VdV_S_Y")]
        public void YouTube(string input, string expected)
        {
            // Act
            bool result = TextCleaner.TryClean(input, out string actual);

            // Assert
            Assert.True(result);
            Assert.Equal(expected, actual);
        }
    }
}
