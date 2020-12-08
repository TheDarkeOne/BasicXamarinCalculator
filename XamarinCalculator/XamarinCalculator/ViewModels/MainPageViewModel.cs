using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinCalculator.Services;
using XamarinCalculator.Shared;

namespace XamarinCalculator.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService, ICredentialService credentialService)
            : base(navigationService)
        {
            Title = "Simple Calculator";
            InputText = "";
            OutputText = "";
            calcOperatorArray = new List<char>();
            inputOneState = true;
            calcOperatorArray.Add('+');
            calcOperatorArray.Add('-');
            calcOperatorArray.Add('*');
            calcOperatorArray.Add('/');
            OnNumberPressed = new DelegateCommand<object>(OnSelect);
            this.credentialService = credentialService ?? throw new ArgumentNullException(nameof(credentialService));
        }

        public async Task InitializeData()
        {
            Credentials = await credentialService.GetCredentialsAsync();
        }

        private IEnumerable<UserCredential> credentials;
        public IEnumerable<UserCredential> Credentials
        {
            get { return credentials; }
            set { SetProperty(ref credentials, value); }
        }

        private List<char> calcOperatorArray { get; }
        private bool equalState { get; set; }
        private bool inputOneState { get; set; }
        private bool inputTwoState { get; set; }
        private double input1 { get; set; }
        private double input2 { get; set; }
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
                input1 = 0;
                input2 = 0;
                equalState = false;
                inputOneState = true;
                inputTwoState = false;
            }));

        private DelegateCommand equalPressed;
        private readonly ICredentialService credentialService;

        public DelegateCommand EqualPressed =>
            equalPressed ?? (equalPressed = new DelegateCommand(() =>
            {
                if(!equalState)
                { 
                equalState = true;
                }
                if(InputText == "8008135*42069") 
                {
                    NavigationService.NavigateAsync(nameof(Views.CredentialListPage));
                }
                var calcOperator = CheckOperator(InputText);
                CalculateOutput(calcOperator);
            }));

        public DelegateCommand<object> OnNumberPressed { get; private set; }

        public char convertToChar(object arg) 
        {
            string temp = (string)arg;
            char[] temparr = temp.ToCharArray();
            char input = temparr[0];
            return input;
        }

        public void EqualReset() 
        {
            if (equalState)
            {
                InputText = "";
                input1 = 0;
                input2 = 0;
                inputOneState = true;
                inputTwoState = false;
                equalState = false;
            }
        }

        public void StateOneData(char input)
        {
            if (calcOperatorArray.Contains(input) && inputOneState)
            {
                input1 = Int32.Parse(InputText);
                inputOneState = false;
                inputTwoState = true;
            }
        }

        public void StateTwoData(object arg, char input)
        {
            if (inputTwoState)
            {
                if (!calcOperatorArray.Contains(input))
                {
                    string tempstring = input2.ToString();
                    if (tempstring == "0")
                    {
                        tempstring = (string)arg;
                    }
                    else
                    {
                        tempstring += arg;
                    }
                    input2 = Int32.Parse(tempstring);
                }
            }
        }
        public void OnSelect(object args)
        {
            char input = convertToChar(args);

            EqualReset();

            StateOneData(input);

            InputText += args;

            StateTwoData(args, input);
        }

        public void CalculateOutput(string calcOperator)
        {
            
            double Output;

            switch (calcOperator) 
            {
                case "+":
                    
                    Output = input1 + input2;
                    OutputText = Output.ToString();
                    break;
                case "-":
                    Output = input1 - input2;
                    OutputText = Output.ToString();
                    break;
                case "*":
                    Output = input1 * input2;
                    OutputText = Output.ToString();
                    break;
                case "/":
                    if (input2 == 0)
                    {
                        Output = 0;
                    }
                    else 
                    { 
                        Output = input1 / input2;
                    }
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
