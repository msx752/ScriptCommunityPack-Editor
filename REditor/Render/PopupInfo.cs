using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FastColoredTextBoxNS.Render
{
    public class PopupToolTip : IDisposable
    {
        public T GetACItem<T>() where T : AutoCompleteItem
        {
            object classType = new object();
            if (Properties.Contains(PropertyTypes.SnippetAuto))
                classType = new SnippetAuto(Name) { ImageIndex =3 };
            else if (Properties.Contains(PropertyTypes.DeclarationAuto))
                classType = new DeclarationAuto(Name) { ImageIndex = 2 };
            else if (Properties.Contains(PropertyTypes.TriggerAuto))
                classType = new MethodAuto(Name) { ImageIndex = 7 };
            else
            {
                classType = new MethodAuto(this);
                (classType as MethodAuto).ImageIndex = 9;
            }
            return classType as T;
        }

        public string Comment { get; set; } = "none";
        private string _name = "";
        public string Name
        {
            get
            {
                if (Properties.Contains(PropertyTypes.DeclarationAuto))
                    return "[" + _name + "]\n";
                else if (Properties.Contains(PropertyTypes.TriggerAuto))
                    return _name + "\n";
                else
                    return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Parameters { get; set; } = "";
        public List<PropertyTypes> Properties { get; set; } = new List<PropertyTypes>();
        public override string ToString()
        {
            string propList = string.Join(",", Properties.ToArray()).Replace(" ", "").Replace(",", ", ");
            string valu = string.Format("{0}\r\n {{ {1} }}\r\n{2}\r\nProperties [ {3} ]", Name, Parameters.Replace(" ","").Replace(",", ", "), Comment, propList);
            return valu;
        }

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~PopupToolTip() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion IDisposable Support
    }
}