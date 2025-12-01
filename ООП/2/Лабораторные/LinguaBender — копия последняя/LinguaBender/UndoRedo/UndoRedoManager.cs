using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinguaBender.UndoRedo
{
    public class UndoRedoManager
    {
        private readonly Stack<ICommandAction> _undoStack = new();
        private readonly Stack<ICommandAction> _redoStack = new();

        public void ExecuteAction(ICommandAction action)
        {
            action.Execute();
            _undoStack.Push(action);
            _redoStack.Clear();       //чистим после действия
        }

        public void Undo()
        {
            if (_undoStack.Count > 0)
            {
                var action = _undoStack.Pop();
                action.UnExecute();
                _redoStack.Push(action);
            }
        }

        public void Redo()
        {
            if (_redoStack.Count > 0)
            {
                var action = _redoStack.Pop();
                action.Execute();
                _undoStack.Push(action);
            }
        }
    }
}
