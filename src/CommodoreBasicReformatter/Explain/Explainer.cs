namespace CommodoreBasicReformatter.Explain
{
    public interface IExplainer
    {
        void AddExplanations(GrammarProgram program);
    }

    public class Explainer : IExplainer
    {
        public void AddExplanations(GrammarProgram program)
        {
            new MemoryExplainer().AddExplanations(program);
            new ChrExplainer().AddExplanations(program);
        }
    }
}