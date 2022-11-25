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

        private void MediaService_Saving(IMediaService sender,SaveEventArgs<IMedia> eventArgs) {            

            try {

                EventModel saveEvent = null;
                int _currentUserId;

                using(var contextReference = _context.EnsureUmbracoContext()) {
                    _currentUserId = contextReference.UmbracoContext.Security.IsAuthenticated() ? contextReference.UmbracoContext.Security.CurrentUser.Id : -1;
                }

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.id == 1);
                    saveEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }

                foreach(var mediaItem in eventArgs.SavedEntities) {
                    if(saveEvent != null) {
                        if(saveEvent.mediaNodes.Split(',').Contains(mediaItem.Id.ToString()) || saveEvent.disableEvent) {
                            if(!saveEvent.userExceptions.Split(',').Contains(_currentUserId.ToString())) {
                                eventArgs.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot save " + mediaItem.Name,EventMessageType.Error));
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Media Protector failed for save action: " + ex.Message);
            }            
        }

        private void MediaService_Deleting(IMediaService sender,DeleteEventArgs<IMedia> eventArgs) {            

            try {

                EventModel deleteEvent = null;
                int _currentUserId;

                using(var contextReference = _context.EnsureUmbracoContext()) {
                    _currentUserId = contextReference.UmbracoContext.Security.IsAuthenticated() ? contextReference.UmbracoContext.Security.CurrentUser.Id : -1;
                }

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.id == 4);
                    deleteEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }

                foreach(var mediaItem in eventArgs.DeletedEntities) {
                    if(deleteEvent != null) {
                        if(deleteEvent.mediaNodes.Split(',').Contains(mediaItem.Id.ToString()) || deleteEvent.disableEvent) {
                            if(!deleteEvent.userExceptions.Split(',').Contains(_currentUserId.ToString())) {
                                eventArgs.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot delete " + mediaItem.Name,EventMessageType.Error));
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Media Protector failed for delete action: " + ex.Message);
            }            
        }

        private void MediaService_Moving(IMediaService sender,MoveEventArgs<IMedia> eventArgs) {           

            try {

                EventModel moveEvent = null;
                int _currentUserId;

                using(var contextReference = _context.EnsureUmbracoContext()) {
                    _currentUserId = contextReference.UmbracoContext.Security.IsAuthenticated() ? contextReference.UmbracoContext.Security.CurrentUser.Id : -1;
                }

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.id == 2);
                    moveEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }

                foreach(var mediaItem in eventArgs.MoveInfoCollection) {
                    if(moveEvent != null) {
                        if(moveEvent.mediaNodes.Split(',').Contains(mediaItem.Entity.Id.ToString()) || moveEvent.disableEvent) {
                            if(!moveEvent.userExceptions.Split(',').Contains(_currentUserId.ToString())) {
                                eventArgs.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot move " + mediaItem.Entity.Name,EventMessageType.Error));
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Media Protector failed for move action: " + ex.Message);
            }            
        }

        private void MediaService_Trashing(IMediaService sender,MoveEventArgs<IMedia> eventArgs) {            

            try {

                EventModel trashEvent = null;
                int _currentUserId;

                using(var contextReference = _context.EnsureUmbracoContext()) {
                    _currentUserId = contextReference.UmbracoContext.Security.IsAuthenticated() ? contextReference.UmbracoContext.Security.CurrentUser.Id : -1;
                }

                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.id == 3);
                    trashEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }

                foreach(var mediaItem in eventArgs.MoveInfoCollection) {
                    if(trashEvent != null) {
                        if(trashEvent.mediaNodes.Split(',').Contains(mediaItem.Entity.Id.ToString()) || trashEvent.disableEvent) {
                            if(!trashEvent.userExceptions.Split(',').Contains(_currentUserId.ToString())) {
                                eventArgs.CancelOperation(new EventMessage("Action rejected. Contact website admin","You cannot trash " + mediaItem.Entity.Name,EventMessageType.Error));
                            }
                        }
                    }
                }
            }
            catch(Exception ex) {
                _logger.Error<EventModel>("Media Protector failed  trash action: " + ex.Message);
            }            
        }
        public void Terminate() {
            // Nothing to terminate
        }
    }
}