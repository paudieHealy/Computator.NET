using Accord.Collections;
using Computator.NET.Core.Abstract.Controls;
using Computator.NET.Core.Abstract.Services;
using Computator.NET.Core.Model;
using Computator.NET.Core.Properties;
using Computator.NET.DataTypes;
using Computator.NET.DataTypes.Charts;

namespace Computator.NET.Core.Menu.Commands.FileCommands
{
    public class PrintPreviewCommand : CommandBase
    {
        private ICanFileEdit customFunctionsCodeEditor;
        private ISharedViewState _sharedViewState;
        private ICanFileEdit scriptingCodeEditor;
        private IApplicationManager _applicationManager;
        private ReadOnlyDictionary<CalculationsMode, IChart> _charts;

        public PrintPreviewCommand(ICanFileEdit scriptingCodeEditor, ICanFileEdit customFunctionsCodeEditor, ISharedViewState sharedViewState, IApplicationManager applicationManager, ReadOnlyDictionary<CalculationsMode, IChart> charts)
        {
            Icon = Resources.printPreviewToolStripMenuItemImage;
            Text = MenuStrings.printPreviewToolStripMenuItem_Text;
            ToolTip = MenuStrings.printPreviewToolStripMenuItem_Text;

            this.scriptingCodeEditor = scriptingCodeEditor;
            this.customFunctionsCodeEditor = customFunctionsCodeEditor;
            _sharedViewState = sharedViewState;
            _applicationManager = applicationManager;
            _charts = charts;
        }


        public override void Execute()
        {
            switch ((int) _sharedViewState.CurrentView)
            {
                case 0:
                    //if (_calculationsMode == CalculationsMode.Real)
                    _charts[_sharedViewState.CalculationsMode].PrintPreview();
                    // else
                    //  SendStringAsKey("^P");
                    break;

                case 4:
                    //scriptingCodeEditor();

                    break;

                case 5:
                    //this.customFunctionsCodeEditor
                    break;

                default:
                    _applicationManager.SendStringAsKey("^P"); //this.chart2d.Printing.PrintPreview();
                    break;
            }
        }
    }
}