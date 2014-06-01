namespace CodeSharper.Core.Texts
{
    public class TextInformation
    {
        private readonly MutableNode _node;

        #region Constructors

        public TextInformation(MutableNode node)
        {
            _node = node;
        }

        #endregion

        public string FullText
        {
            get { return string.Empty; }
        }

        public TextSpan TextSpan
        {
            get { return new TextSpan(); }
        }

        public MutableNode GetNode()
        {
            return _node;
        }
    }
}