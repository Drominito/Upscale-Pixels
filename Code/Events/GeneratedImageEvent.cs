using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upscale_Pixels.Code.Events
{
    public class EventPublisher
    {
        public event EventHandler FileCreatedEvent;

        public void ExecuteEvent()
        {
            OnFileCreatedEvent();
        }

        public void OnFileCreatedEvent()
        {
            FileCreatedEvent.Invoke(this, EventArgs.Empty);
        }
    }

    public class EventSubscriber
    {
        public EventSubscriber(EventPublisher publisher)
        {
            publisher.FileCreatedEvent += HandleEvent;
        }

        private void HandleEvent(object? sender, EventArgs e)
        {
            //UsedImagesHistoryData 

        }
    }
}