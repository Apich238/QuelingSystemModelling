using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RZ
{
    internal class Step
    {
        private Step next;
        private List<State> WorkersStates;
        private int WorkersCountLimit;
    }
    public enum State
    {
        free,//может принять заявку
        busy,//обрабатывает заявку
        done//заявка обработана. ожидается возможность её передачи дальше
    }

}
