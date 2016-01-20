namespace FastColoredTextBoxNS
{
    using System;
    using System.Collections.Generic;

    internal class CommandManager
    {
        private readonly int maxHistoryLength = 200;
        private int autoUndoCommands = 0;
        private int disabledCommands = 0;
        private LimitedStack<UndoableCommand> history;
        private Stack<UndoableCommand> redoStack = new Stack<UndoableCommand>();

        public CommandManager(TextSource ts)
        {
            this.history = new LimitedStack<UndoableCommand>(this.maxHistoryLength);
            this.TextSource = ts;
        }

        public bool RedoEnabled
        {
            get
            {
                return (this.redoStack.Count > 0);
            }
        }

        public TextSource TextSource { get; private set; }

        public bool UndoEnabled
        {
            get
            {
                return (this.history.Count > 0);
            }
        }

        public void BeginAutoUndoCommands()
        {
            this.autoUndoCommands++;
        }

        public void EndAutoUndoCommands()
        {
            this.autoUndoCommands--;
            if ((this.autoUndoCommands == 0) && (this.history.Count > 0))
            {
                this.history.Peek().autoUndo = false;
            }
        }

        public void ExecuteCommand(Command cmd)
        {
            if (this.disabledCommands <= 0)
            {
                if (cmd is UndoableCommand)
                {
                    (cmd as UndoableCommand).autoUndo = this.autoUndoCommands > 0;
                    this.history.Push(cmd as UndoableCommand);
                }
                try
                {
                    cmd.Execute();
                }
                catch (ArgumentOutOfRangeException)
                {
                    if (cmd is UndoableCommand)
                    {
                        this.history.Pop();
                    }
                }
                this.redoStack.Clear();
                this.TextSource.CurrentTB.OnUndoRedoStateChanged();
            }
        }

        public void Undo()
        {
            if (this.history.Count > 0)
            {
                UndoableCommand item = this.history.Pop();
                this.BeginDisableCommands();
                try
                {
                    item.Undo();
                }
                finally
                {
                    this.EndDisableCommands();
                }
                this.redoStack.Push(item);
            }
            if ((this.history.Count > 0) && this.history.Peek().autoUndo)
            {
                this.Undo();
            }
            this.TextSource.CurrentTB.OnUndoRedoStateChanged();
        }

        internal void ClearHistory()
        {
            this.history.Clear();
            this.redoStack.Clear();
            this.TextSource.CurrentTB.OnUndoRedoStateChanged();
        }

        internal void Redo()
        {
            if (this.redoStack.Count != 0)
            {
                UndoableCommand command;
                this.BeginDisableCommands();
                try
                {
                    command = this.redoStack.Pop();
                    this.TextSource.CurrentTB.Selection.Start = command.sel.Start;
                    this.TextSource.CurrentTB.Selection.End = command.sel.End;
                    command.Execute();
                    this.history.Push(command);
                }
                finally
                {
                    this.EndDisableCommands();
                }
                if (command.autoUndo)
                {
                    this.Redo();
                }
                this.TextSource.CurrentTB.OnUndoRedoStateChanged();
            }
        }

        private void BeginDisableCommands()
        {
            this.disabledCommands++;
        }
        private void EndDisableCommands()
        {
            this.disabledCommands--;
        }
    }
}