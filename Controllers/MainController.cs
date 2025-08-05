using Netwise.Interfaces;
using Netwise.Services;
using Netwise.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netwise.Controllers
{
    public class MainController
    {
        private readonly IService _service;
        private readonly TxtFileHandler _fileHandler;
        private readonly MainView _view;

        public MainController(IService service, TxtFileHandler fileHandler, MainView view)
        {
            _service = service;
            _fileHandler = fileHandler;
            _view = view;
        }

        public async Task RunAsync()
        {
            while (true)
            {
                _view.ShowMenu();
                var choice = _view.GetUserChoice();

                switch (choice)
                {
                    case "1":
                        var fact = await _service.GetFact();
                        _fileHandler.SaveFact(fact);
                        _view.DisplayCatFact(fact);
                        break;

                    case "2":
                        _fileHandler.DisplayFile();
                        break;

                    case "3":
                        _view.DisplayMessage("Exiting the application.");
                        return;

                    default:
                        _view.DisplayMessage("Invalid choice, try again.");
                        break;
                }
            }
        }
    }
}
