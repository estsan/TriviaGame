using TriviaGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaGame.ViewModels
{
    class GameViewModel
    {
        ResultObject _resultObject;
        public void CreateQuestion()
        {
            _resultObject = new ResultObject();
            _resultObject.CreateQuestion();
        }
    }
}
