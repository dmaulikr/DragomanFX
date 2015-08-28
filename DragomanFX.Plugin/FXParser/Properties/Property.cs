using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DragomanFX.Plugin.FXParser.Properties
{
    public abstract class Property
    {
        protected Property(Match match) { }

        public abstract override string ToString();
    }
}
