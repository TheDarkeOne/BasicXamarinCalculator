using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XamarinCalculator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
            InputText = "";
            OutputText = "";
            OnNumberPressed = new DelegateCommand<object>(OnSelect);
        }

        private bool equalState { get; set; }

        string inputText;
        public string InputText
        {
            get => inputText;
            set { SetProperty(ref inputText, value); }
        }

        string outputText;
        public string OutputText
        {
            get => outputText;
            set { SetProperty(ref outputText, value); }
        }

        private DelegateCommand clearPressed;
        public DelegateCommand ClearPressed =>
            clearPressed ?? (clearPressed = new DelegateCommand( () =>
            {
                InputText = "";
                OutputText = "";
                equalState = false;
            }));

        private DelegateCommand equalPressed;
        public DelegateCommand EqualPressed =>
            equalPressed ?? (equalPressed = new DelegateCommand(() =>
            {
                if(!equalState)
                { 
                equalState = true;
                }
                var calcOperator = CheckOperator(InputText);
                CalculateOutput(InputText, calcOperator);
            }));

        public DelegateCommand<object> OnNumberPressed { get; private set; }

        public void OnSelect(object args)
        {
            if (equalState)
            {
                InputText = "";
                equalState = false;
            }
            InputText += args;
        }

        public void CalculateOutput(string input, string calcOperator)
        {
            string[] newInput;
            string tempString1;
            string tempString2;
            double Input1;
            double Input2;
            double Output;

            switch (calcOperator) 
            {
                case "+":
                    newInput = input.Split('+');
                    tempString1 = newInput[0];
                    Input1 = Int32.Parse(tempString1);


                    tempString2 = newInput[1];
                    Input2 = Int32.Parse(tempString2);

                    Output = Input1 + Input2;
                    OutputText = Output.ToString();
                    break;
                case "-":
                    newInput = input.Split('-');
                    tempString1 = newInput[0];
                    Input1 = Int32.Parse(tempString1);


                    tempString2 = newInput[1];
                    Input2 = Int32.Parse(tempString2);

                    Output = Input1 - Input2;
                    OutputText = Output.ToString();
                    break;
                case "*":
                    newInput = input.Split('*');
                    tempString1 = newInput[0];
                    Input1 = Int32.Parse(tempString1);


                    tempString2 = newInput[1];
                    Input2 = Int32.Parse(tempString2);

                    Output = Input1 * Input2;
                    OutputText = Output.ToString();
                    break;
                case "/":
                    newInput = input.Split('/');
                    tempString1 = newInput[0];
                    Input1 = Int32.Parse(tempString1);


                    tempString2 = newInput[1];
                    Input2 = Int32.Parse(tempString2);

                    Output = Input1 / Input2;
                    OutputText = Output.ToString();
                    break;
                default:
                    break;
            }

        }

        public string CheckOperator(string input)
        { 
            if (input.Contains('+'))
            {
                return "+";
            } else if(input.Contains('-'))
            {
                return "-";
            }
            else if(input.Contains('*'))
            {
                return "*";
            }
            else if(input.Contains('/'))
            {
                return "/";
            }
            else
            {
                return "0";
            }
        }
    }
}
