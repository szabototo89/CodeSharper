using CodeSharper.Core.Texts;

namespace CodeSharper.Core
{
    public class AbstractSyntaxTree
    {
        private readonly TextInformationManager _textInformationManager;

        public AbstractSyntaxTree()
        {
            _textInformationManager = new TextInformationManager();
        }



        public TextInformationManager TextInformationManager
        {
            get { return _textInformationManager; }
        }
    }
}