using MediaProtector.App_Plugins.MediaProtector.Models;
using System;
using System.Linq;
using Umbraco.Core.Composing;
using Umbraco.Core.Events;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using Umbraco.Core.Services;
using Umbraco.Core.Services.Implement;
using Umbraco.Web;


namespace MediaProtector.App_Plugins.MediaProtector.Components {
    public class EventBlocker:IComponent {
        private IScopeProvider _scopeProvider;
        private ILogger _logger;
        private IUmbracoContextFactory _context;
        public EventBlocker(IUmbracoContextFactory context,IScopeProvider scopeProvider,ILogger logger) {
            _scopeProvider = scopeProvider;
            _logger = logger;
            _context = context;
        }

        public void Initialize() {
            MediaService.Saving += MediaService_Saving;
            MediaService.Moving += MediaService_Moving;
            MediaService.Deleting += MediaService_Deleting;
            MediaService.Trashing += MediaService_Trashing;
        }

        private void MediaService_Saving(IMediaService sender,SaveEventArgs<IMedia> eventElement) {

            EventModel saveEvent = null;
            int _currentUserId;

            using(var contextReference = _context.EnsureUmbracoContext()) {
                _currentUserId = contextReference.UmbracoContext.Security.CurrentUser.Id;
            }

            try {

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.eventType == "save");
                    saveEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Failed to get Media Protector setting for save event: " + ex.Message);
            }

            foreach(var mediaItem in eventElement.SavedEntities) {
                if(saveEvent != null) {
                    if(saveEvent.mediaNodes.Contains(mediaItem.Id.ToString())) {
                        if(!saveEvent.userExceptions.Contains(_currentUserId.ToString())) {
                            eventElement.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot save " + mediaItem.Name,EventMessageType.Warning));
                        }
                    }
                }
            }
        }

        private void MediaService_Deleting(IMediaService sender,DeleteEventArgs<IMedia> eventElement) {
            EventModel deleteEvent = null;
            int _currentUserId;

            using(var contextReference = _context.EnsureUmbracoContext()) {
                _currentUserId = contextReference.UmbracoContext.Security.CurrentUser.Id;
            }

            try {

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.eventType == "delete");
                    deleteEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Failed to get Media Protector setting for delete event: " + ex.Message);
            }

            foreach(var mediaItem in eventElement.DeletedEntities) {
                if(deleteEvent != null) {
                    if(deleteEvent.mediaNodes.Contains(mediaItem.Id.ToString())) {
                        if(!deleteEvent.userExceptions.Contains(_currentUserId.ToString())) {
                            eventElement.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot delete " + mediaItem.Name,EventMessageType.Warning));
                        }
                    }
                }
            }
        }

        private void MediaService_Moving(IMediaService sender,MoveEventArgs<IMedia> eventElement) {
            EventModel moveEvent = null;
            int _currentUserId;

            using(var contextReference = _context.EnsureUmbracoContext()) {
                _currentUserId = contextReference.UmbracoContext.Security.CurrentUser.Id;
            }

            try {

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.eventType == "move");
                    moveEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Failed to get Media Protector setting for move event: " + ex.Message);
            }

            foreach(var mediaItem in eventElement.MoveInfoCollection) {
                if(moveEvent != null) {
                    if(moveEvent.mediaNodes.Contains(mediaItem.Entity.Id.ToString())) {
                        if(!moveEvent.userExceptions.Contains(_currentUserId.ToString())) {
                            eventElement.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot move " + mediaItem.Entity.Name,EventMessageType.Warning));
                        }
                    }
                }
            }
        }

        private void MediaService_Trashing(IMediaService sender,MoveEventArgs<IMedia> eventElement) {
            EventModel trashEvent = null;
            int _currentUserId;

            using(var contextReference = _context.EnsureUmbracoContext()) {
                _currentUserId = contextReference.UmbracoContext.Security.CurrentUser.Id;
            }

            try {

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.eventType == "trash");
                    trashEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Failed to get Media Protector setting for trash event: " + ex.Message);
            }

            foreach(var mediaItem in eventElement.MoveInfoCollection) {
                if(trashEvent != null) {
                    if(trashEvent.mediaNodes.Contains(mediaItem.Entity.Id.ToString())) {
                        if(!trashEvent.userExceptions.Contains(_currentUserId.ToString())) {
                            eventElement.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot trash " + mediaItem.Entity.Name,EventMessageType.Warning));
                        }
                    }
                }
            }
        }
        public void Terminate() {
            // Nothing to terminate
        }
    }
}