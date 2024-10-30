using NUnit.Framework;
using lab;

namespace PLLabTests
{
    [TestFixture]
    public class ArgParserTests
    {
        [Test]
        public void ArgParser_HasArg_ArgExisted_retTrue()
        {
            // Устанавливаем аргумент командной строки --verbose
            ArgParser.SetArgs("--verbose");
            // По факту тут любая строка пройдет тестирование, не понимаю смысл.
            Assert.That(ArgParser.HasArg("--verbose"), "Arg with name --verbose not found");
        }

        [Test]
        public void ArgParser_HasArg_ArgNotExisted_retFalse()
        {
            ArgParser.SetArgs("--Name", "Ivanov");
            // По факту тут любая строка пройдет тестирование, не понимаю смысл.
            Assert.That(!ArgParser.HasArg("--verbose"), "Arg with name --verbose not found.");
        }

        [Test]
        public void ArgParser_GetArgAsString_ArgExisted_retValue()
        {
            // Установка аргумента командной строки --name = Ivan 
            ArgParser.SetArgs("--Name", "Ivan");
            // Проверка, равен ли аргумент строки --name, Ivan
            Assert.That(ArgParser.GetArgAsString("--Name"), Is.EqualTo("Ivan"));
        }

        [Test]
        public void ArgParser_GetArgAsString_ArgNotExisted_retNull()
        {
            // Установка аргумента командной строки --name = Ivan 
            ArgParser.SetArgs("--Name", "Ivan");
            // Проверка, равно ли значение аргумента --version null
            Assert.That(ArgParser.GetArgAsString("--Version"), Is.Null);
        }
    }
}