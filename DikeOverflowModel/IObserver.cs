using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DikeOverflowModel
{
    public interface IObserver
    {
        protected List<IObservable> Subscribers { get; }
        public void Notify();
        public void Subscribe(IObservable subscriber);
    }
}
