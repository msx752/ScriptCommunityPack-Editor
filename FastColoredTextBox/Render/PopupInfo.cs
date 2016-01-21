using System;
using System.Collections.Generic;

namespace FastColoredTextBoxNS.Render
{
    public class PopupToolTip:IDisposable
    {
        public string Comment { get; set; } = "none";

        public string Name { get; set; } = "";

        public String Parameters { get; set; } = "";
        public List<PropertyTypes> Properties { get; set; } = new List<PropertyTypes>();

        public override string ToString()
        {
            string propList = "";
            for (int i = 0; i < Properties.Count; i++)
                propList += " , " + Properties[i].ToString();

            string newName = Name;
            if (newName.IndexOf("@") != -1)
                newName += "\n";

            if (propList.Length > 0)
                propList = propList.Substring(3);

            string valu = string.Format("{0}\r\n {{ {1} }}\r\n{2}\r\nProperties [ {3} ]", newName, Parameters.Replace(",", ", "), Comment, propList);
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
        #endregion
    }
}