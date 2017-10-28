using FluentAssertions;
using System.IO;
using Xunit;

namespace VESR
{
    public class IntegrationTest
    {
        [Theory]
        [InlineData("../../../../Private/VesrPost.txt", "../../../../Private/VesrPost.xml")]
        public void ConvertSampleInputToOutput(string sampleXmlInputFilePath, string sampleEsrOutputFilePath)
        {
            byte[] expectedEsrFile = File.ReadAllBytes(sampleEsrOutputFilePath);

            var inputXmlStream = File.OpenRead(sampleXmlInputFilePath);
            var outputEsrStream = new MemoryStream();

            ISO20022ToEsrConverter.Convert(inputXmlStream, outputEsrStream);

            var actualEsrFile = outputEsrStream.ToArray();

            actualEsrFile.Should().BeEquivalentTo(expectedEsrFile);
        }
    }
}
