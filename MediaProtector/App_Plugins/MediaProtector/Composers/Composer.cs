using MediaProtector.App_Plugins.MediaProtector.Controllers;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace MediaProtector.App_Plugins.MediaProtector.Components {
    public class Composer:IUserComposer {
        public void Compose(Composition composition) {
            //Append our component to the collection of Components
            // It will be the last one to be run
            composition.Components().Append<InitializePlan>();
            composition.Components().Append<EventBlocker>();
            composition.Register<MediaProtectorApiController>();
        }
    }
}