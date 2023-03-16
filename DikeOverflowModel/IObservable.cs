using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DikeOverflowModel
{
    public interface IObservable
    {
        public void Update(SettingsView s);
    }
}
