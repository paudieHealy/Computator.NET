using System;
using Computator.NET.Compilation;
using Computator.NET.DataTypes.Events;
using Computator.NET.DataTypes.Localization;
using Computator.NET.Evaluation;
using Computator.NET.UI.ErrorHandling;
using Computator.NET.UI.Interfaces;

namespace Computator.NET.UI.Presenters
{
    public class ScriptingViewPresenter
    {
        private readonly IErrorHandler _errorHandler;
        private ISharedViewState _sharedViewState;

        private readonly ScriptEvaluator _eval = new ScriptEvaluator();
        private readonly IScriptingView _view;

        public ScriptingViewPresenter(IScriptingView view, IErrorHandler errorHandler, ISharedViewState sharedViewState)
        {
            _view = view;
            _errorHandler = errorHandler;
            _sharedViewState = sharedViewState;
            _view.ProcessClicked += _view_ProcessClicked;
            _sharedViewState.DefaultActions[ViewName.Scripting] = _view_ProcessClicked;
            var solutionExplorerPresenter = new SolutionExplorerPresenter(_view.SolutionExplorerView,
                _view.CodeEditorView, true);
        }

        private void _view_ProcessClicked(object sender, EventArgs e)
        {
            _view.ConsoleOutput = Strings.ConsoleOutput;

            _view.CodeEditorView.ClearHighlightedErrors();
            _sharedViewState.CustomFunctionsEditor.ClearHighlightedErrors();

            try
            {
                var function = _eval.Evaluate(_view.CodeEditorView.Text, _sharedViewState.CustomFunctionsText);
                function.Evaluate(output => _view.AppendToConsole(output));
            }
            catch (Exception ex)
            {
                var exception = ex as CompilationException;
                if (exception != null)
                {
                    _view.CodeEditorView.HighlightErrors(exception.Errors[CompilationErrorPlace.MainCode]);
                }
                ExceptionsHandler.Instance.HandleException(ex, _errorHandler);
            }
        }
    }
}