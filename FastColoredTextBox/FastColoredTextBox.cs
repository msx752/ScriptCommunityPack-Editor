using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace FastColoredTextBoxNS
{
    public class FastColoredTextBox : UserControl
    {
        internal readonly List<LineInfo> lineInfos = new List<LineInfo>();
        private const int maxBracketSearchIterations = 0x3e8;
        private const int minLeftIndent = 8;
        private const int minLinesForAccuracy = 0x186a0;
        private const int SB_ENDSCROLL = 8;
        private const int WM_HSCROLL = 0x114;
        private const int WM_IME_SETCONTEXT = 0x281;
        private const int WM_VSCROLL = 0x115;
        private readonly Range selection;
        private readonly System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private readonly System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
        private readonly List<VisualMarker> visibleMarkers = new List<VisualMarker>();
        private bool caretVisible;
        private Color changedLineColor;
        private int charHeight;
        private Color currentLineColor;
        private Range delayedTextChangedRange;
        private string descriptionFile;
        private int endFoldingLine = -1;
        private FindForm findForm;
        private Color foldingIndicatorColor;
        private bool handledChar;
        private bool highlightFoldingIndicator;
        private Color indentBackColor;
        private bool isChanged;
        private Language language;
        private Keys lastModifiers;
        private Point lastMouseCoord;
        private DateTime lastNavigatedDateTime;
        private Range leftBracketPosition;
        private Range leftBracketPosition2;
        private int leftPadding;
        private int lineInterval;
        private Color lineNumberColor;
        private uint lineNumberStartValue;
        private TextSource lines;
        private IntPtr m_hImc;
        private int maxLineLength = 0;
        private bool mouseIsDrag;
        private bool multiline;
        private bool needRecalc;
        private bool needRiseSelectionChangedDelayed;
        private bool needRiseTextChangedDelayed;
        private bool needRiseVisibleRangeChangedDelayed;
        private Color paddingBackColor;
        private int preferredLineWidth;
        private ReplaceForm replaceForm;
        private Range rightBracketPosition;
        private Range rightBracketPosition2;
        private bool scrollBars;
        private Color selectionColor;
        private Color serviceLinesColor;
        private bool showLineNumbers;
        private FastColoredTextBox sourceTextBox;
        private int startFoldingLine = -1;
        private System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer();
        private int updating;
        private Range updatingRange;
        private bool wordWrap;
        private int wordWrapLinesCount;
        private WordWrapMode wordWrapMode = WordWrapMode.WordWrapControlWidth;
        public FastColoredTextBox()
        {
            try
            {
                TypeDescriptor.AddProvider(new FCTBDescriptionProvider(base.GetType()), this);
                base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                base.SetStyle(ControlStyles.UserPaint, true);
                base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
                base.SetStyle(ControlStyles.ResizeRedraw, true);
                this.Font = new System.Drawing.Font("Consolas", 9.75f, FontStyle.Regular, GraphicsUnit.Point);
                this.InitTextSource(this.CreateTextSource());
                if (this.lines.Count == 0)
                {
                    this.lines.InsertLine(0, this.lines.CreateLine());
                }
                Range range = new Range(this)
                {
                    Start = new Place(0, 0)
                };
                this.selection = range;
                this.Cursor = Cursors.IBeam;
                this.BackColor = Color.Snow;
                this.LineNumberColor = Color.SteelBlue;
                this.IndentBackColor = Color.White;
                this.ServiceLinesColor = Color.LightSteelBlue;
                this.FoldingIndicatorColor = Color.SteelBlue;
                this.CurrentLineColor = Color.Transparent;
                this.ChangedLineColor = Color.Transparent;
                this.HighlightFoldingIndicator = true;
                this.ShowLineNumbers = true;
                this.TabLength = 4;
                this.FoldedBlockStyle = new FoldedBlockStyle(Brushes.Gray, null, FontStyle.Regular);
                this.SelectionColor = Color.GreenYellow;
                this.BracketsStyle = new MarkerStyle(new SolidBrush(Color.FromArgb(80, Color.Lime)));
                this.BracketsStyle2 = new MarkerStyle(new SolidBrush(Color.FromArgb(60, Color.Red)));
                this.DelayedEventsInterval = 100;
                this.DelayedTextChangedInterval = 100;
                this.AllowSeveralTextStyleDrawing = false;
                this.LeftBracket = '\0';
                this.RightBracket = '\0';
                this.LeftBracket2 = '\0';
                this.RightBracket2 = '\0';
                this.SyntaxHighlighter = new SyntaxHighlighter();
                this.language = Language.Custom;
                this.PreferredLineWidth = 0;
                this.needRecalc = true;
                this.lastNavigatedDateTime = DateTime.Now;
                this.AutoIndent = true;
                this.CommentPrefix = "//";
                this.lineNumberStartValue = 1;
                this.multiline = true;
                this.scrollBars = true;
                this.AcceptsTab = true;
                this.AcceptsReturn = true;
                this.caretVisible = true;
                this.CaretColor = Color.Black;
                this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 0);
                this.PaddingBackColor = Color.White;
                base.AutoScroll = true;
                this.timer.Tick += new EventHandler(this.timer_Tick);
                this.timer2.Tick += new EventHandler(this.timer2_Tick);

                this.ToolTip = new ToolTip();
                this.timer3.Interval = 500;
                this.timer3.Tick += timer3_Tick;
                this.MouseMove += MYTextBox_MouseMove;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        [Description("It occurs when calculates AutoIndent for new line."), Browsable(true)]
        public event EventHandler<AutoIndentEventArgs> AutoIndentNeeded;

        [Description("Occurs when current highlighted folding area is changed."), Browsable(true)]
        public event EventHandler<EventArgs> FoldingHighlightChanged;

        [Description("It occurs when visible char is enetered (alphabetic, digit, punctuation, DEL, BACKSPACE)."), Browsable(true)]
        public event KeyPressEventHandler KeyPressed;

        [Description("It occurs when visible char is enetering (alphabetic, digit, punctuation, DEL, BACKSPACE)."), Browsable(true)]
        public event KeyPressEventHandler KeyPressing;

        [Browsable(true), Description("Occurs when line was inserted/added.")]
        public event EventHandler<LineInsertedEventArgs> LineInserted;

        [Browsable(true), Description("Occurs when line was removed.")]
        public event EventHandler<LineRemovedEventArgs> LineRemoved;

        [Browsable(true), Description("It occurs when line background is painting.")]
        public event EventHandler<PaintLineEventArgs> PaintLine;

        [Description("It occurs after changing of selection."), Browsable(true)]
        public event EventHandler SelectionChanged;

        [Browsable(true), Description("It occurs after changing of selection. This event occurs with a delay relative to SelectionChanged, and fires only once.")]
        public event EventHandler SelectionChangedDelayed;

        [Browsable(true), Description("It occurs after insert, delete, clear, undo and redo operations.")]
        public event EventHandler<TextChangedEventArgs> TextChanged;

        [Description("It occurs after insert, delete, clear, undo and redo operations. This event occurs with a delay relative to TextChanged, and fires only once."), Browsable(true)]
        public event EventHandler<TextChangedEventArgs> TextChangedDelayed;

        [Description("It occurs before insert, delete, clear, undo and redo operations."), Browsable(true)]
        public event EventHandler<TextChangingEventArgs> TextChanging;

        /// <summary>
        /// Occurs when mouse is moving over text and tooltip is needed
        /// </summary>
        [Browsable(true)]
        [Description("Occurs when mouse is moving over text and tooltip is needed.")]
        public event EventHandler<ToolTipNeededEventArgs> ToolTipNeeded;

        [Browsable(true), Description("Occurs when undo/redo stack is changed.")]
        public event EventHandler<EventArgs> UndoRedoStateChanged;

        [Browsable(true), Description("It occurs after changing of visible range.")]
        public event EventHandler VisibleRangeChanged;

        [Description("It occurs after changing of visible range. This event occurs with a delay relative to VisibleRangeChanged, and fires only once."), Browsable(true)]
        public event EventHandler VisibleRangeChangedDelayed;

        [Browsable(true), Description("It occurs when user click on VisualMarker.")]
        public event EventHandler<VisualMarkerEventArgs> VisualMarkerClick;

        [Browsable(false)]
        internal event EventHandler BindingTextChanged;
#pragma warning restore CS0108 // Member hides inherited member; missing new keyword
        [Description("Indicates if return characters are accepted as input."), DefaultValue(true)]
        public bool AcceptsReturn { get; set; }

        [DefaultValue(true), Description("Indicates if tab characters are accepted as input.")]
        public bool AcceptsTab { get; set; }

        [Browsable(true), Description("Allows text rendering several styles same time."), DefaultValue(false)]
        public bool AllowSeveralTextStyleDrawing { get; set; }

        [DefaultValue(true), Description("Allows auto indent. Inserts spaces before line chars.")]
        public bool AutoIndent { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoScroll
        {
            get
            {
                return base.AutoScroll;
            }
            set
            {
            }
        }

        [DefaultValue(typeof(Color), "White"), Description("Background color.")]
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }

        [Browsable(false)]
        public MarkerStyle BracketsStyle { get; set; }

        [Browsable(false)]
        public MarkerStyle BracketsStyle2 { get; set; }

        [Description("Color of caret."), DefaultValue(typeof(Color), "Black")]
        public Color CaretColor { get; set; }

        [DefaultValue(true), Description("Shows or hides the caret")]
        public bool CaretVisible
        {
            get
            {
                return this.caretVisible;
            }
            set
            {
                this.caretVisible = value;
                this.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "Transparent"), Description("Background color for highlighting of changed lines. Set to Color.Transparent to hide changed line highlighting")]
        public Color ChangedLineColor
        {
            get
            {
                return this.changedLineColor;
            }
            set
            {
                this.changedLineColor = value;
                this.Invalidate();
            }
        }

        [Description("Height of char in pixels")]
        public int CharHeight
        {
            get
            {
                return this.charHeight;
            }
            private set
            {
                this.charHeight = value;
                this.OnCharSizeChanged();
            }
        }

        [Description("Width of char in pixels")]
        public int CharWidth { get; private set; }

        [Description("Comment line prefix."), DefaultValue("//")]
        public string CommentPrefix { get; set; }

        [Description("Background color for current line. Set to Color.Transparent to hide current line highlighting"), DefaultValue(typeof(Color), "Transparent")]
        public Color CurrentLineColor
        {
            get
            {
                return this.currentLineColor;
            }
            set
            {
                this.currentLineColor = value;
                this.Invalidate();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public TextStyle DefaultStyle
        {
            get
            {
                return this.lines.DefaultStyle;
            }
            set
            {
                this.lines.DefaultStyle = value;
            }
        }

        [Description("Minimal delay(ms) for delayed events (except TextChangedDelayed)."), Browsable(true), DefaultValue(100)]
        public int DelayedEventsInterval
        {
            get
            {
                return this.timer.Interval;
            }
            set
            {
                this.timer.Interval = value;
            }
        }

        [Browsable(true), Description("Minimal delay(ms) for TextChangedDelayed event."), DefaultValue(100)]
        public int DelayedTextChangedInterval
        {
            get
            {
                return this.timer2.Interval;
            }
            set
            {
                this.timer2.Interval = value;
            }
        }

        [Browsable(true)]
        [DefaultValue(null)]
        //[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
        [Description(
            "XML file with description of syntax highlighting. This property works only with Language == Language.Custom."
            )]
        public string DescriptionFile
        {
            get
            {
                return this.descriptionFile;
            }
            set
            {
                this.descriptionFile = value;
                this.Invalidate();
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int EndFoldingLine
        {
            get
            {
                return this.endFoldingLine;
            }
        }

        [Browsable(false)]
        public TextStyle FoldedBlockStyle { get; set; }

        [DefaultValue(typeof(Color), "Green"), Description("Color of folding area indicator.")]
        public Color FoldingIndicatorColor
        {
            get
            {
                return this.foldingIndicatorColor;
            }
            set
            {
                this.foldingIndicatorColor = value;
                this.Invalidate();
            }
        }

        [DefaultValue(typeof(System.Drawing.Font), "Consolas, 9.75")]
        public override System.Drawing.Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                SizeF charSize = GetCharSize(base.Font, 'M');
                SizeF ef2 = GetCharSize(base.Font, '.');
                if (charSize != ef2)
                {
                    base.Font = new System.Drawing.Font("Courier New", base.Font.SizeInPoints, FontStyle.Regular, GraphicsUnit.Point);
                }
                SizeF ef3 = GetCharSize(base.Font, 'M');
                this.CharWidth = ((int)Math.Round((double)(ef3.Width * 1f))) - 1;
                this.CharHeight = (this.lineInterval + ((int)Math.Round((double)(ef3.Height * 1f)))) - 1;
                this.NeedRecalc();
                this.Invalidate();
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                this.lines.InitDefaultStyle();
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool HasSourceTextBox
        {
            get
            {
                return (this.SourceTextBox != null);
            }
        }

        [DefaultValue(true), Description("Enables folding indicator (left vertical line between folding bounds)")]
        public bool HighlightFoldingIndicator
        {
            get
            {
                return this.highlightFoldingIndicator;
            }
            set
            {
                this.highlightFoldingIndicator = value;
                this.Invalidate();
            }
        }

        [DefaultValue(typeof(HighlightingRangeType), "ChangedRange"), Description("This property specifies which part of the text will be highlighted as you type.")]
        public HighlightingRangeType HighlightingRangeType { get; set; }

        [Browsable(false)]
        public string Html
        {
            get
            {
                ExportToHTML ohtml = new ExportToHTML
                {
                    UseNbsp = false,
                    UseStyleTag = false,
                    UseBr = false
                };
                return ("<pre>" + ohtml.GetHtml(this) + "</pre>");
            }
        }

        [Browsable(false)]
        public bool ImeAllowed
        {
            get
            {
                return (((base.ImeMode != ImeMode.Disable) && (base.ImeMode != ImeMode.Off)) && (base.ImeMode != ImeMode.NoControl));
            }
        }

        [DefaultValue(typeof(Color), "White"), Description("Background color of indent area")]
        public Color IndentBackColor
        {
            get
            {
                return this.indentBackColor;
            }
            set
            {
                this.indentBackColor = value;
                this.Invalidate();
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public bool IsChanged
        {
            get
            {
                return this.isChanged;
            }
            set
            {
                if (!value)
                {
                    this.lines.ClearIsChanged();
                }
                this.isChanged = value;
            }
        }

        [Browsable(false)]
        public bool IsReplaceMode
        {
            get
            {
                return ((Control.IsKeyLocked(Keys.Insert) && (this.Selection.Start == this.Selection.End)) && (this.Selection.Start.iChar < this.lines[this.Selection.Start.iLine].Count));
            }
        }

        [Browsable(true), DefaultValue(typeof(Language), "Custom"), Description("Language for highlighting by built-in highlighter.")]
        public Language Language
        {
            get
            {
                return this.language;
            }
            set
            {
                this.language = value;
                this.Invalidate();
            }
        }

        [Description(@"Opening bracket for brackets highlighting. Set to '\x0' for disable brackets highlighting."), DefaultValue('\0')]
        public char LeftBracket { get; set; }

        [DefaultValue('\0'), Description(@"Alternative opening bracket for brackets highlighting. Set to '\x0' for disable brackets highlighting.")]
        public char LeftBracket2 { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Range LeftBracketPosition
        {
            get
            {
                return this.leftBracketPosition;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public Range LeftBracketPosition2
        {
            get
            {
                return this.leftBracketPosition2;
            }
        }

        [Description("Left indent in pixels"), Browsable(false)]
        public int LeftIndent { get; private set; }

        [Description("Width of left service area (in pixels)"), DefaultValue(0)]
        public int LeftPadding
        {
            get
            {
                return this.leftPadding;
            }
            set
            {
                this.leftPadding = value;
                this.Invalidate();
            }
        }

        [Description("Interval between lines in pixels"), DefaultValue(0)]
        public int LineInterval
        {
            get
            {
                return this.lineInterval;
            }
            set
            {
                this.lineInterval = value;
                this.Font = this.Font;
                this.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "SteelBlue"), Description("Color of line numbers.")]
        public Color LineNumberColor
        {
            get
            {
                return this.lineNumberColor;
            }
            set
            {
                this.lineNumberColor = value;
                this.Invalidate();
            }
        }

        [DefaultValue(typeof(uint), "1"), Description("Start value of first line number.")]
        public uint LineNumberStartValue
        {
            get
            {
                return this.lineNumberStartValue;
            }
            set
            {
                this.lineNumberStartValue = value;
                this.needRecalc = true;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public IList<string> Lines
        {
            get
            {
                return this.lines.Lines;
            }
        }

        [Browsable(false)]
        public int LinesCount
        {
            get
            {
                return this.lines.Count;
            }
        }

        [Description("Multiline mode."), DefaultValue(true), Browsable(true)]
        public bool Multiline
        {
            get
            {
                return this.multiline;
            }
            set
            {
                if (this.multiline != value)
                {
                    this.multiline = value;
                    this.needRecalc = true;
                    if (this.multiline)
                    {
                        base.AutoScroll = true;
                        this.ShowScrollBars = true;
                    }
                    else
                    {
                        base.AutoScroll = false;
                        this.ShowScrollBars = false;
                        if (this.lines.Count > 1)
                        {
                            this.lines.RemoveLine(1, this.lines.Count - 1);
                        }
                        this.lines.Manager.ClearHistory();
                    }
                    this.Invalidate();
                }
            }
        }

        [Browsable(true), Description("Paddings of text area.")]
        public new System.Windows.Forms.Padding Padding { get; set; }

        [Description("Background color of padding area"), DefaultValue(typeof(Color), "White")]
        public Color PaddingBackColor
        {
            get
            {
                return this.paddingBackColor;
            }
            set
            {
                this.paddingBackColor = value;
                this.Invalidate();
            }
        }

        [Description("This property draws vertical line after defined char position. Set to 0 for disable drawing of vertical line."), DefaultValue(0)]
        public int PreferredLineWidth
        {
            get
            {
                return this.preferredLineWidth;
            }
            set
            {
                this.preferredLineWidth = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public Range Range
        {
            get
            {
                return new Range(this, new Place(0, 0), new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1));
            }
        }

        [DefaultValue(false)]
        public bool ReadOnly { get; set; }

        [Browsable(false)]
        public bool RedoEnabled
        {
            get
            {
                return this.lines.Manager.RedoEnabled;
            }
        }

        [DefaultValue('\0'), Description(@"Closing bracket for brackets highlighting. Set to '\x0' for disable brackets highlighting.")]
        public char RightBracket { get; set; }

        [DefaultValue('\0'), Description(@"Alternative closing bracket for brackets highlighting. Set to '\x0' for disable brackets highlighting.")]
        public char RightBracket2 { get; set; }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Range RightBracketPosition
        {
            get
            {
                return this.rightBracketPosition;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Range RightBracketPosition2
        {
            get
            {
                return this.rightBracketPosition2;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string SelectedText
        {
            get
            {
                return this.Selection.Text;
            }
            set
            {
                this.InsertText(value);
            }
        }

        [Browsable(false)]
        public Range Selection
        {
            get
            {
                return this.selection;
            }
            set
            {
                this.selection.BeginUpdate();
                this.selection.Start = value.Start;
                this.selection.End = value.End;
                this.selection.EndUpdate();
                this.Invalidate();
            }
        }

        [DefaultValue(typeof(Color), "Blue"), Description("Color of selected area.")]
        public virtual Color SelectionColor
        {
            get
            {
                return this.selectionColor;
            }
            set
            {
                this.selectionColor = value;
                if (this.selectionColor.A == 0xff)
                {
                    this.selectionColor = Color.FromArgb(25, Color.Red);
                }
                this.SelectionStyle = new SelectionStyle(new SolidBrush(this.selectionColor));
                this.Invalidate();
            }
        }

        [Browsable(false), DefaultValue(0)]
        public int SelectionLength
        {
            get
            {
                return Math.Abs((int)(this.PlaceToPosition(this.Selection.Start) - this.PlaceToPosition(this.Selection.End)));
            }
            set
            {
                if (value > 0)
                {
                    this.Selection.End = this.PositionToPlace(this.SelectionStart + value);
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int SelectionStart
        {
            get
            {
                return Math.Min(this.PlaceToPosition(this.Selection.Start), this.PlaceToPosition(this.Selection.End));
            }
            set
            {
                this.Selection.Start = this.PositionToPlace(value);
            }
        }

        [Browsable(false)]
        public SelectionStyle SelectionStyle { get; set; }

        [DefaultValue(typeof(Color), "Silver"), Description("Color of service lines (folding lines, borders of blocks etc.)")]
        public Color ServiceLinesColor
        {
            get
            {
                return this.serviceLinesColor;
            }
            set
            {
                this.serviceLinesColor = value;
                this.Invalidate();
            }
        }

        [Description("Shows line numbers."), DefaultValue(true)]
        public bool ShowLineNumbers
        {
            get
            {
                return this.showLineNumbers;
            }
            set
            {
                this.showLineNumbers = value;
                this.Invalidate();
            }
        }

        [Description("Scollbars visibility."), Browsable(true), DefaultValue(true)]
        public bool ShowScrollBars
        {
            get
            {
                return this.scrollBars;
            }
            set
            {
                if (value != this.scrollBars)
                {
                    this.scrollBars = value;
                    this.needRecalc = true;
                    this.Invalidate();
                }
            }
        }

        [Browsable(true), DefaultValue((string)null), Description("Allows to get text from other FastColoredTextBox.")]
        public FastColoredTextBox SourceTextBox
        {
            get
            {
                return this.sourceTextBox;
            }
            set
            {
                if (value != this.sourceTextBox)
                {
                    this.sourceTextBox = value;
                    if (this.sourceTextBox == null)
                    {
                        this.InitTextSource(this.CreateTextSource());
                        this.lines.InsertLine(0, this.TextSource.CreateLine());
                        this.IsChanged = false;
                    }
                    else
                    {
                        this.InitTextSource(this.SourceTextBox.TextSource);
                        this.isChanged = false;
                    }
                    this.Invalidate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public int StartFoldingLine
        {
            get
            {
                return this.startFoldingLine;
            }
        }

        [Browsable(false)]
        public Style[] Styles
        {
            get
            {
                return this.lines.Styles;
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public SyntaxHighlighter SyntaxHighlighter { get; set; }

        [DefaultValue(4), Description("Spaces count for tab")]
        public int TabLength { get; set; }

        [Bindable(true), SettingsBindable(true), Browsable(true), Description("Text of the control."), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override string Text
        {
            get
            {
                Range range = new Range(this);
                range.SelectAll();
                return range.Text;
            }
            set
            {
                this.SetAsCurrentTB();
                this.Selection.BeginUpdate();
                try
                {
                    this.Selection.SelectAll();
                    this.InsertText(value);
                    this.GoHome();
                }
                finally
                {
                    this.Selection.EndUpdate();
                }
            }
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public TextSource TextSource
        {
            get
            {
                return this.lines;
            }
            set
            {
                this.InitTextSource(value);
            }
        }

        [Browsable(false)]
        public int TextVersion { get; private set; }

        /// <summary>
        /// ToolTip component
        /// </summary>
        [Browsable(true)]
        [Description("ToolTip component.")]
        public ToolTip ToolTip { get; set; }

        /// <summary>
        /// Delay (ms) of ToolTip
        /// </summary>
        [Browsable(true)]
        [DefaultValue(500)]
        [Description("Delay(ms) of ToolTip.")]
        public int ToolTipDelay
        {
            get { return timer3.Interval; }
            set { timer3.Interval = value; }
        }

        [Browsable(false)]
        public bool UndoEnabled
        {
            get
            {
                return this.lines.Manager.UndoEnabled;
            }
        }

        [Browsable(false)]
        public Range VisibleRange
        {
            get
            {
                return this.GetRange(this.PointToPlace(new Point(this.LeftIndent, 0)), this.PointToPlace(new Point(base.ClientSize.Width, base.ClientSize.Height)));
            }
        }

        [Description("WordWrap."), Browsable(true), DefaultValue(false)]
        public bool WordWrap
        {
            get
            {
                return this.wordWrap;
            }
            set
            {
                if (this.wordWrap != value)
                {
                    this.wordWrap = value;
                    this.RecalcWordWrap(0, this.LinesCount - 1);
                    this.Invalidate();
                }
            }
        }

        [Browsable(false)]
        public int WordWrapLinesCount
        {
            get
            {
                if (this.needRecalc)
                {
                    this.Recalc();
                }
                return this.wordWrapLinesCount;
            }
        }

        [Browsable(true), Description("WordWrap mode."), DefaultValue(typeof(WordWrapMode), "WordWrapControlWidth")]
        public WordWrapMode WordWrapMode
        {
            get
            {
                return this.wordWrapMode;
            }
            set
            {
                if (this.wordWrapMode != value)
                {
                    this.wordWrapMode = value;
                    this.RecalcWordWrap(0, this.LinesCount - 1);
                    this.Invalidate();
                }
            }
        }

        private new Size AutoScrollMinSize
        {
            get
            {
                if (this.scrollBars)
                {
                    return base.AutoScrollMinSize;
                }
                return new Size(base.HorizontalScroll.Maximum, base.VerticalScroll.Maximum);
            }
            set
            {
                if (this.scrollBars)
                {
                    base.AutoScroll = true;
                    Size size = value;
                    if (this.WordWrap)
                    {
                        int maxLineWordWrapedWidth = this.GetMaxLineWordWrapedWidth();
                        int width = Math.Min(size.Width, maxLineWordWrapedWidth);
                        size = new Size(width, size.Height);
                    }
                    base.AutoScrollMinSize = size;
                }
                else
                {
                    base.AutoScroll = false;
                    base.AutoScrollMinSize = new Size(0, 0);
                    base.VerticalScroll.Visible = false;
                    base.HorizontalScroll.Visible = false;
                    base.HorizontalScroll.Maximum = value.Width;
                    base.VerticalScroll.Maximum = value.Height;
                }
            }
        }

        private int LeftIndentLine
        {
            get
            {
                return ((this.LeftIndent - 4) - 3);
            }
        }

        public Char this[Place place]
        {
            get
            {
                return this.lines[place.iLine][place.iChar];
            }
            set
            {
                this.lines[place.iLine][place.iChar] = value;
            }
        }

        public Line this[int iLine]
        {
            get
            {
                return this.lines[iLine];
            }
        }

        public static SizeF GetCharSize(System.Drawing.Font font, char c)
        {
            Size size = TextRenderer.MeasureText("<" + c.ToString() + ">", font);
            Size size2 = TextRenderer.MeasureText("<>", font);
            return new SizeF((float)((size.Width - size2.Width) + 1), (float)font.Height);
        }

        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);

        public static MemoryStream PrepareHtmlForClipboard(string html)
        {
            Encoding encoding = Encoding.UTF8;
            string format = "Version:0.9\r\nStartHTML:{0:000000}\r\nEndHTML:{1:000000}\r\nStartFragment:{2:000000}\r\nEndFragment:{3:000000}\r\n";
            string s = "<html>\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=" + encoding.WebName + "\">\r\n<title>HTML clipboard</title>\r\n</head>\r\n<body>\r\n<!--StartFragment-->";
            string str3 = "<!--EndFragment-->\r\n</body>\r\n</html>\r\n";
            string str4 = string.Format(format, new object[] { 0, 0, 0, 0 });
            int byteCount = encoding.GetByteCount(str4);
            int num2 = encoding.GetByteCount(s);
            int num3 = encoding.GetByteCount(html);
            int num4 = encoding.GetByteCount(str3);
            string str5 = string.Format(format, new object[] { byteCount, ((byteCount + num2) + num3) + num4, byteCount + num2, (byteCount + num2) + num3 }) + s + html + str3;
            return new MemoryStream(encoding.GetBytes(str5));
        }

        public int AddStyle(Style style)
        {
            if (style == null)
            {
                return -1;
            }
            int styleIndex = this.GetStyleIndex(style);
            if (styleIndex < 0)
            {
                styleIndex = this.Styles.Length - 1;
                while (styleIndex >= 0)
                {
                    if (this.Styles[styleIndex] != null)
                    {
                        break;
                    }
                    styleIndex--;
                }
                styleIndex++;
                if (styleIndex >= this.Styles.Length)
                {
                    throw new Exception("Maximum count of Styles is exceeded");
                }
                this.Styles[styleIndex] = style;
            }
            return styleIndex;
        }

        public void AppendText(string text)
        {
            if (text != null)
            {
                Place start = this.Selection.Start;
                Place end = this.Selection.End;
                this.Selection.BeginUpdate();
                this.lines.Manager.BeginAutoUndoCommands();
                try
                {
                    if (this.lines.Count > 0)
                    {
                        this.Selection.Start = new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1);
                    }
                    else
                    {
                        this.Selection.Start = new Place(0, 0);
                    }
                    this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, text));
                }
                finally
                {
                    this.lines.Manager.EndAutoUndoCommands();
                    this.Selection.Start = start;
                    this.Selection.End = end;
                    this.Selection.EndUpdate();
                }
                this.Invalidate();
            }
        }

        public void BeginAutoUndo()
        {
            this.lines.Manager.BeginAutoUndoCommands();
        }

        public void BeginUpdate()
        {
            if (this.updating == 0)
            {
                this.updatingRange = null;
            }
            this.updating++;
        }

        public virtual int CalcAutoIndent(int iLine)
        {
            if ((iLine < 0) || (iLine >= this.LinesCount))
            {
                return 0;
            }
            EventHandler<AutoIndentEventArgs> autoIndentNeeded = this.AutoIndentNeeded;
            if (autoIndentNeeded == null)
            {
                if ((this.Language != Language.Custom) && (this.SyntaxHighlighter != null))
                {
                    SyntaxHighlighter syntaxHighlighter = this.SyntaxHighlighter;
                    autoIndentNeeded = new EventHandler<AutoIndentEventArgs>(syntaxHighlighter.AutoIndentNeeded);
                }
                else
                {
                    autoIndentNeeded = new EventHandler<AutoIndentEventArgs>(this.CalcAutoIndentShiftByCodeFolding);
                }
            }
            Stack<AutoIndentEventArgs> stack = new Stack<AutoIndentEventArgs>();
            int num2 = iLine - 1;
            while (num2 >= 0)
            {
                AutoIndentEventArgs args = new AutoIndentEventArgs(num2, this.lines[num2].Text, (num2 > 0) ? this.lines[num2 - 1].Text : "", this.TabLength);
                autoIndentNeeded(this, args);
                stack.Push(args);
                if ((args.Shift == 0) && (args.LineText.Trim() != ""))
                {
                    break;
                }
                num2--;
            }
            int startSpacesCount = this.lines[(num2 >= 0) ? num2 : 0].StartSpacesCount;
            while (stack.Count != 0)
            {
                startSpacesCount += stack.Pop().ShiftNextLines;
            }
            AutoIndentEventArgs e = new AutoIndentEventArgs(iLine, this.lines[iLine].Text, (iLine > 0) ? this.lines[iLine - 1].Text : "", this.TabLength);
            autoIndentNeeded(this, e);
            return (startSpacesCount + e.Shift);
        }

        public void Clear()
        {
            this.Selection.BeginUpdate();
            try
            {
                this.Selection.SelectAll();
                this.ClearSelected();
                this.lines.Manager.ClearHistory();
                this.Invalidate();
            }
            finally
            {
                this.Selection.EndUpdate();
            }
        }

        public void ClearCurrentLine()
        {
            this.Selection.Expand();
            this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
            if ((this.Selection.Start.iLine != 0) || this.Selection.GoRightThroughFolded())
            {
                if (this.Selection.Start.iLine > 0)
                {
                    this.lines.Manager.ExecuteCommand(new InsertCharCommand(this.TextSource, '\b'));
                }
                this.Invalidate();
            }
        }

        public void ClearSelected()
        {
            if (this.Selection.Start != this.Selection.End)
            {
                this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
                this.Invalidate();
            }
        }

        public void ClearStyle(StyleIndex styleIndex)
        {
            foreach (Line line in this.lines)
            {
                line.ClearStyle(styleIndex);
            }
            for (int i = 0; i < this.lineInfos.Count; i++)
            {
                this.SetVisibleState(i, VisibleState.Visible);
            }
            this.Invalidate();
        }

        public void ClearStylesBuffer()
        {
            for (int i = 0; i < this.Styles.Length; i++)
            {
                this.Styles[i] = null;
            }
        }

        public void ClearUndo()
        {
            this.lines.Manager.ClearHistory();
        }

        public void CloseBindingFile()
        {
            if (this.lines is FileTextSource)
            {
                (this.lines as FileTextSource).CloseFile();
                this.InitTextSource(this.CreateTextSource());
                this.lines.InsertLine(0, this.TextSource.CreateLine());
                this.IsChanged = false;
                this.Invalidate();
            }
        }

        public void CollapseAllFoldingBlocks()
        {
            for (int i = 0; i < this.LinesCount; i++)
            {
                if (this.lines.LineHasFoldingStartMarker(i))
                {
                    int toLine = this.FindEndOfFoldingBlock(i);
                    if (toLine >= 0)
                    {
                        this.CollapseBlock(i, toLine);
                        i = toLine;
                    }
                }
            }
            this.OnVisibleRangeChanged();
        }

        public void CollapseBlock(int fromLine, int toLine)
        {
            int iLine = Math.Min(fromLine, toLine);
            int num2 = Math.Max(fromLine, toLine);
            if (iLine != num2)
            {
                while (iLine <= num2)
                {
                    if (this.GetLineText(iLine).Trim().Length > 0)
                    {
                        for (int i = iLine + 1; i <= num2; i++)
                        {
                            this.SetVisibleState(i, VisibleState.Hidden);
                        }
                        this.SetVisibleState(iLine, VisibleState.StartOfHiddenBlock);
                        this.Invalidate();
                        break;
                    }
                    iLine++;
                }
                iLine = Math.Min(fromLine, toLine);
                num2 = Math.Max(fromLine, toLine);
                int num4 = this.FindNextVisibleLine(num2);
                if (num4 == num2)
                {
                    num4 = this.FindPrevVisibleLine(iLine);
                }
                this.Selection.Start = new Place(0, num4);
                this.needRecalc = true;
                this.Invalidate();
            }
        }

        public void CollapseFoldingBlock(int iLine)
        {
            if ((iLine < 0) || (iLine >= this.lines.Count))
            {
                throw new ArgumentOutOfRangeException("Line index out of range");
            }
            if (string.IsNullOrEmpty(this.lines[iLine].FoldingStartMarker))
            {
                throw new ArgumentOutOfRangeException("This line is not folding start line");
            }
            int toLine = this.FindEndOfFoldingBlock(iLine);
            if (toLine >= 0)
            {
                this.CollapseBlock(iLine, toLine);
            }
        }

        public void CommentSelected()
        {
            this.CommentSelected(this.CommentPrefix);
        }

        public void CommentSelected(string commentPrefix)
        {
            if (!string.IsNullOrEmpty(commentPrefix))
            {
                this.Selection.Normalize();
                if (this.lines[this.Selection.Start.iLine].Text.TrimStart(new char[0]).StartsWith(commentPrefix))
                {
                    this.RemoveLinePrefix(commentPrefix);
                }
                else
                {
                    this.InsertLinePrefix(commentPrefix);
                }
            }
        }

        public void Copy()
        {
            if (this.Selection.End != this.Selection.Start)
            {
                ExportToHTML ohtml = new ExportToHTML
                {
                    UseBr = false,
                    UseNbsp = false,
                    UseStyleTag = true
                };
                string html = "<pre>" + ohtml.GetHtml(this.Selection) + "</pre>";
                DataObject data = new DataObject();
                data.SetData(DataFormats.UnicodeText, true, this.Selection.Text);
                data.SetData(DataFormats.Html, PrepareHtmlForClipboard(html));
                Thread thread = new Thread(() => Clipboard.SetDataObject(data, true));
                thread.SetApartmentState(ApartmentState.STA);
                thread.Start();
                thread.Join();
            }
        }

        public void Cut()
        {
            if (this.Selection.End != this.Selection.Start)
            {
                this.Copy();
                this.ClearSelected();
            }
        }

        public void DecreaseIndent()
        {
            if (this.Selection.Start != this.Selection.End)
            {
                Range range = this.Selection.Clone();
                int iLine = Math.Min(this.Selection.Start.iLine, this.Selection.End.iLine);
                int num2 = Math.Max(this.Selection.Start.iLine, this.Selection.End.iLine);
                this.BeginUpdate();
                this.Selection.BeginUpdate();
                this.lines.Manager.BeginAutoUndoCommands();
                for (int i = iLine; i <= num2; i++)
                {
                    this.Selection.Start = new Place(0, i);
                    this.Selection.End = new Place(Math.Min(this.lines[i].Count, this.TabLength), i);
                    if (this.Selection.Text.Trim() == "")
                    {
                        this.ClearSelected();
                    }
                }
                this.lines.Manager.EndAutoUndoCommands();
                this.Selection.Start = new Place(0, iLine);
                this.Selection.End = new Place(this.lines[num2].Count, num2);
                this.needRecalc = true;
                this.EndUpdate();
                this.Selection.EndUpdate();
            }
        }

        public void DoAutoIndent()
        {
            Range range = this.Selection.Clone();
            range.Normalize();
            this.BeginUpdate();
            this.Selection.BeginUpdate();
            this.lines.Manager.BeginAutoUndoCommands();
            for (int i = range.Start.iLine; i <= range.End.iLine; i++)
            {
                this.DoAutoIndent(i);
            }
            this.lines.Manager.EndAutoUndoCommands();
            this.Selection.Start = range.Start;
            this.Selection.End = range.End;
            this.Selection.Expand();
            this.Selection.EndUpdate();
            this.EndUpdate();
        }

        public virtual void DoAutoIndent(int iLine)
        {
            Place start = this.Selection.Start;
            int num = this.CalcAutoIndent(iLine);
            int startSpacesCount = this.lines[iLine].StartSpacesCount;
            int count = num - startSpacesCount;
            if (count < 0)
            {
                count = -Math.Min(-count, startSpacesCount);
            }
            if (count != 0)
            {
                this.Selection.Start = new Place(0, iLine);
                if (count > 0)
                {
                    this.InsertText(new string(' ', count));
                }
                else
                {
                    this.Selection.Start = new Place(0, iLine);
                    this.Selection.End = new Place(-count, iLine);
                    this.ClearSelected();
                }
                this.Selection.Start = new Place(Math.Min(this.lines[iLine].Count, Math.Max(0, start.iChar + count)), iLine);
            }
        }

        public void DoCaretVisible()
        {
            this.Invalidate();
            this.Recalc();
            Point location = this.PlaceToPoint(this.Selection.Start);
            location.Offset(-this.CharWidth, 0);
            this.DoVisibleRectangle(new Rectangle(location, new Size(2 * this.CharWidth, 2 * this.CharHeight)));
        }

        public void DoSelectionVisible()
        {
            if (this.lineInfos[this.Selection.End.iLine].VisibleState != VisibleState.Visible)
            {
                this.ExpandBlock(this.Selection.End.iLine);
            }
            if (this.lineInfos[this.Selection.Start.iLine].VisibleState != VisibleState.Visible)
            {
                this.ExpandBlock(this.Selection.Start.iLine);
            }
            this.Recalc();
            this.DoVisibleRectangle(new Rectangle(this.PlaceToPoint(new Place(0, this.Selection.End.iLine)), new Size(2 * this.CharWidth, 2 * this.CharHeight)));
            Point location = this.PlaceToPoint(this.Selection.Start);
            Point point2 = this.PlaceToPoint(this.Selection.End);
            location.Offset(-this.CharWidth, -base.ClientSize.Height / 2);
            this.DoVisibleRectangle(new Rectangle(location, new Size(Math.Abs((int)(point2.X - location.X)), base.ClientSize.Height)));
            this.Invalidate();
        }

        public void EndAutoUndo()
        {
            this.lines.Manager.EndAutoUndoCommands();
        }

        public void EndUpdate()
        {
            this.updating--;
            if ((this.updating == 0) && (this.updatingRange != null))
            {
                this.updatingRange.Expand();
                this.OnTextChanged(this.updatingRange);
            }
        }

        public void ExpandAllFoldingBlocks()
        {
            for (int i = 0; i < this.LinesCount; i++)
            {
                this.SetVisibleState(i, VisibleState.Visible);
            }
            this.OnVisibleRangeChanged();
            this.Invalidate();
        }

        public void ExpandBlock(int iLine)
        {
            if (this.lineInfos[iLine].VisibleState != VisibleState.Visible)
            {
                int num;
                for (num = iLine; num < this.LinesCount; num++)
                {
                    if (this.lineInfos[num].VisibleState == VisibleState.Visible)
                    {
                        break;
                    }
                    this.SetVisibleState(num, VisibleState.Visible);
                    this.needRecalc = true;
                }
                for (num = iLine - 1; num >= 0; num--)
                {
                    if (this.lineInfos[num].VisibleState == VisibleState.Visible)
                    {
                        break;
                    }
                    this.SetVisibleState(num, VisibleState.Visible);
                    this.needRecalc = true;
                }
                this.Invalidate();
            }
        }

        public void ExpandBlock(int fromLine, int toLine)
        {
            int num = Math.Min(fromLine, toLine);
            int num2 = Math.Max(fromLine, toLine);
            for (int i = num; i <= num2; i++)
            {
                this.SetVisibleState(i, VisibleState.Visible);
            }
            this.needRecalc = true;
            this.Invalidate();
        }

        public void ExpandFoldedBlock(int iLine)
        {
            if ((iLine < 0) || (iLine >= this.lines.Count))
            {
                throw new ArgumentOutOfRangeException("Line index out of range");
            }
            int toLine = iLine;
            while (toLine < (this.LinesCount - 1))
            {
                if (this.lineInfos[toLine + 1].VisibleState != VisibleState.Hidden)
                {
                    break;
                }
                toLine++;
            }
            this.ExpandBlock(iLine, toLine);
        }

        public Range GetLine(int iLine)
        {
            if ((iLine < 0) || (iLine >= this.lines.Count))
            {
                throw new ArgumentOutOfRangeException("Line index out of range");
            }
            return new Range(this) { Start = new Place(0, iLine), End = new Place(this.lines[iLine].Count, iLine) };
        }

        public int GetLineLength(int iLine)
        {
            if ((iLine < 0) || (iLine >= this.lines.Count))
            {
                throw new ArgumentOutOfRangeException("Line index out of range");
            }
            return this.lines[iLine].Count;
        }

        public string GetLineText(int iLine)
        {
            if ((iLine < 0) || (iLine >= this.lines.Count))
            {
                throw new ArgumentOutOfRangeException("Line index out of range");
            }
            StringBuilder builder = new StringBuilder(this.lines[iLine].Count);
            foreach (Char ch in this.lines[iLine])
            {
                builder.Append(ch.c);
            }
            return builder.ToString();
        }

        public Range GetRange(Place fromPlace, Place toPlace)
        {
            return new Range(this, fromPlace, toPlace);
        }

        public Range GetRange(int fromPos, int toPos)
        {
            return new Range(this) { Start = this.PositionToPlace(fromPos), End = this.PositionToPlace(toPos) };
        }

        public IEnumerable<Range> GetRanges(string regexPattern)
        {
            Range iteratorVariable0 = new Range(this);
            iteratorVariable0.SelectAll();
            foreach (Range iteratorVariable1 in iteratorVariable0.GetRanges(regexPattern, RegexOptions.None))
            {
                yield return iteratorVariable1;
            }
        }

        public IEnumerable<Range> GetRanges(string regexPattern, RegexOptions options)
        {
            Range iteratorVariable0 = new Range(this);
            iteratorVariable0.SelectAll();
            foreach (Range iteratorVariable1 in iteratorVariable0.GetRanges(regexPattern, options))
            {
                yield return iteratorVariable1;
            }
        }

        public int GetStyleIndex(Style style)
        {
            return Array.IndexOf<Style>(this.Styles, style);
        }

        public StyleIndex GetStyleIndexMask(Style[] styles)
        {
            StyleIndex none = StyleIndex.None;
            foreach (Style style in styles)
            {
                int styleIndex = this.GetStyleIndex(style);
                if (styleIndex >= 0)
                {
                    none = (StyleIndex)((ushort)(none | Range.ToStyleIndex(styleIndex)));
                }
            }
            return none;
        }

        public VisibleState GetVisibleState(int iLine)
        {
            return this.lineInfos[iLine].VisibleState;
        }

        public void GoEnd()
        {
            if (this.lines.Count > 0)
            {
                this.Selection.Start = new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1);
            }
            else
            {
                this.Selection.Start = new Place(0, 0);
            }
            this.DoCaretVisible();
        }

        public void GoHome()
        {
            this.Selection.Start = new Place(0, 0);
            base.VerticalScroll.Value = 0;
            base.HorizontalScroll.Value = 0;
        }

        public void IncreaseIndent()
        {
            if (this.Selection.Start != this.Selection.End)
            {
                Range range = this.Selection.Clone();
                int iLine = Math.Min(this.Selection.Start.iLine, this.Selection.End.iLine);
                int num2 = Math.Max(this.Selection.Start.iLine, this.Selection.End.iLine);
                this.BeginUpdate();
                this.Selection.BeginUpdate();
                this.lines.Manager.BeginAutoUndoCommands();
                for (int i = iLine; i <= num2; i++)
                {
                    if (this.lines[i].Count != 0)
                    {
                        this.Selection.Start = new Place(0, i);
                        this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, new string(' ', this.TabLength)));
                    }
                }
                this.lines.Manager.EndAutoUndoCommands();
                this.Selection.Start = new Place(0, iLine);
                this.Selection.End = new Place(this.lines[num2].Count, num2);
                this.needRecalc = true;
                this.Selection.EndUpdate();
                this.EndUpdate();
                this.Invalidate();
            }
        }

        public void InsertLinePrefix(string prefix)
        {
            Range range = this.Selection.Clone();
            int fromLine = Math.Min(this.Selection.Start.iLine, this.Selection.End.iLine);
            int toLine = Math.Max(this.Selection.Start.iLine, this.Selection.End.iLine);
            this.BeginUpdate();
            this.Selection.BeginUpdate();
            this.lines.Manager.BeginAutoUndoCommands();
            int minStartSpacesCount = this.GetMinStartSpacesCount(fromLine, toLine);
            for (int i = fromLine; i <= toLine; i++)
            {
                this.Selection.Start = new Place(minStartSpacesCount, i);
                this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, prefix));
            }
            this.Selection.Start = new Place(0, fromLine);
            this.Selection.End = new Place(this.lines[toLine].Count, toLine);
            this.needRecalc = true;
            this.lines.Manager.EndAutoUndoCommands();
            this.Selection.EndUpdate();
            this.EndUpdate();
            this.Invalidate();
        }

        public void InsertText(string text)
        {
            if (text != null)
            {
                this.lines.Manager.BeginAutoUndoCommands();
                try
                {
                    if (this.Selection.Start != this.Selection.End)
                    {
                        this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
                    }
                    this.lines.Manager.ExecuteCommand(new InsertTextCommand(this.TextSource, text));
                    if (this.updating <= 0)
                    {
                        this.DoCaretVisible();
                    }
                }
                finally
                {
                    this.lines.Manager.EndAutoUndoCommands();
                }
                this.Invalidate();
            }
        }

        public void InsertText(string text, Style style)
        {
            if (text != null)
            {
                Place start = this.Selection.Start;
                this.InsertText(text);
                new Range(this, start, this.Selection.Start).SetStyle(style);
            }
        }

        public new void Invalidate()
        {
            if (base.InvokeRequired)
            {
                base.BeginInvoke(new MethodInvoker(this.Invalidate));
            }
            else
            {
                base.Invalidate();
            }
        }

        public void LowerCase()
        {
            Range range = this.Selection.Clone();
            this.SelectedText = this.SelectedText.ToLower();
            this.Selection.Start = range.Start;
            this.Selection.End = range.End;
        }

        public void Navigate(int iLine)
        {
            if (iLine < this.LinesCount)
            {
                this.lastNavigatedDateTime = this.lines[iLine].LastVisit;
                this.Selection.Start = new Place(0, iLine);
                this.DoSelectionVisible();
            }
        }

        public bool NavigateBackward()
        {
            DateTime lastVisit = new DateTime();
            int iLine = -1;
            for (int i = 0; i < this.LinesCount; i++)
            {
                if (this.lines.IsLineLoaded(i) && ((this.lines[i].LastVisit < this.lastNavigatedDateTime) && (this.lines[i].LastVisit > lastVisit)))
                {
                    lastVisit = this.lines[i].LastVisit;
                    iLine = i;
                }
            }
            if (iLine >= 0)
            {
                this.Navigate(iLine);
                return true;
            }
            return false;
        }

        public bool NavigateForward()
        {
            DateTime now = DateTime.Now;
            int iLine = -1;
            for (int i = 0; i < this.LinesCount; i++)
            {
                if (this.lines.IsLineLoaded(i) && ((this.lines[i].LastVisit > this.lastNavigatedDateTime) && (this.lines[i].LastVisit < now)))
                {
                    now = this.lines[i].LastVisit;
                    iLine = i;
                }
            }
            if (iLine >= 0)
            {
                this.Navigate(iLine);
                return true;
            }
            return false;
        }

        public void NeedRecalc()
        {
            this.needRecalc = true;
        }

        public void OnKeyPressed(char c)
        {
            KeyPressEventArgs e = new KeyPressEventArgs(c);
            if (this.KeyPressed != null)
            {
                this.KeyPressed(this, e);
            }
        }

        public void OnKeyPressing(KeyPressEventArgs args)
        {
            if (this.KeyPressing != null)
            {
                this.KeyPressing(this, args);
            }
        }

        public virtual void OnSelectionChanged()
        {
            if (this.HighlightFoldingIndicator)
            {
                this.HighlightFoldings();
            }
            this.needRiseSelectionChangedDelayed = true;
            this.ResetTimer(this.timer);
            if (this.SelectionChanged != null)
            {
                this.SelectionChanged(this, new EventArgs());
            }
        }

        public virtual void OnSelectionChangedDelayed()
        {
            this.RecalcScrollByOneLine(this.Selection.Start.iLine);
            this.ClearBracketsPositions();
            if ((this.LeftBracket != '\0') && (this.RightBracket != '\0'))
            {
                this.HighlightBrackets(this.LeftBracket, this.RightBracket, ref this.leftBracketPosition, ref this.rightBracketPosition);
            }
            if ((this.LeftBracket2 != '\0') && (this.RightBracket2 != '\0'))
            {
                this.HighlightBrackets(this.LeftBracket2, this.RightBracket2, ref this.leftBracketPosition2, ref this.rightBracketPosition2);
            }
            if (((this.Selection.Start == this.Selection.End) && (this.Selection.Start.iLine < this.LinesCount)) && (this.lastNavigatedDateTime != this.lines[this.Selection.Start.iLine].LastVisit))
            {
                this.lines[this.Selection.Start.iLine].LastVisit = DateTime.Now;
                this.lastNavigatedDateTime = this.lines[this.Selection.Start.iLine].LastVisit;
            }
            if (this.SelectionChangedDelayed != null)
            {
                this.SelectionChangedDelayed(this, new EventArgs());
            }
        }

        public virtual void OnSyntaxHighlight(TextChangedEventArgs args)
        {
            Range unionWith;
            switch (this.HighlightingRangeType)
            {
                case HighlightingRangeType.VisibleRange:
                    unionWith = this.VisibleRange.GetUnionWith(args.ChangedRange);
                    break;

                case HighlightingRangeType.AllTextRange:
                    unionWith = this.Range;
                    break;

                default:
                    unionWith = args.ChangedRange;
                    break;
            }
            if (this.SyntaxHighlighter != null)
            {
                if (!((this.Language != Language.Custom) || string.IsNullOrEmpty(this.DescriptionFile)))
                {
                    this.SyntaxHighlighter.HighlightSyntax(this.DescriptionFile, unionWith);
                }
                else
                {
                    this.SyntaxHighlighter.HighlightSyntax(this.Language, unionWith);
                }
            }
        }

        public virtual void OnTextChanged()
        {
            Range changedRange = new Range(this);
            changedRange.SelectAll();
            this.OnTextChanged(new TextChangedEventArgs(changedRange));
        }

        public virtual void OnTextChanged(Range r)
        {
            this.OnTextChanged(new TextChangedEventArgs(r));
        }

        public virtual void OnTextChanged(int fromLine, int toLine)
        {
            Range changedRange = new Range(this)
            {
                Start = new Place(0, Math.Min(fromLine, toLine)),
                End = new Place(this.lines[Math.Max(fromLine, toLine)].Count, Math.Max(fromLine, toLine))
            };
            this.OnTextChanged(new TextChangedEventArgs(changedRange));
        }

        public virtual void OnTextChangedDelayed(Range changedRange)
        {
            if (this.TextChangedDelayed != null)
            {
                this.TextChangedDelayed(this, new TextChangedEventArgs(changedRange));
            }
        }

        public virtual void OnTextChanging()
        {
            string text = null;
            this.OnTextChanging(ref text);
        }

        public virtual void OnTextChanging(ref string text)
        {
            this.ClearBracketsPositions();
            if (this.TextChanging != null)
            {
                TextChangingEventArgs e = new TextChangingEventArgs
                {
                    InsertingText = text
                };
                this.TextChanging(this, e);
                text = e.InsertingText;
                if (e.Cancel)
                {
                    text = string.Empty;
                }
            }
        }

        public void OnUndoRedoStateChanged()
        {
            if (this.UndoRedoStateChanged != null)
            {
                this.UndoRedoStateChanged(this, EventArgs.Empty);
            }
        }

        public virtual void OnVisibleRangeChanged()
        {
            this.needRiseVisibleRangeChangedDelayed = true;
            this.ResetTimer(this.timer);
            if (this.VisibleRangeChanged != null)
            {
                this.VisibleRangeChanged(this, new EventArgs());
            }
        }

        public virtual void OnVisibleRangeChangedDelayed()
        {
            if (this.VisibleRangeChangedDelayed != null)
            {
                this.VisibleRangeChangedDelayed(this, new EventArgs());
            }
        }

        public virtual void OnVisualMarkerClick(MouseEventArgs args, StyleVisualMarker marker)
        {
            if (this.VisualMarkerClick != null)
            {
                this.VisualMarkerClick(this, new VisualMarkerEventArgs(marker.Style, marker, args));
            }
        }

        public void OpenBindingFile(string fileName, Encoding enc)
        {
            try
            {
                FileTextSource ts = new FileTextSource(this);
                this.InitTextSource(ts);
                ts.OpenFile(fileName, enc);
                this.IsChanged = false;
                this.OnVisibleRangeChanged();
            }
            catch
            {
                this.InitTextSource(this.CreateTextSource());
                this.lines.InsertLine(0, this.TextSource.CreateLine());
                this.IsChanged = false;
                throw;
            }
        }

        public void Paste()
        {
            string text = null;
            var thread = new Thread(() =>
            {
                if (Clipboard.ContainsText())
                    text = Clipboard.GetText();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            thread.Join();

            if (text != null)
            {
                this.InsertText(text);
            }
        }

        public Point PlaceToPoint(Place place)
        {
            if (place.iLine >= this.lineInfos.Count)
            {
                return new Point();
            }
            int startY = this.lineInfos[place.iLine].startY;
            int wordWrapStringIndex = this.lineInfos[place.iLine].GetWordWrapStringIndex(place.iChar);
            startY += wordWrapStringIndex * this.CharHeight;
            LineInfo info = this.lineInfos[place.iLine];
            int num3 = (place.iChar - info.GetWordWrapStringStartPosition(wordWrapStringIndex)) * this.CharWidth;
            startY -= base.VerticalScroll.Value;
            return new Point(((this.LeftIndent + this.Padding.Left) + num3) - base.HorizontalScroll.Value, startY);
        }

        public int PlaceToPosition(Place point)
        {
            if (((point.iLine < 0) || (point.iLine >= this.lines.Count)) || (point.iChar >= (this.lines[point.iLine].Count + Environment.NewLine.Length)))
            {
                return -1;
            }
            int num = 0;
            for (int i = 0; i < point.iLine; i++)
            {
                num += this.lines[i].Count + Environment.NewLine.Length;
            }
            return (num + point.iChar);
        }

        public Place PointToPlace(Point point)
        {
            LineInfo info;
            point.Offset(base.HorizontalScroll.Value, base.VerticalScroll.Value);
            point.Offset(-this.LeftIndent - this.Padding.Left, 0);
            int iLine = this.YtoLineIndex(point.Y);
            int num2 = 0;
            while (iLine < this.lines.Count)
            {
                info = this.lineInfos[iLine];
                num2 = this.lineInfos[iLine].startY + (info.WordWrapStringsCount * this.CharHeight);
                if ((num2 > point.Y) && (this.lineInfos[iLine].VisibleState == VisibleState.Visible))
                {
                    break;
                }
                iLine++;
            }
            if (iLine >= this.lines.Count)
            {
                iLine = this.lines.Count - 1;
            }
            if (this.lineInfos[iLine].VisibleState != VisibleState.Visible)
            {
                iLine = this.FindPrevVisibleLine(iLine);
            }
            info = this.lineInfos[iLine];
            int wordWrapStringsCount = info.WordWrapStringsCount;
            do
            {
                wordWrapStringsCount--;
                num2 -= this.CharHeight;
            }
            while (num2 > point.Y);
            if (wordWrapStringsCount < 0)
            {
                wordWrapStringsCount = 0;
            }
            int wordWrapStringStartPosition = this.lineInfos[iLine].GetWordWrapStringStartPosition(wordWrapStringsCount);
            int wordWrapStringFinishPosition = this.lineInfos[iLine].GetWordWrapStringFinishPosition(wordWrapStringsCount, this.lines[iLine]);
            int iChar = (int)Math.Round((double)(((float)point.X) / ((float)this.CharWidth)));
            iChar = (iChar < 0) ? wordWrapStringStartPosition : (wordWrapStringStartPosition + iChar);
            if (iChar > wordWrapStringFinishPosition)
            {
                iChar = wordWrapStringFinishPosition + 1;
            }
            if (iChar > this.lines[iLine].Count)
            {
                iChar = this.lines[iLine].Count;
            }
            return new Place(iChar, iLine);
        }

        public int PointToPosition(Point point)
        {
            return this.PlaceToPosition(this.PointToPlace(point));
        }

        public Place PositionToPlace(int pos)
        {
            if (pos < 0)
            {
                return new Place(0, 0);
            }
            for (int i = 0; i < this.lines.Count; i++)
            {
                int num2 = this.lines[i].Count + Environment.NewLine.Length;
                if (pos < this.lines[i].Count)
                {
                    return new Place(pos, i);
                }
                if (pos < num2)
                {
                    return new Place(this.lines[i].Count, i);
                }
                pos -= num2;
            }
            if (this.lines.Count > 0)
            {
                return new Place(this.lines[this.lines.Count - 1].Count, this.lines.Count - 1);
            }
            return new Place(0, 0);
        }

        public void Print()
        {
            PrintDialogSettings settings = new PrintDialogSettings
            {
                ShowPageSetupDialog = false,
                ShowPrintDialog = false,
                ShowPrintPreviewDialog = false
            };
            this.Print(this.Range, settings);
        }

        public void Print(PrintDialogSettings settings)
        {
            this.Print(this.Range, settings);
        }

        public void Print(Range range, PrintDialogSettings settings)
        {
            WebBrowser browser = new WebBrowser();

            settings.printRange = range;
            browser.Tag = settings;
            browser.Visible = false;
            browser.Location = new Point(-1000, -1000);
            browser.Parent = this;
            browser.Navigate("about:blank");
            browser.Navigated += new WebBrowserNavigatedEventHandler(this.ShowPrintDialog);
        }

        public void Redo()
        {
            this.lines.Manager.Redo();
            this.Invalidate();
        }

        public void RemoveLinePrefix(string prefix)
        {
            Range range = this.Selection.Clone();
            int iLine = Math.Min(this.Selection.Start.iLine, this.Selection.End.iLine);
            int num2 = Math.Max(this.Selection.Start.iLine, this.Selection.End.iLine);
            this.BeginUpdate();
            this.Selection.BeginUpdate();
            this.lines.Manager.BeginAutoUndoCommands();
            for (int i = iLine; i <= num2; i++)
            {
                string text = this.lines[i].Text;
                string str2 = text.TrimStart(new char[0]);
                if (str2.StartsWith(prefix))
                {
                    int iChar = text.Length - str2.Length;
                    this.Selection.Start = new Place(iChar, i);
                    this.Selection.End = new Place(iChar + prefix.Length, i);
                    this.ClearSelected();
                }
            }
            this.Selection.Start = new Place(0, iLine);
            this.Selection.End = new Place(this.lines[num2].Count, num2);
            this.needRecalc = true;
            this.lines.Manager.EndAutoUndoCommands();
            this.Selection.EndUpdate();
            this.EndUpdate();
        }

        public void SaveToFile(string fileName, Encoding enc)
        {
            this.lines.SaveToFile(fileName, enc);
            this.IsChanged = false;
            this.OnVisibleRangeChanged();
        }

        public void ScrollLeft()
        {
            this.Invalidate();
            base.HorizontalScroll.Value = 0;
            this.AutoScrollMinSize -= new Size(1, 0);
            this.AutoScrollMinSize += new Size(1, 0);
        }

        public void SelectAll()
        {
            this.Selection.SelectAll();
        }

        public void SetVisibleState(int iLine, VisibleState state)
        {
            LineInfo info = this.lineInfos[iLine];
            info.VisibleState = state;
            this.lineInfos[iLine] = info;
            this.needRecalc = true;
        }

        public void ShowFindDialog()
        {
            this.ShowFindDialog(null);
        }

        public void ShowFindDialog(string findText)
        {
            if (this.findForm == null)
            {
                this.findForm = new FindForm(this);
            }
            if (findText != null)
            {
                this.findForm.tbFind.Text = findText;
            }
            else if ((this.Selection.Start != this.Selection.End) && (this.Selection.Start.iLine == this.Selection.End.iLine))
            {
                this.findForm.tbFind.Text = this.Selection.Text;
            }
            this.findForm.tbFind.SelectAll();
            this.findForm.Show();
        }

        public void ShowGoToDialog()
        {
            GoToForm form = new GoToForm
            {
                TotalLineCount = this.LinesCount,
                SelectedLineNumber = this.Selection.Start.iLine + 1
            };
            if (form.ShowDialog() == DialogResult.OK)
            {
                int iStartLine = Math.Min(this.LinesCount - 1, Math.Max(0, form.SelectedLineNumber - 1));
                this.Selection = new Range(this, 0, iStartLine, 0, iStartLine);
                this.DoSelectionVisible();
            }
        }

        public void ShowReplaceDialog()
        {
            this.ShowReplaceDialog(null);
        }

        public void ShowReplaceDialog(string findText)
        {
            if (!this.ReadOnly)
            {
                if (this.replaceForm == null)
                {
                    this.replaceForm = new ReplaceForm(this);
                }
                if (findText != null)
                {
                    this.replaceForm.tbFind.Text = findText;
                }
                else if ((this.Selection.Start != this.Selection.End) && (this.Selection.Start.iLine == this.Selection.End.iLine))
                {
                    this.replaceForm.tbFind.Text = this.Selection.Text;
                }
                this.replaceForm.tbFind.SelectAll();
                this.replaceForm.Show();
            }
        }

        public void Undo()
        {
            this.lines.Manager.Undo();
            this.Invalidate();
        }

        public void UpperCase()
        {
            Range range = this.Selection.Clone();
            this.SelectedText = this.SelectedText.ToUpper();
            this.Selection.Start = range.Start;
            this.Selection.End = range.End;
        }

        internal void AddVisualMarker(VisualMarker marker)
        {
            this.visibleMarkers.Add(marker);
        }
        internal virtual void CalcAutoIndentShiftByCodeFolding(object sender, AutoIndentEventArgs args)
        {
            if (!(!string.IsNullOrEmpty(this.lines[args.iLine].FoldingEndMarker) || string.IsNullOrEmpty(this.lines[args.iLine].FoldingStartMarker)))
            {
                args.ShiftNextLines = this.TabLength;
            }
            else if (!(string.IsNullOrEmpty(this.lines[args.iLine].FoldingEndMarker) || !string.IsNullOrEmpty(this.lines[args.iLine].FoldingStartMarker)))
            {
                args.Shift = -this.TabLength;
                args.ShiftNextLines = -this.TabLength;
            }
        }
        internal int FindNextVisibleLine(int iLine)
        {
            if (iLine < (this.lines.Count - 1))
            {
                int num = iLine;
                do
                {
                    iLine++;
                }
                while ((iLine < (this.lines.Count - 1)) && (this.lineInfos[iLine].VisibleState != VisibleState.Visible));
                if (this.lineInfos[iLine].VisibleState != VisibleState.Visible)
                {
                    return num;
                }
            }
            return iLine;
        }

        internal int FindPrevVisibleLine(int iLine)
        {
            if (iLine > 0)
            {
                int num = iLine;
                do
                {
                    iLine--;
                }
                while ((iLine > 0) && (this.lineInfos[iLine].VisibleState != VisibleState.Visible));
                if (this.lineInfos[iLine].VisibleState != VisibleState.Visible)
                {
                    return num;
                }
            }
            return iLine;
        }

        internal int GetOrSetStyleLayerIndex(Style style)
        {
            int styleIndex = this.GetStyleIndex(style);
            if (styleIndex < 0)
            {
                styleIndex = this.AddStyle(style);
            }
            return styleIndex;
        }

        internal void OnLineInserted(int index)
        {
            this.OnLineInserted(index, 1);
        }

        internal void OnLineInserted(int index, int count)
        {
            if (this.LineInserted != null)
            {
                this.LineInserted(this, new LineInsertedEventArgs(index, count));
            }
        }

        internal void OnLineRemoved(int index, int count, List<int> removedLineIds)
        {
            if ((count > 0) && (this.LineRemoved != null))
            {
                this.LineRemoved(this, new LineRemovedEventArgs(index, count, removedLineIds));
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (this.SyntaxHighlighter != null)
                {
                    this.SyntaxHighlighter.Dispose();
                }
                this.timer.Dispose();
                this.timer2.Dispose();
                if (this.findForm != null)
                {
                    this.findForm.Dispose();
                }
                if (this.replaceForm != null)
                {
                    this.replaceForm.Dispose();
                }
                if (this.Font != null)
                {
                    this.Font.Dispose();
                }
                if (this.TextSource != null)
                {
                    this.TextSource.Dispose();
                }
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (!((keyData != Keys.Tab) || this.AcceptsTab))
            {
                return false;
            }
            if (!((keyData != Keys.Enter) || this.AcceptsReturn))
            {
                return false;
            }
            if ((keyData & Keys.Alt) == Keys.None)
            {
                Keys keys = keyData & Keys.KeyCode;
                if (keys == Keys.Enter)
                {
                    return true;
                }
            }
            if ((keyData & Keys.Alt) != Keys.Alt)
            {
                switch ((keyData & Keys.KeyCode))
                {
                    case Keys.Escape:
                        return false;

                    case Keys.PageUp:
                    case Keys.Next:
                    case Keys.End:
                    case Keys.Home:
                    case Keys.Left:
                    case Keys.Up:
                    case Keys.Right:
                    case Keys.Down:
                        return true;

                    case Keys.Tab:
                        return ((keyData & Keys.Control) == Keys.None);
                }
            }
            return base.IsInputKey(keyData);
        }

        protected virtual void OnCharSizeChanged()
        {
            base.VerticalScroll.SmallChange = this.charHeight;
            base.VerticalScroll.LargeChange = 10 * this.charHeight;
            base.HorizontalScroll.SmallChange = this.CharWidth;
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            if (this.WordWrap)
            {
                this.RecalcWordWrap(0, this.lines.Count - 1);
                this.Invalidate();
            }
            this.OnVisibleRangeChanged();
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.mouseIsDrag = false;
        }

        protected virtual void OnFoldingHighlightChanged()
        {
            if (this.FoldingHighlightChanged != null)
            {
                this.FoldingHighlightChanged(this, EventArgs.Empty);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            this.SetAsCurrentTB();
            base.OnGotFocus(e);
            this.Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            this.lastModifiers = e.Modifiers;
            this.handledChar = false;
            if (e.Handled)
            {
                this.handledChar = true;
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Back:
                        if (!this.ReadOnly)
                        {
                            if (e.Modifiers == Keys.Alt)
                            {
                                this.Undo();
                            }
                            else if (e.Modifiers == Keys.None)
                            {
                                if (!this.OnKeyPressing('\b'))
                                {
                                    if (this.Selection.End != this.Selection.Start)
                                    {
                                        this.ClearSelected();
                                    }
                                    else
                                    {
                                        this.InsertChar('\b');
                                    }
                                    this.OnKeyPressed('\b');
                                }
                            }
                            else if ((e.Modifiers == Keys.Control) && !this.OnKeyPressing('\b'))
                            {
                                if (this.Selection.End != this.Selection.Start)
                                {
                                    this.ClearSelected();
                                }
                                this.Selection.GoWordLeft(true);
                                this.ClearSelected();
                                this.OnKeyPressed('\b');
                            }
                        }
                        break;

                    case Keys.Tab:
                        if (!((e.Modifiers != Keys.Shift) || this.ReadOnly))
                        {
                            this.DecreaseIndent();
                        }
                        break;

                    case Keys.Space:
                        if (!this.ReadOnly && (((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift)) && !this.OnKeyPressing(' ')))
                        {
                            if (this.Selection.End != this.Selection.Start)
                            {
                                this.ClearSelected();
                            }
                            if (this.IsReplaceMode)
                            {
                                this.Selection.GoRight(true);
                                this.Selection.Inverse();
                            }
                            this.InsertChar(' ');
                            this.OnKeyPressed(' ');
                        }
                        break;

                    case Keys.PageUp:
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.Selection.GoPageUp(e.Shift);
                            this.ScrollLeft();
                        }
                        break;

                    case Keys.Next:
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.Selection.GoPageDown(e.Shift);
                            this.ScrollLeft();
                        }
                        break;

                    case Keys.End:
                        if ((e.Modifiers == Keys.Control) || (e.Modifiers == (Keys.Control | Keys.Shift)))
                        {
                            this.Selection.GoLast(e.Shift);
                        }
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.Selection.GoEnd(e.Shift);
                        }
                        break;

                    case Keys.Home:
                        if ((e.Modifiers == Keys.Control) || (e.Modifiers == (Keys.Control | Keys.Shift)))
                        {
                            this.Selection.GoFirst(e.Shift);
                        }
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.GoHome(e.Shift);
                            this.ScrollLeft();
                        }
                        break;

                    case Keys.Left:
                        if ((e.Modifiers == Keys.Control) || (e.Modifiers == (Keys.Control | Keys.Shift)))
                        {
                            this.Selection.GoWordLeft(e.Shift);
                        }
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.Selection.GoLeft(e.Shift);
                        }
                        break;

                    case Keys.Up:
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.Selection.GoUp(e.Shift);
                            this.ScrollLeft();
                        }
                        break;

                    case Keys.Right:
                        if ((e.Modifiers == Keys.Control) || (e.Modifiers == (Keys.Control | Keys.Shift)))
                        {
                            this.Selection.GoWordRight(e.Shift);
                        }
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.Selection.GoRight(e.Shift);
                        }
                        break;

                    case Keys.Down:
                        if ((e.Modifiers == Keys.None) || (e.Modifiers == Keys.Shift))
                        {
                            this.Selection.GoDown(e.Shift);
                            this.ScrollLeft();
                        }
                        break;

                    case Keys.Delete:
                        if (!this.ReadOnly)
                        {
                            if (e.Modifiers == Keys.None)
                            {
                                if (!this.OnKeyPressing('\x00ff'))
                                {
                                    if (this.Selection.End != this.Selection.Start)
                                    {
                                        this.ClearSelected();
                                    }
                                    else if (this.Selection.GoRightThroughFolded())
                                    {
                                        int iLine = this.Selection.Start.iLine;
                                        this.InsertChar('\b');
                                        if ((iLine != this.Selection.Start.iLine) && this.AutoIndent)
                                        {
                                            this.RemoveSpacesAfterCaret();
                                        }
                                    }
                                    this.OnKeyPressed('\x00ff');
                                }
                            }
                            else if ((e.Modifiers == Keys.Control) && !this.OnKeyPressing('\x00ff'))
                            {
                                if (this.Selection.End != this.Selection.Start)
                                {
                                    this.ClearSelected();
                                }
                                else
                                {
                                    this.Selection.GoWordRight(true);
                                    this.ClearSelected();
                                }
                                this.OnKeyPressed('\x00ff');
                            }
                        }
                        break;

                    case Keys.A:
                        if (e.Modifiers == Keys.Control)
                        {
                            this.Selection.SelectAll();
                        }
                        break;

                    case Keys.C:
                        if (e.Modifiers == Keys.Control)
                        {
                            this.Copy();
                        }
                        if (e.Modifiers == (Keys.Control | Keys.Shift))
                        {
                            this.CommentSelected();
                        }
                        break;

                    case Keys.F:
                        if (e.Modifiers == Keys.Control)
                        {
                            this.ShowFindDialog();
                        }
                        break;

                    case Keys.G:
                        if (e.Modifiers == Keys.Control)
                        {
                            this.ShowGoToDialog();
                        }
                        break;

                    case Keys.H:
                        if (e.Modifiers == Keys.Control)
                        {
                            this.ShowReplaceDialog();
                        }
                        break;

                    case Keys.R:
                        if (!((e.Modifiers != Keys.Control) || this.ReadOnly))
                        {
                            this.Redo();
                        }
                        break;

                    case Keys.U:
                        if (e.Modifiers == (Keys.Control | Keys.Shift))
                        {
                            this.LowerCase();
                        }
                        if (e.Modifiers == Keys.Control)
                        {
                            this.UpperCase();
                        }
                        break;

                    case Keys.V:
                        if (!((e.Modifiers != Keys.Control) || this.ReadOnly))
                        {
                            this.Paste();
                        }
                        break;

                    case Keys.X:
                        if (!((e.Modifiers != Keys.Control) || this.ReadOnly))
                        {
                            this.Cut();
                        }
                        break;

                    case Keys.Z:
                        if (!((e.Modifiers != Keys.Control) || this.ReadOnly))
                        {
                            this.Undo();
                        }
                        break;

                    case Keys.F3:
                        if (e.Modifiers == Keys.None)
                        {
                            if ((this.findForm == null) || (this.findForm.tbFind.Text == ""))
                            {
                                this.ShowFindDialog();
                            }
                            else
                            {
                                this.findForm.FindNext();
                            }
                        }
                        break;

                    case Keys.OemMinus:
                        if (e.Modifiers == Keys.Control)
                        {
                            this.NavigateBackward();
                        }
                        if (e.Modifiers == (Keys.Control | Keys.Shift))
                        {
                            this.NavigateForward();
                        }
                        break;

                    default:
                        if ((((e.Modifiers & Keys.Control) != Keys.None) || ((e.Modifiers & Keys.Alt) != Keys.None)) || (e.KeyCode == Keys.ShiftKey))
                        {
                            return;
                        }
                        break;
                }
                e.Handled = true;
                this.DoCaretVisible();
                this.Invalidate();
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.KeyCode == Keys.ShiftKey)
            {
                this.lastModifiers &= ~Keys.Shift;
            }
            if (e.KeyCode == Keys.Alt)
            {
                this.lastModifiers &= ~Keys.Alt;
            }
            if (e.KeyCode == Keys.ControlKey)
            {
                this.lastModifiers &= ~Keys.Control;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.m_hImc = ImmGetContext(base.Handle);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.Invalidate();
        }

        protected virtual void OnMarkerClick(MouseEventArgs args, VisualMarker marker)
        {
            if (marker is StyleVisualMarker)
            {
                this.OnVisualMarkerClick(args, marker as StyleVisualMarker);
            }
            else if (marker is CollapseFoldingMarker)
            {
                this.CollapseFoldingBlock((marker as CollapseFoldingMarker).iLine);
                this.OnVisibleRangeChanged();
                this.Invalidate();
            }
            else if (marker is ExpandFoldingMarker)
            {
                this.ExpandFoldedBlock((marker as ExpandFoldingMarker).iLine);
                this.OnVisibleRangeChanged();
                this.Invalidate();
            }
            else if (marker is FoldedAreaMarker)
            {
                int iLine = (marker as FoldedAreaMarker).iLine;
                int num2 = this.FindEndOfFoldingBlock(iLine);
                if (num2 >= 0)
                {
                    this.Selection.BeginUpdate();
                    this.Selection.Start = new Place(0, iLine);
                    this.Selection.End = new Place(this.lines[num2].Count, num2);
                    this.Selection.EndUpdate();
                    this.Invalidate();
                }
            }
        }

        protected virtual void OnMarkerDoubleClick(VisualMarker marker)
        {
            if (marker is FoldedAreaMarker)
            {
                this.ExpandFoldedBlock((marker as FoldedAreaMarker).iLine);
                this.Invalidate();
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            VisualMarker marker = this.FindVisualMarkerForPoint(e.Location);
            if (marker != null)
            {
                this.OnMarkerDoubleClick(marker);
            }
            else
            {
                int num3;
                char c;
                Place place = this.PointToPlace(e.Location);
                int iChar = place.iChar;
                int num2 = place.iChar;
                for (num3 = place.iChar; num3 < this.lines[place.iLine].Count; num3++)
                {
                    c = this.lines[place.iLine][num3].c;
                    if (!char.IsLetterOrDigit(c) && (c != '_'))
                    {
                        break;
                    }
                    num2 = num3 + 1;
                }
                for (num3 = place.iChar - 1; num3 >= 0; num3--)
                {
                    c = this.lines[place.iLine][num3].c;
                    if (!char.IsLetterOrDigit(c) && (c != '_'))
                    {
                        break;
                    }
                    iChar = num3;
                }
                this.Selection.Start = new Place(num2, place.iLine);
                this.Selection.End = new Place(iChar, place.iLine);
                this.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (e.Button == MouseButtons.Left)
            {
                VisualMarker marker = this.FindVisualMarkerForPoint(e.Location);
                if (marker != null)
                {
                    this.mouseIsDrag = false;
                    this.OnMarkerClick(e, marker);
                }
                else
                {
                    this.mouseIsDrag = true;
                    Place end = this.Selection.End;
                    this.Selection.BeginUpdate();
                    this.Selection.Start = this.PointToPlace(e.Location);
                    if ((this.lastModifiers & Keys.Shift) != Keys.None)
                    {
                        this.Selection.End = end;
                    }
                    this.Selection.EndUpdate();
                    this.Invalidate();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if ((e.Button == MouseButtons.Left) && this.mouseIsDrag)
            {
                Place end = this.Selection.End;
                this.Selection.BeginUpdate();
                this.Selection.Start = this.PointToPlace(e.Location);
                this.Selection.End = end;
                this.Selection.EndUpdate();
                this.DoCaretVisible();
                this.Invalidate();
            }
            else
            {
                VisualMarker marker = this.FindVisualMarkerForPoint(e.Location);
                if (marker != null)
                {
                    this.Cursor = marker.Cursor;
                }
                else
                {
                    this.Cursor = Cursors.IBeam;
                }
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseWheel(e);
            this.OnVisibleRangeChanged();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.needRecalc)
            {
                this.Recalc();
            }
            this.visibleMarkers.Clear();
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Brush brush = new SolidBrush(this.LineNumberColor);
            Pen pen = new Pen(this.ServiceLinesColor);
            Brush brush2 = new SolidBrush(this.ChangedLineColor);
            Brush brush3 = new SolidBrush(this.IndentBackColor);
            Brush brush4 = new SolidBrush(this.PaddingBackColor);
            Pen pen2 = new Pen(this.CurrentLineColor);
            Brush brush5 = new SolidBrush(Color.FromArgb(50, this.CurrentLineColor));
            e.Graphics.FillRectangle(brush4, 0, -base.VerticalScroll.Value, base.ClientSize.Width, Math.Max(0, this.Padding.Top - 1));
            int num = (this.wordWrapLinesCount * this.charHeight) + this.Padding.Top;
            e.Graphics.FillRectangle(brush4, 0, num - base.VerticalScroll.Value, base.ClientSize.Width, base.ClientSize.Height);
            int num2 = ((this.LeftIndent + (this.maxLineLength * this.CharWidth)) + this.Padding.Left) + 1;
            //Console.WriteLine(num2);//930
            e.Graphics.FillRectangle(brush4, num2 - base.HorizontalScroll.Value, 0, base.ClientSize.Width, base.ClientSize.Height);
            e.Graphics.FillRectangle(brush4, this.LeftIndent - base.HorizontalScroll.Value, 0, Math.Max(0, this.Padding.Left - 1), base.ClientSize.Height);
            e.Graphics.FillRectangle(brush4, this.LeftIndentLine, 0, (this.LeftIndent - this.LeftIndentLine) + 1, base.ClientSize.Height);
            int x = Math.Max(this.LeftIndent, (this.LeftIndent + this.Padding.Left) - base.HorizontalScroll.Value);
            int width = (num2 - base.HorizontalScroll.Value) - x;
            e.Graphics.FillRectangle(brush3, 0, 0, this.LeftIndentLine, base.ClientSize.Height);
            if (this.LeftIndent > 8)
            {
                e.Graphics.DrawLine(pen, this.LeftIndentLine, 0, this.LeftIndentLine, base.ClientSize.Height);
            }
            if (this.PreferredLineWidth > 0)
            {
                e.Graphics.DrawLine(pen, new Point((((this.LeftIndent + this.Padding.Left) + (this.PreferredLineWidth * this.CharWidth)) - base.HorizontalScroll.Value) + 1, 0), new Point((((this.LeftIndent + this.Padding.Left) + (this.PreferredLineWidth * this.CharWidth)) - base.HorizontalScroll.Value) + 1, base.Height));
            }
            int firstChar = Math.Max(0, base.HorizontalScroll.Value - this.Padding.Left) / this.CharWidth;
            int lastChar = (base.HorizontalScroll.Value + base.ClientSize.Width) / this.CharWidth;
            if (width > this.CharWidth)
            {
                for (int i = this.YtoLineIndex(base.VerticalScroll.Value); i < this.lines.Count; i++)
                {
                    Line line = this.lines[i];
                    LineInfo info = this.lineInfos[i];
                    if (info.startY > (base.VerticalScroll.Value + base.ClientSize.Height))
                    {
                        break;
                    }
                    if (((info.startY + (info.WordWrapStringsCount * this.CharHeight)) >= base.VerticalScroll.Value) && (info.VisibleState != VisibleState.Hidden))
                    {
                        int y = info.startY - base.VerticalScroll.Value;
                        e.Graphics.SmoothingMode = SmoothingMode.None;
                        if ((info.VisibleState == VisibleState.Visible) && (line.BackgroundBrush != null))
                        {
                            e.Graphics.FillRectangle(line.BackgroundBrush, new Rectangle(x, y, width, this.CharHeight * info.WordWrapStringsCount));
                        }
                        if ((this.CurrentLineColor != Color.Transparent) && (i == this.Selection.Start.iLine))
                        {
                            if (this.Selection.Start == this.Selection.End)
                            {
                                e.Graphics.FillRectangle(brush5, new Rectangle(x, y, width, this.CharHeight));
                            }
                            else
                            {
                                e.Graphics.DrawLine(pen2, x, y + this.CharHeight, x + width, y + this.CharHeight);
                            }
                        }
                        if ((this.ChangedLineColor != Color.Transparent) && line.IsChanged)
                        {
                            e.Graphics.FillRectangle(brush2, new RectangleF(-10f, (float)y, (float)(((this.LeftIndent - 8) - 2) + 10), (float)(this.CharHeight + 1)));
                        }
                        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        if (info.VisibleState == VisibleState.Visible)
                        {
                            this.OnPaintLine(new PaintLineEventArgs(i, new Rectangle(this.LeftIndent, y, base.Width, this.CharHeight * info.WordWrapStringsCount), e.Graphics, e.ClipRectangle));
                        }
                        if (this.ShowLineNumbers)
                        {
                            long num13 = i + this.lineNumberStartValue;
                            e.Graphics.DrawString(num13.ToString(), this.Font, brush, new RectangleF(-10f, (float)y, (float)(((this.LeftIndent - 8) - 2) + 10), (float)this.CharHeight), new StringFormat(StringFormatFlags.DirectionRightToLeft));
                        }
                        if (info.VisibleState == VisibleState.StartOfHiddenBlock)
                        {
                            this.visibleMarkers.Add(new ExpandFoldingMarker(i, new Rectangle(this.LeftIndentLine - 4, (y + (this.CharHeight / 2)) - 3, 8, 8)));
                        }
                        if ((!string.IsNullOrEmpty(line.FoldingStartMarker) && (info.VisibleState == VisibleState.Visible)) && string.IsNullOrEmpty(line.FoldingEndMarker))
                        {
                            this.visibleMarkers.Add(new CollapseFoldingMarker(i, new Rectangle(this.LeftIndentLine - 4, (y + (this.CharHeight / 2)) - 3, 8, 8)));
                        }
                        if (((info.VisibleState == VisibleState.Visible) && !string.IsNullOrEmpty(line.FoldingEndMarker)) && string.IsNullOrEmpty(line.FoldingStartMarker))
                        {
                            e.Graphics.DrawLine(pen, this.LeftIndentLine, (y + (this.CharHeight * info.WordWrapStringsCount)) - 1, this.LeftIndentLine + 4, (y + (this.CharHeight * info.WordWrapStringsCount)) - 1);
                        }
                        for (int j = 0; j < info.WordWrapStringsCount; j++)
                        {
                            y = (info.startY + (j * this.CharHeight)) - base.VerticalScroll.Value;
                            this.DrawLineChars(e, firstChar, lastChar, i, j, (this.LeftIndent + this.Padding.Left) - base.HorizontalScroll.Value, y);
                        }
                    }
                }
            }
            if (((this.BracketsStyle != null) && (this.leftBracketPosition != null)) && (this.rightBracketPosition != null))
            {
                this.BracketsStyle.Draw(e.Graphics, this.PlaceToPoint(this.leftBracketPosition.Start), this.leftBracketPosition);
                this.BracketsStyle.Draw(e.Graphics, this.PlaceToPoint(this.rightBracketPosition.Start), this.rightBracketPosition);
            }
            if (((this.BracketsStyle2 != null) && (this.leftBracketPosition2 != null)) && (this.rightBracketPosition2 != null))
            {
                this.BracketsStyle2.Draw(e.Graphics, this.PlaceToPoint(this.leftBracketPosition2.Start), this.leftBracketPosition2);
                this.BracketsStyle2.Draw(e.Graphics, this.PlaceToPoint(this.rightBracketPosition2.Start), this.rightBracketPosition2);
            }
            e.Graphics.SmoothingMode = SmoothingMode.None;
            if ((((this.startFoldingLine >= 0) || (this.endFoldingLine >= 0)) && (this.Selection.Start == this.Selection.End)) && (this.endFoldingLine < this.lineInfos.Count))
            {
                int num10 = (((this.startFoldingLine >= 0) ? this.lineInfos[this.startFoldingLine].startY : 0) - base.VerticalScroll.Value) + (this.CharHeight / 2);
                int num11 = (((this.endFoldingLine >= 0) ? (this.lineInfos[this.endFoldingLine].startY + ((this.lineInfos[this.endFoldingLine].WordWrapStringsCount - 1) * this.CharHeight)) : ((this.WordWrapLinesCount + 1) * this.CharHeight)) - base.VerticalScroll.Value) + this.CharHeight;
                using (Pen pen3 = new Pen(Color.FromArgb(100, this.FoldingIndicatorColor), 4f))
                {
                    e.Graphics.DrawLine(pen3, this.LeftIndent - 5, num10, this.LeftIndent - 5, num11);
                }
            }
            foreach (VisualMarker marker in this.visibleMarkers)
            {
                marker.Draw(e.Graphics, pen);
            }
            Point point = this.PlaceToPoint(this.Selection.Start);
            if ((this.Focused && (point.X >= this.LeftIndent)) && this.CaretVisible)
            {
                int nWidth = this.IsReplaceMode ? this.CharWidth : 1;
                CreateCaret(base.Handle, 0, nWidth, this.CharHeight + 1);
                SetCaretPos(point.X, point.Y);
                ShowCaret(base.Handle);
                using (Pen pen4 = new Pen(this.CaretColor))
                {
                    e.Graphics.DrawLine(pen4, point.X, point.Y, point.X, point.Y + this.CharHeight);
                }
            }
            else
            {
                HideCaret(base.Handle);
            }
            brush.Dispose();
            pen.Dispose();
            brush2.Dispose();
            brush3.Dispose();
            pen2.Dispose();
            brush5.Dispose();
            brush4.Dispose();
            base.OnPaint(e);
        }

        protected virtual void OnPaintLine(PaintLineEventArgs e)
        {
            if (this.PaintLine != null)
            {
                this.PaintLine(this, e);
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            base.OnScroll(se);
            this.OnVisibleRangeChanged();
            this.Invalidate();
        }

        protected virtual void OnTextChanged(TextChangedEventArgs args)
        {
            args.ChangedRange.Normalize();
            if (this.updating > 0)
            {
                if (this.updatingRange == null)
                {
                    this.updatingRange = args.ChangedRange.Clone();
                }
                else
                {
                    if (this.updatingRange.Start.iLine > args.ChangedRange.Start.iLine)
                    {
                        this.updatingRange.Start = new Place(0, args.ChangedRange.Start.iLine);
                    }
                    if (this.updatingRange.End.iLine < args.ChangedRange.End.iLine)
                    {
                        this.updatingRange.End = new Place(this.lines[args.ChangedRange.End.iLine].Count, args.ChangedRange.End.iLine);
                    }
                    this.updatingRange = this.updatingRange.GetIntersectionWith(this.Range);
                }
            }
            else
            {
                this.IsChanged = true;
                this.TextVersion++;
                this.MarkLinesAsChanged(args.ChangedRange);
                if (this.wordWrap)
                {
                    this.RecalcWordWrap(args.ChangedRange.Start.iLine, args.ChangedRange.End.iLine);
                }
                base.OnTextChanged(args);
                if (this.delayedTextChangedRange == null)
                {
                    this.delayedTextChangedRange = args.ChangedRange.Clone();
                }
                else
                {
                    this.delayedTextChangedRange = this.delayedTextChangedRange.GetUnionWith(args.ChangedRange);
                }
                this.needRiseTextChangedDelayed = true;
                this.ResetTimer(this.timer2);
                this.OnSyntaxHighlight(args);
                if (this.TextChanged != null)
                {
                    this.TextChanged(this, args);
                }
                if (this.BindingTextChanged != null)
                {
                    this.BindingTextChanged(this, EventArgs.Empty);
                }
                base.OnTextChanged(EventArgs.Empty);
                this.OnVisibleRangeChanged();
            }
        }

        protected virtual void OnToolTip()
        {
            if (ToolTip == null)
                return;
            if (ToolTipNeeded == null)
                return;

            //get place under mouse
            Place place = PointToPlace(lastMouseCoord);
            //ToolTip.SetToolTip(
            //check distance
            Point p = PlaceToPoint(place);
            if (Math.Abs(p.X - lastMouseCoord.X) > CharWidth * 2 ||
                Math.Abs(p.Y - lastMouseCoord.Y) > CharHeight * 2)
                return;
            //get word under mouse
            var r = new Range(this, place, place);

            string hoveredWord = r.GetFragment("[a-zA-Z]").Text;
            //event handler
            var ea = new ToolTipNeededEventArgs(place, hoveredWord);
            ToolTipNeeded(this, ea);

            if (ea.ToolTipText != null)
            {
                //show tooltip
                ToolTip.ToolTipTitle = ea.ToolTipTitle;
                ToolTip.ToolTipIcon = ea.ToolTipIcon;
                ToolTip.SetToolTip(this, ea.ToolTipText);
                ToolTip.Show(ea.ToolTipText, this, new Point(lastMouseCoord.X, lastMouseCoord.Y + CharHeight));
            }
        }

        protected override bool ProcessMnemonic(char charCode)
        {
            return (this.Focused && (this.ProcessKeyPress(charCode) || base.ProcessMnemonic(charCode)));
        }

        protected override void WndProc(ref Message m)
        {
            if (((m.Msg == 0x114) || (m.Msg == 0x115)) && (m.WParam.ToInt32() != 8))
            {
                this.Invalidate();
            }
            base.WndProc(ref m);
            if (this.ImeAllowed && ((m.Msg == 0x281) && (m.WParam.ToInt32() == 1)))
            {
                ImmAssociateContext(base.Handle, this.m_hImc);
            }
        }

        [DllImport("User32.dll")]
        private static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);

        [DllImport("User32.dll")]
        private static extern bool DestroyCaret();

        [DllImport("User32.dll")]
        private static extern bool HideCaret(IntPtr hWnd);

        [DllImport("User32.dll")]
        private static extern bool SetCaretPos(int x, int y);

        [DllImport("User32.dll")]
        private static extern bool ShowCaret(IntPtr hWnd);

        private void CancelToolTip()
        {
            timer3.Stop();
            if (ToolTip != null && !string.IsNullOrEmpty(ToolTip.GetToolTip(this)))
            {
                ToolTip.Hide(this);
                ToolTip.SetToolTip(this, null);
            }
        }

        private void ClearBracketsPositions()
        {
            this.leftBracketPosition = null;
            this.rightBracketPosition = null;
            this.leftBracketPosition2 = null;
            this.rightBracketPosition2 = null;
        }
        private TextSource CreateTextSource()
        {
            return new TextSource(this);
        }
        private void DoVisibleRectangle(Rectangle rect)
        {
            int num = base.VerticalScroll.Value;
            int num2 = base.VerticalScroll.Value;
            int num3 = base.HorizontalScroll.Value;
            if (rect.Bottom > base.ClientRectangle.Height)
            {
                num2 += rect.Bottom - base.ClientRectangle.Height;
            }
            else if (rect.Top < 0)
            {
                num2 += rect.Top;
            }
            if (rect.Right > base.ClientRectangle.Width)
            {
                num3 += rect.Right - base.ClientRectangle.Width;
            }
            else if (rect.Left < this.LeftIndent)
            {
                num3 += rect.Left - this.LeftIndent;
            }
            if (!this.Multiline)
            {
                num2 = 0;
            }
            num2 = Math.Max(0, num2);
            num3 = Math.Max(0, num3);
            try
            {
                if (!(!base.VerticalScroll.Visible && this.ShowScrollBars))
                {
                    base.VerticalScroll.Value = num2;
                }
                if (!(!base.HorizontalScroll.Visible && this.ShowScrollBars))
                {
                    base.HorizontalScroll.Value = num3;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
            }
            if (this.ShowScrollBars)
            {
                base.AutoScrollMinSize -= new Size(1, 0);
                base.AutoScrollMinSize += new Size(1, 0);
            }
            if (num != base.VerticalScroll.Value)
            {
                this.OnVisibleRangeChanged();
            }
        }

        private void DrawLineChars(PaintEventArgs e, int firstChar, int lastChar, int iLine, int iWordWrapLine, int x, int y)
        {
            Line line = this.lines[iLine];
            LineInfo info = this.lineInfos[iLine];
            int wordWrapStringStartPosition = info.GetWordWrapStringStartPosition(iWordWrapLine);
            int wordWrapStringFinishPosition = info.GetWordWrapStringFinishPosition(iWordWrapLine, line);
            int num3 = x;
            if (num3 < this.LeftIndent)
            {
                firstChar++;
            }
            lastChar = Math.Min(wordWrapStringFinishPosition - wordWrapStringStartPosition, lastChar);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (info.VisibleState == VisibleState.StartOfHiddenBlock)
            {
                this.FoldedBlockStyle.Draw(e.Graphics, new Point(num3 + (firstChar * this.CharWidth), y), new Range(this, wordWrapStringStartPosition + firstChar, iLine, (wordWrapStringStartPosition + lastChar) + 1, iLine));
            }
            else
            {
                StyleIndex none = StyleIndex.None;
                int num4 = firstChar - 1;
                for (int i = firstChar; i <= lastChar; i++)
                {
                    StyleIndex style = line[wordWrapStringStartPosition + i].style;
                    if (none != style)
                    {
                        this.FlushRendering(e.Graphics, none, new Point(num3 + ((num4 + 1) * this.CharWidth), y), new Range(this, (wordWrapStringStartPosition + num4) + 1, iLine, wordWrapStringStartPosition + i, iLine));
                        num4 = i - 1;
                        none = style;
                    }
                }
                this.FlushRendering(e.Graphics, none, new Point(num3 + ((num4 + 1) * this.CharWidth), y), new Range(this, (wordWrapStringStartPosition + num4) + 1, iLine, (wordWrapStringStartPosition + lastChar) + 1, iLine));
            }
            if ((this.Selection.End != this.Selection.Start) && (lastChar >= firstChar))
            {
                e.Graphics.SmoothingMode = SmoothingMode.None;
                Range range = new Range(this, wordWrapStringStartPosition + firstChar, iLine, (wordWrapStringStartPosition + lastChar) + 1, iLine);
                range = this.Selection.GetIntersectionWith(range);
                if ((range != null) && (this.SelectionStyle != null))
                {
                    this.SelectionStyle.Draw(e.Graphics, new Point(num3 + ((range.Start.iChar - wordWrapStringStartPosition) * this.CharWidth), y), range);
                }
            }
        }
        private int FindEndOfFoldingBlock(int iStartLine)
        {
            int num = 0x7d0;
            int num2 = 0;
            string foldingStartMarker = this.lines[iStartLine].FoldingStartMarker;
            for (int i = iStartLine; i < this.LinesCount; i++)
            {
                if (this.lines.LineHasFoldingStartMarker(i) && (this.lines[i].FoldingStartMarker == foldingStartMarker))
                {
                    num2++;
                }
                if (this.lines.LineHasFoldingEndMarker(i) && (this.lines[i].FoldingEndMarker == foldingStartMarker))
                {
                    num2--;
                    if (num2 <= 0)
                    {
                        return i;
                    }
                }
                num--;
                if (num < 0)
                {
                    break;
                }
            }
            return -1;
        }
        private VisualMarker FindVisualMarkerForPoint(Point p)
        {
            foreach (VisualMarker marker in this.visibleMarkers)
            {
                if (marker.rectangle.Contains(p))
                {
                    return marker;
                }
            }
            return null;
        }

        private void FlushRendering(Graphics gr, StyleIndex styleIndex, Point pos, Range range)
        {
            if (range.End > range.Start)
            {
                int num = 1;
                bool flag = false;
                for (int i = 0; i < this.Styles.Length; i++)
                {
                    if ((this.Styles[i] != null) && ((((int)styleIndex) & num) != ((int)StyleIndex.None)))
                    {
                        Style style = this.Styles[i];
                        bool flag2 = style is TextStyle;
                        if ((!flag || !flag2) || this.AllowSeveralTextStyleDrawing)
                        {
                            style.Draw(gr, pos, range);
                        }
                        flag |= flag2;
                    }
                    num = num << 1;
                }
                if (!flag)
                {
                    this.DefaultStyle.Draw(gr, pos, range);
                }
            }
        }
        private int GetMaxLineWordWrapedWidth()
        {
            if (this.wordWrap)
            {
                switch (this.wordWrapMode)
                {
                    case WordWrapMode.WordWrapControlWidth:
                    case WordWrapMode.CharWrapControlWidth:
                        return base.ClientSize.Width;

                    case WordWrapMode.WordWrapPreferredWidth:
                    case WordWrapMode.CharWrapPreferredWidth:
                        return ((((this.LeftIndent + (this.PreferredLineWidth * this.CharWidth)) + 2) + this.Padding.Left) + this.Padding.Right);
                }
            }
            return 0x7fffffff;
        }

        private int GetMinStartSpacesCount(int fromLine, int toLine)
        {
            if (fromLine > toLine)
            {
                return 0;
            }
            int num = 0x7fffffff;
            for (int i = fromLine; i <= toLine; i++)
            {
                int startSpacesCount = this.lines[i].StartSpacesCount;
                if (startSpacesCount < num)
                {
                    num = startSpacesCount;
                }
            }
            return num;
        }
        private void GoHome(bool shift)
        {
            this.Selection.BeginUpdate();
            try
            {
                int iLine = this.Selection.Start.iLine;
                int startSpacesCount = this[iLine].StartSpacesCount;
                if (this.Selection.Start.iChar <= startSpacesCount)
                {
                    this.Selection.GoHome(shift);
                }
                else
                {
                    this.Selection.GoHome(shift);
                    for (int i = 0; i < startSpacesCount; i++)
                    {
                        this.Selection.GoRight(shift);
                    }
                }
            }
            finally
            {
                this.Selection.EndUpdate();
            }
        }
        private void HighlightBrackets(char LeftBracket, char RightBracket, ref Range leftBracketPosition, ref Range rightBracketPosition)
        {
            if (this.Selection.Start == this.Selection.End)
            {
                Range range = leftBracketPosition;
                Range range2 = rightBracketPosition;
                Range range3 = this.Selection.Clone();
                int num = 0;
                int num2 = 0x3e8;
                while (range3.GoLeftThroughFolded())
                {
                    if (range3.CharAfterStart == LeftBracket)
                    {
                        num++;
                    }
                    if (range3.CharAfterStart == RightBracket)
                    {
                        num--;
                    }
                    if (num == 1)
                    {
                        range3.End = new Place(range3.Start.iChar + 1, range3.Start.iLine);
                        leftBracketPosition = range3;
                        break;
                    }
                    num2--;
                    if (num2 <= 0)
                    {
                        break;
                    }
                }
                range3 = this.Selection.Clone();
                num = 0;
                num2 = 0x3e8;
                do
                {
                    if (range3.CharAfterStart == LeftBracket)
                    {
                        num++;
                    }
                    if (range3.CharAfterStart == RightBracket)
                    {
                        num--;
                    }
                    if (num == -1)
                    {
                        range3.End = new Place(range3.Start.iChar + 1, range3.Start.iLine);
                        rightBracketPosition = range3;
                        break;
                    }
                    num2--;
                }
                while ((num2 > 0) && range3.GoRightThroughFolded());
                if ((range != leftBracketPosition) || (range2 != rightBracketPosition))
                {
                    this.Invalidate();
                }
            }
        }

        private void HighlightFoldings()
        {
            int startFoldingLine = this.startFoldingLine;
            int endFoldingLine = this.endFoldingLine;
            this.startFoldingLine = -1;
            this.endFoldingLine = -1;
            string foldingStartMarker = null;
            int num3 = 0;
            for (int i = this.Selection.Start.iLine; i >= Math.Max(this.Selection.Start.iLine - 0x7d0, 0); i--)
            {
                bool flag = this.lines.LineHasFoldingStartMarker(i);
                bool flag2 = this.lines.LineHasFoldingEndMarker(i);
                if (!flag2 || !flag)
                {
                    if (flag)
                    {
                        num3--;
                        if (num3 == -1)
                        {
                            this.startFoldingLine = i;
                            foldingStartMarker = this.lines[i].FoldingStartMarker;
                            break;
                        }
                    }
                    if (flag2 && (i != this.Selection.Start.iLine))
                    {
                        num3++;
                    }
                }
            }
            if (this.startFoldingLine >= 0)
            {
                this.endFoldingLine = this.FindEndOfFoldingBlock(this.startFoldingLine);
                if (this.endFoldingLine == this.startFoldingLine)
                {
                    this.endFoldingLine = -1;
                }
            }
            if ((this.startFoldingLine != startFoldingLine) || (this.endFoldingLine != endFoldingLine))
            {
                this.OnFoldingHighlightChanged();
            }
        }
        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.Name = "FastColoredTextBox";
            base.ResumeLayout(false);
        }

        private void InitTextSource(TextSource ts)
        {
            if (this.lines != null)
            {
                ts.LineInserted -= new EventHandler<LineInsertedEventArgs>(this.ts_LineInserted);
                ts.LineRemoved -= new EventHandler<LineRemovedEventArgs>(this.ts_LineRemoved);
                ts.TextChanged -= new EventHandler<TextSource.TextChangedEventArgs>(this.ts_TextChanged);
                ts.RecalcNeeded -= new EventHandler<TextSource.TextChangedEventArgs>(this.ts_RecalcNeeded);
                ts.TextChanging -= new EventHandler<TextChangingEventArgs>(this.ts_TextChanging);
                this.lines.Dispose();
            }
            this.lineInfos.Clear();
            this.lines = ts;
            if (ts != null)
            {
                ts.LineInserted += new EventHandler<LineInsertedEventArgs>(this.ts_LineInserted);
                ts.LineRemoved += new EventHandler<LineRemovedEventArgs>(this.ts_LineRemoved);
                ts.TextChanged += new EventHandler<TextSource.TextChangedEventArgs>(this.ts_TextChanged);
                ts.RecalcNeeded += new EventHandler<TextSource.TextChangedEventArgs>(this.ts_RecalcNeeded);
                ts.TextChanging += new EventHandler<TextChangingEventArgs>(this.ts_TextChanging);
                while (this.lineInfos.Count < ts.Count)
                {
                    this.lineInfos.Add(new LineInfo(-1));
                }
            }
            this.isChanged = false;
            this.needRecalc = true;
        }

        private void InsertChar(char c)
        {
            this.lines.Manager.BeginAutoUndoCommands();
            try
            {
                if (this.Selection.Start != this.Selection.End)
                {
                    this.lines.Manager.ExecuteCommand(new ClearSelectedCommand(this.TextSource));
                }
                this.lines.Manager.ExecuteCommand(new InsertCharCommand(this.TextSource, c));
            }
            finally
            {
                this.lines.Manager.EndAutoUndoCommands();
            }
            this.Invalidate();
        }
        private void MarkLinesAsChanged(Range range)
        {
            for (int i = range.Start.iLine; i <= range.End.iLine; i++)
            {
                if ((i >= 0) && (i < this.lines.Count))
                {
                    this.lines[i].IsChanged = true;
                }
            }
        }
        private void MYTextBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (lastMouseCoord != e.Location)
            {
                CancelToolTip();
                timer3.Start();
            }
            lastMouseCoord = e.Location;
        }

        private bool OnKeyPressing(char c)
        {
            KeyPressEventArgs args = new KeyPressEventArgs(c);
            this.OnKeyPressing(args);
            return args.Handled;
        }
        private bool ProcessKeyPress(char c)
        {
            if (!this.handledChar)
            {
                if (c == ' ')
                {
                    return true;
                }
                if ((c == '\b') && ((this.lastModifiers & Keys.Alt) != Keys.None))
                {
                    return true;
                }
                if ((char.IsControl(c) && (c != '\r')) && (c != '\t'))
                {
                    return false;
                }
                if (!(!this.ReadOnly && base.Enabled))
                {
                    return false;
                }
                if ((((this.lastModifiers != Keys.None) && (this.lastModifiers != Keys.Shift)) && ((this.lastModifiers != (Keys.Alt | Keys.Control)) && (this.lastModifiers != (Keys.Alt | Keys.Control | Keys.Shift)))) && ((this.lastModifiers != Keys.Alt) || char.IsLetterOrDigit(c)))
                {
                    return false;
                }
                char ch = c;
                if (this.OnKeyPressing(ch))
                {
                    return true;
                }
                if (!((c != '\r') || this.AcceptsReturn))
                {
                    return false;
                }
                if (c == '\t')
                {
                    if (!this.AcceptsTab)
                    {
                        return false;
                    }
                    if (this.Selection.Start.iLine == this.Selection.End.iLine)
                    {
                        this.ClearSelected();
                        int count = this.TabLength - (this.Selection.Start.iChar % this.TabLength);
                        if (this.IsReplaceMode)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                this.Selection.GoRight(true);
                            }
                            this.Selection.Inverse();
                        }
                        this.InsertText(new string(' ', count));
                    }
                    else if ((this.lastModifiers & Keys.Shift) == Keys.None)
                    {
                        this.IncreaseIndent();
                    }
                }
                else
                {
                    if (c == '\r')
                    {
                        c = '\n';
                    }
                    if (this.IsReplaceMode)
                    {
                        this.Selection.GoRight(true);
                        this.Selection.Inverse();
                    }
                    this.InsertChar(c);
                    if (this.AutoIndent)
                    {
                        this.DoCaretVisible();
                        int num3 = this.CalcAutoIndent(this.Selection.Start.iLine);
                        if (this[this.Selection.Start.iLine].AutoIndentSpacesNeededCount != num3)
                        {
                            this.DoAutoIndent(this.Selection.Start.iLine);
                            this[this.Selection.Start.iLine].AutoIndentSpacesNeededCount = num3;
                        }
                    }
                }
                this.DoCaretVisible();
                this.Invalidate();
                this.OnKeyPressed(ch);
            }
            return true;
        }
        private void Recalc()
        {
            if (this.needRecalc)
            {
                this.needRecalc = false;
                this.LeftIndent = this.LeftPadding;
                long num = (this.LinesCount + this.lineNumberStartValue) - 1L;
                int num2 = 2 + ((num > 0L) ? ((int)Math.Log10((double)num)) : 0);
                if (base.Created)
                {
                    if (this.ShowLineNumbers)
                    {
                        this.LeftIndent += ((num2 * this.CharWidth) + 8) + 1;
                    }
                }
                else
                {
                    this.needRecalc = true;
                }
                this.wordWrapLinesCount = 0;
                this.maxLineLength = this.RecalcMaxLineLength();
                int width = (((this.LeftIndent + (this.maxLineLength * this.CharWidth)) + 2) + this.Padding.Left) + this.Padding.Right;
                if (this.wordWrap)
                {
                    switch (this.WordWrapMode)
                    {
                        case WordWrapMode.WordWrapControlWidth:
                        case WordWrapMode.CharWrapControlWidth:
                            this.maxLineLength = Math.Min(this.maxLineLength, (((base.ClientSize.Width - this.LeftIndent) - this.Padding.Left) - this.Padding.Right) / this.CharWidth);
                            width = 0;
                            break;

                        case WordWrapMode.WordWrapPreferredWidth:
                        case WordWrapMode.CharWrapPreferredWidth:
                            this.maxLineLength = Math.Min(this.maxLineLength, this.PreferredLineWidth);
                            width = (((this.LeftIndent + (this.PreferredLineWidth * this.CharWidth)) + 2) + this.Padding.Left) + this.Padding.Right;
                            break;
                    }
                }
                this.AutoScrollMinSize = new Size(width, ((this.wordWrapLinesCount * this.CharHeight) + this.Padding.Top) + this.Padding.Bottom);
            }
        }

        private int RecalcMaxLineLength()
        {
            int num = 0;
            TextSource lines = this.lines;
            int count = lines.Count;
            int charHeight = this.CharHeight;
            int top = this.Padding.Top;
            for (int i = 0; i < count; i++)
            {
                int lineLength = lines.GetLineLength(i);
                LineInfo info = this.lineInfos[i];
                if ((lineLength > num) && (info.VisibleState == VisibleState.Visible))
                {
                    num = lineLength;
                }
                info.startY = (this.wordWrapLinesCount * charHeight) + top;
                this.wordWrapLinesCount += info.WordWrapStringsCount;
                this.lineInfos[i] = info;
            }
            return num;
        }

        private void RecalcScrollByOneLine(int iLine)
        {
            if (iLine < this.lines.Count)
            {
                int count = this.lines[iLine].Count;
                if (!((this.maxLineLength >= count) || this.WordWrap))
                {
                    this.maxLineLength = count;
                }
                int width = (((this.LeftIndent + (count * this.CharWidth)) + 2) + this.Padding.Left) + this.Padding.Right;
                if (this.AutoScrollMinSize.Width < width)
                {
                    this.AutoScrollMinSize = new Size(width, this.AutoScrollMinSize.Height);
                }
            }
        }

        private void RecalcWordWrap(int fromLine, int toLine)
        {
            int maxCharsPerLine = 0;
            bool charWrap = false;
            switch (this.WordWrapMode)
            {
                case WordWrapMode.WordWrapControlWidth:
                    maxCharsPerLine = (((base.ClientSize.Width - this.LeftIndent) - this.Padding.Left) - this.Padding.Right) / this.CharWidth;
                    break;

                case WordWrapMode.WordWrapPreferredWidth:
                    maxCharsPerLine = this.PreferredLineWidth;
                    break;

                case WordWrapMode.CharWrapControlWidth:
                    maxCharsPerLine = (((base.ClientSize.Width - this.LeftIndent) - this.Padding.Left) - this.Padding.Right) / this.CharWidth;
                    charWrap = true;
                    break;

                case WordWrapMode.CharWrapPreferredWidth:
                    maxCharsPerLine = this.PreferredLineWidth;
                    charWrap = true;
                    break;
            }
            for (int i = fromLine; i <= toLine; i++)
            {
                if (this.lines.IsLineLoaded(i))
                {
                    if (!this.wordWrap)
                    {
                        LineInfo info2 = this.lineInfos[i];
                        info2.CutOffPositions.Clear();
                    }
                    else
                    {
                        LineInfo info = this.lineInfos[i];
                        info.CalcCutOffs(maxCharsPerLine, this.ImeAllowed, charWrap, this.lines[i]);
                        this.lineInfos[i] = info;
                    }
                }
            }
            this.needRecalc = true;
        }
        private void RemoveSpacesAfterCaret()
        {
            if (this.Selection.Start == this.Selection.End)
            {
                Place start = this.Selection.Start;
                while (this.Selection.CharAfterStart == ' ')
                {
                    this.Selection.GoRight(true);
                }
                this.ClearSelected();
            }
        }

        private void ResetTimer(System.Windows.Forms.Timer timer)
        {
            timer.Stop();
            if (base.IsHandleCreated)
            {
                timer.Start();
            }
        }
        private void SetAsCurrentTB()
        {
            this.TextSource.CurrentTB = this;
        }
        private void ShowPrintDialog(object sender, WebBrowserNavigatedEventArgs e)
        {
            WebBrowser browser = sender as WebBrowser;
            PrintDialogSettings tag = browser.Tag as PrintDialogSettings;
            string html = new ExportToHTML { UseBr = true, UseForwardNbsp = true, UseNbsp = false, UseStyleTag = false }.GetHtml(tag.printRange);
            browser.Document.Body.InnerHtml = html;
            if (tag.ShowPrintPreviewDialog)
            {
                browser.ShowPrintPreviewDialog();
            }
            else
            {
                if (tag.ShowPageSetupDialog)
                {
                    browser.ShowPageSetupDialog();
                }
                if (tag.ShowPrintDialog)
                {
                    browser.ShowPrintDialog();
                }
                else
                {
                    browser.Print();
                }
            }
            browser.Parent = null;
            browser.Dispose();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            if (this.needRiseSelectionChangedDelayed)
            {
                this.needRiseSelectionChangedDelayed = false;
                this.OnSelectionChangedDelayed();
            }
            if (this.needRiseVisibleRangeChangedDelayed)
            {
                this.needRiseVisibleRangeChangedDelayed = false;
                this.OnVisibleRangeChangedDelayed();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.timer2.Enabled = false;
            if (this.needRiseTextChangedDelayed)
            {
                this.needRiseTextChangedDelayed = false;
                if (this.delayedTextChangedRange != null)
                {
                    this.delayedTextChangedRange = this.Range.GetIntersectionWith(this.delayedTextChangedRange);
                    this.delayedTextChangedRange.Expand();
                    this.OnTextChangedDelayed(this.delayedTextChangedRange);
                    this.delayedTextChangedRange = null;
                }
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            timer3.Stop();
            OnToolTip();
        }

        private void ts_LineInserted(object sender, LineInsertedEventArgs e)
        {
            List<LineInfo> collection = new List<LineInfo>(e.Count);
            for (int i = 0; i < e.Count; i++)
            {
                collection.Add(new LineInfo(-1));
            }
            this.lineInfos.InsertRange(e.Index, collection);
            this.OnLineInserted(e.Index, e.Count);
        }

        private void ts_LineRemoved(object sender, LineRemovedEventArgs e)
        {
            this.lineInfos.RemoveRange(e.Index, e.Count);
            this.OnLineRemoved(e.Index, e.Count, e.RemovedLineUniqueIds);
        }

        private void ts_RecalcNeeded(object sender, TextSource.TextChangedEventArgs e)
        {
            if (((e.iFromLine == e.iToLine) && !this.WordWrap) && (this.lines.Count > 0x186a0))
            {
                this.RecalcScrollByOneLine(e.iFromLine);
            }
            else
            {
                this.needRecalc = true;
            }
        }

        private void ts_TextChanged(object sender, TextSource.TextChangedEventArgs e)
        {
            if (!((e.iFromLine != e.iToLine) || this.WordWrap))
            {
                this.RecalcScrollByOneLine(e.iFromLine);
            }
            else
            {
                this.needRecalc = true;
            }
            this.Invalidate();
            if (this.TextSource.CurrentTB == this)
            {
                this.OnTextChanged(e.iFromLine, e.iToLine);
            }
        }

        private void ts_TextChanging(object sender, TextChangingEventArgs e)
        {
            if (this.TextSource.CurrentTB == this)
            {
                string text = e.InsertingText;
                this.OnTextChanging(ref text);
                e.InsertingText = text;
            }
        }
        private int YtoLineIndex(int y)
        {
            int num = this.lineInfos.BinarySearch(new LineInfo(-10), new LineYComparer(y));
            num = (num < 0) ? (-num - 2) : num;
            if (num < 0)
            {
                return 0;
            }
            if (num > (this.lines.Count - 1))
            {
                return (this.lines.Count - 1);
            }
            return num;
        }
        /// <summary>
        /// ToolTipNeeded event args
        /// </summary>
        public class ToolTipNeededEventArgs : EventArgs
        {
            public ToolTipNeededEventArgs(Place place, string hoveredWord)
            {
                HoveredWord = hoveredWord;
                Place = place;
            }

            public string HoveredWord { get; private set; }

            public Place Place { get; private set; }
            public ToolTipIcon ToolTipIcon { get; set; }

            public string ToolTipText { get; set; }

            public string ToolTipTitle { get; set; }
        }
        private class LineYComparer : IComparer<LineInfo>
        {
            private readonly int Y;

            public LineYComparer(int Y)
            {
                this.Y = Y;
            }

            public int Compare(LineInfo x, LineInfo y)
            {
                if (x.startY == -10)
                {
                    return -y.startY.CompareTo(this.Y);
                }
                return x.startY.CompareTo(this.Y);
            }
        }
    }
}