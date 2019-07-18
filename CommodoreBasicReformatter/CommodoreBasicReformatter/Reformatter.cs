using System.Text;

namespace CommodoreBasicReformatter
{
    public class Reformatter
    {
        public string Reformat(string fileContent)
        {
            var g = new C64BasicGrammar(fileContent);
            var tokens = g.Parse();
            var sb=new StringBuilder();
            tokens.ForEach(x => sb.AppendLine(x.ToString()));
            return sb.ToString();
        }
    }
}