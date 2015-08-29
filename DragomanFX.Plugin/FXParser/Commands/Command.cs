namespace DragomanFX.Plugin.FXParser.Commands
{
    public abstract class Command
    {
        protected Recipe recipe;

        protected Command(string parameters, Recipe recipe)
        {
            this.recipe = recipe;
        }

        public abstract override string ToString();
    }
}