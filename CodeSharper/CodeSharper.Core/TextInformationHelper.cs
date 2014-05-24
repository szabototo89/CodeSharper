namespace CodeSharper.Core
{
    public static class TextInformationHelper
    {
        public static TextInformation GetTextInformation(this MutableNode node)
        {
            return new TextInformation(node);
        }
    }
}