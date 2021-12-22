using Stella.DEVO.Esame.DataAccess.Models;

namespace Stella.DEVO.Esame.Sender
{
    public interface IWorker
    {
        void SendToQueue(InputText message);
    }
}