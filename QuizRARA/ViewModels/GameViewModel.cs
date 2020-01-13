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
        public async Task<ResultObject> CreateQuestion(string category, string difficulty)
        {
            _resultObject = new ResultObject();
            await _resultObject.CreateQuestion(category, difficulty);
            return _resultObject;
        }
    }
}
