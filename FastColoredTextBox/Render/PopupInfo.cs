using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FastColoredTextBoxNS.Render
{
    public class PopupInfo : IDisposable
    {
        public string Name { get; set; } = "";
        public List<String> Parameters { get; set; } = new List<string>();
        public string Comment { get; set; } = "none";
        public List<PropertyTypes> Properties { get; set; } = new List<PropertyTypes>();

        public override string ToString()
        {
            string paramList = "";
            for (int i = 0; i < Parameters.Count; i++)
            {
                paramList += "," + Parameters[i];
            }
            //this.Destination = this.Destination.Replace(",", "\r\n");
            string propList = "";
            for (int i = 0; i < Properties.Count; i++)
            {
                propList += Properties[i].ToString() + ",";
            }
            //string[] lst = this.Destination.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            //lst[0] = lst[0].Replace("[ [", "[");
            //lst[lst.Length - 1] = lst[lst.Length - 1].Replace("] ]", "]");
            //string dest = "";
            //string valu = "";

            //foreach (string item in lst)
            //{
            //    dest = dest + item + "\r\n";
            //}

            string valu = Name + "|" + paramList + "|" + Comment + "|" + propList;

            //valu = valu.Substring(0, valu.Length - 4);
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
        // ~PopupInfo() {
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
