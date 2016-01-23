using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FastColoredTextBoxNS
{
    public class FileTextSource : TextSource, IDisposable
    {
        private Encoding fileEncoding;
        private FileStream fs;
        private List<int> sourceFileLinePositions;
        private System.Windows.Forms.Timer timer;

        public FileTextSource(FastColoredTextBox currentTB) : base(currentTB)
        {
            this.sourceFileLinePositions = new List<int>();
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Interval = 0x2710;
            this.timer.Tick += new EventHandler(this.timer_Tick);
            this.timer.Enabled = true;
        }

        public event EventHandler<LineNeededEventArgs> LineNeeded;

        public event EventHandler<LinePushedEventArgs> LinePushed;

        public override Line this[int i]
        {
            get
            {
                if (base.lines[i] == null)
                {
                    this.LoadLineFromSourceFile(i);
                }
                return base.lines[i];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void Clear()
        {
            base.Clear();
        }

        public override void ClearIsChanged()
        {
            foreach (Line line in base.lines)
            {
                if (line != null)
                {
                    line.IsChanged = false;
                }
            }
        }

        public void CloseFile()
        {
            if (this.fs != null)
            {
                this.fs.Dispose();
            }
            this.fs = null;
        }

        public void Dispose()
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
        {
            if (this.fs != null)
            {
                this.fs.Dispose();
            }
            this.timer.Dispose();
        }

        public override int GetLineLength(int i)
        {
            if (base.lines[i] == null)
            {
                return 0;
            }
            return base.lines[i].Count;
        }

        public override void InsertLine(int index, Line line)
        {
            this.sourceFileLinePositions.Insert(index, -1);
            base.InsertLine(index, line);
        }

        public override bool LineHasFoldingEndMarker(int iLine)
        {
            if (base.lines[iLine] == null)
            {
                return false;
            }
            return !string.IsNullOrEmpty(base.lines[iLine].FoldingEndMarker);
        }

        public override bool LineHasFoldingStartMarker(int iLine)
        {
            if (base.lines[iLine] == null)
            {
                return false;
            }
            return !string.IsNullOrEmpty(base.lines[iLine].FoldingStartMarker);
        }

        public void OpenFile(string fileName, Encoding enc)
        {
            this.Clear();
            if (this.fs != null)
            {
                this.fs.Dispose();
            }
            this.fs = new FileStream(fileName, FileMode.Open);
            long length = this.fs.Length;
            enc = this.DefineEncoding(enc);
            int num2 = this.DefineShift(enc);
            this.sourceFileLinePositions.Add((int)this.fs.Position);
            base.lines.Add(null);
            while (this.fs.Position < length)
            {
                if (this.fs.ReadByte() == 10)
                {
                    this.sourceFileLinePositions.Add(((int)this.fs.Position) + num2);
                    base.lines.Add(null);
                }
            }
            Line[] collection = new Line[100];
            int count = base.lines.Count;
            base.lines.AddRange(collection);
            base.lines.TrimExcess();
            base.lines.RemoveRange(count, collection.Length);
            int[] numArray = new int[100];
            count = base.lines.Count;
            this.sourceFileLinePositions.AddRange(numArray);
            this.sourceFileLinePositions.TrimExcess();
            this.sourceFileLinePositions.RemoveRange(count, collection.Length);
            this.fileEncoding = enc;
            base.OnLineInserted(0, this.Count);
            int num5 = Math.Min(base.lines.Count, base.CurrentTB.Height / base.CurrentTB.CharHeight);
            for (int i = 0; i < num5; i++)
            {
                this.LoadLineFromSourceFile(i);
            }
            base.NeedRecalc(new TextSource.TextChangedEventArgs(0, 1));
        }

        public override void RemoveLine(int index, int count)
        {
            this.sourceFileLinePositions.RemoveRange(index, count);
            base.RemoveLine(index, count);
        }

        public override void SaveToFile(string fileName, Encoding enc)
        {
            int num;
            List<int> list = new List<int>(this.Count);
            string path = Path.Combine(Path.GetDirectoryName(fileName), Path.GetFileNameWithoutExtension(fileName) + ".tmp");
            StreamReader sr = new StreamReader(this.fs, this.fileEncoding);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream, enc))
                {
                    writer.Flush();
                    for (num = 0; num < this.Count; num++)
                    {
                        string text;
                        list.Add((int)stream.Length);
                        string sourceLineText = this.ReadLine(sr, num);
                        bool flag = (base.lines[num] != null) && base.lines[num].IsChanged;
                        if (flag)
                        {
                            text = base.lines[num].Text;
                        }
                        else
                        {
                            text = sourceLineText;
                        }
                        if (this.LinePushed != null)
                        {
                            LinePushedEventArgs e = new LinePushedEventArgs(sourceLineText, num, flag ? text : null);
                            this.LinePushed(this, e);
                            if (e.SavedText != null)
                            {
                                text = e.SavedText;
                            }
                        }
                        if (num == (this.Count - 1))
                        {
                            writer.Write(text);
                        }
                        else
                        {
                            writer.WriteLine(text);
                        }
                        writer.Flush();
                    }
                }
            }
            for (num = 0; num < this.Count; num++)
            {
                base.lines[num] = null;
            }
            sr.Dispose();
            this.fs.Dispose();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            File.Move(path, fileName);
            this.sourceFileLinePositions = list;
            this.fs = new FileStream(fileName, FileMode.Open);
            this.fileEncoding = enc;
        }

        internal void UnloadLine(int iLine)
        {
            if (!((base.lines[iLine] == null) || base.lines[iLine].IsChanged))
            {
                base.lines[iLine] = null;
            }
        }

        private Encoding DefineEncoding(Encoding enc)
        {
            int num = 0;
            byte[] buffer = new byte[4];
            int num2 = this.fs.Read(buffer, 0, 4);
            if ((((buffer[0] == 0xff) && (buffer[1] == 0xfe)) && ((buffer[2] == 0) && (buffer[3] == 0))) && (num2 >= 4))
            {
                enc = Encoding.UTF32;
                num = 4;
            }
            else if ((((buffer[0] == 0) && (buffer[1] == 0)) && (buffer[2] == 0xfe)) && (buffer[3] == 0xff))
            {
                enc = new UTF32Encoding(true, true);
                num = 4;
            }
            else if (((buffer[0] == 0xef) && (buffer[1] == 0xbb)) && (buffer[2] == 0xbf))
            {
                enc = Encoding.UTF8;
                num = 3;
            }
            else if ((buffer[0] == 0xfe) && (buffer[1] == 0xff))
            {
                enc = Encoding.BigEndianUnicode;
                num = 2;
            }
            else if ((buffer[0] == 0xff) && (buffer[1] == 0xfe))
            {
                enc = Encoding.Unicode;
                num = 2;
            }
            this.fs.Seek((long)num, SeekOrigin.Begin);
            return enc;
        }

        private int DefineShift(Encoding enc)
        {
            if (!enc.IsSingleByte)
            {
                if (enc.HeaderName == "unicodeFFFE")
                {
                    return 0;
                }
                if (enc.HeaderName == "utf-16")
                {
                    return 1;
                }
                if (enc.HeaderName == "utf-32BE")
                {
                    return 0;
                }
                if (enc.HeaderName == "utf-32")
                {
                    return 3;
                }
            }
            return 0;
        }

#pragma warning disable CS0114 // Member hides inherited member; missing override keyword

        private void LoadLineFromSourceFile(int i)
        {
            Line line = this.CreateLine();
            this.fs.Seek((long)this.sourceFileLinePositions[i], SeekOrigin.Begin);
            string sourceLineText = new StreamReader(this.fs, this.fileEncoding).ReadLine();
            if (sourceLineText == null)
            {
                sourceLineText = "";
            }
            if (this.LineNeeded != null)
            {
                LineNeededEventArgs e = new LineNeededEventArgs(sourceLineText, i);
                this.LineNeeded(this, e);
                sourceLineText = e.DisplayedLineText;
                if (sourceLineText == null)
                {
                    return;
                }
            }
            foreach (char ch in sourceLineText)
            {
                line.Add(new Char(ch));
            }
            base.lines[i] = line;
        }

        private string ReadLine(StreamReader sr, int i)
        {
            int num = this.sourceFileLinePositions[i];
            if (num < 0)
            {
                return "";
            }
            this.fs.Seek((long)num, SeekOrigin.Begin);
            sr.DiscardBufferedData();
            return sr.ReadLine();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            try
            {
                this.UnloadUnusedLines();
            }
            finally
            {
                this.timer.Enabled = true;
            }
        }

        private void UnloadUnusedLines()
        {
            int iLine = base.CurrentTB.VisibleRange.Start.iLine;
            int num2 = base.CurrentTB.VisibleRange.End.iLine;
            int num3 = 0;
            for (int i = 0; i < this.Count; i++)
            {
                if (((base.lines[i] != null) && !base.lines[i].IsChanged) && (Math.Abs((int)(i - num2)) > 0x7d0))
                {
                    base.lines[i] = null;
                    num3++;
                }
            }
        }
    }
}