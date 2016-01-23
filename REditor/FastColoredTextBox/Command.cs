namespace FastColoredTextBoxNS
{
    internal abstract class Command
    {
        internal TextSource ts;

        protected Command()
        {
        }

        public abstract void Execute();
    }
}