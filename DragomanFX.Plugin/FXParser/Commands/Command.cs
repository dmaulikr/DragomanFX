using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DragomanFX.Plugin.FXParser.Commands
{
    public abstract class Command
    {
        protected Recipe recipe;

        protected Command(Recipe recipe)
        {
            this.recipe = recipe;
        }

        public abstract Command Parse(string parameters);

        public abstract override string ToString();
    }
}
