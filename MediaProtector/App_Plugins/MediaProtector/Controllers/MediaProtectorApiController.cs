using MediaProtector.App_Plugins.MediaProtector.Models;
using System;
using System.Linq;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Scoping;
using Umbraco.Web.Editors;

namespace MediaProtector.App_Plugins.MediaProtector.Controllers {
    public class MediaProtectorApiController:UmbracoAuthorizedJsonController {
        private readonly IScopeProvider _scopeProvider;
        public MediaProtectorApiController(IScopeProvider scopeProvider) {
            _scopeProvider = scopeProvider;
        }

        public EventModel GetSaveEventModel() {
            try {
                EventModel value;
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                    .Where<EventModel>(x => x.eventType == "save");
                    value = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
                return value;
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to get Media Protector setting for save event",ex.Message);
            }
            return null;
        }

        public EventModel UpdateSaveEventModel(EventModel model) {

            try {
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {

                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.eventType == "save");

                    EventModel saveEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                    saveEvent.lastEdited = DateTime.Now;
                    saveEvent.lastEditedBy = UmbracoContext.Security.CurrentUser.Name;
                    saveEvent.mediaNodes = model.mediaNodes;
                    saveEvent.userExceptions = model.userExceptions;
                    scope.Database.Update(saveEvent);
                }
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to update Media Protector settings for the save event",ex.Message);
            }
            return model;
        }

        public EventModel GetMoveEventModel() {
            try {
                EventModel value;
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>().Where<EventModel>(x => x.eventType == "move");
                    value = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
                return value;
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to get Media Protector setting for move event",ex.Message);
            }
            return null;
        }

        public EventModel UpdateMoveEventModel(EventModel model) {

            try {
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {

                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>().Where<EventModel>(x => x.eventType == "move");

                    EventModel moveEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                    moveEvent.lastEdited = DateTime.Now;
                    moveEvent.lastEditedBy = UmbracoContext.Security.CurrentUser.Name;
                    moveEvent.mediaNodes = model.mediaNodes;
                    moveEvent.userExceptions = model.userExceptions;
                    scope.Database.Update(moveEvent);
                }
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to update Media Protector settings for the move event",ex.Message);
            }
            return model;
        }

        public EventModel GetTrashEventModel() {
            try {
                EventModel value;
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>().Where<EventModel>(x => x.eventType == "trash");
                    value = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
                return value;
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to get Media Protector setting for trash event",ex.Message);
            }
            return null;
        }

        public EventModel UpdateTrashEventModel(EventModel model) {

            try {
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {

                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>().Where<EventModel>(x => x.eventType == "trash");

                    EventModel trashEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                    trashEvent.lastEdited = DateTime.Now;
                    trashEvent.lastEditedBy = UmbracoContext.Security.CurrentUser.Name;
                    trashEvent.mediaNodes = model.mediaNodes;
                    trashEvent.userExceptions = model.userExceptions;
                    scope.Database.Update(trashEvent);
                }
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to update Media Protector settings for the trash event",ex.Message);
            }
            return model;
        }

        public EventModel GetDeleteEventModel() {
            try {
                EventModel value;
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>().Where<EventModel>(x => x.eventType == "delete");
                    value = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
                return value;
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to get Media Protector setting for delete event",ex.Message);
            }
            return null;
        }

        public EventModel UpdateDeleteEventModel(EventModel model) {

            try {
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {

                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>().Where<EventModel>(x => x.eventType == "delete");

                    EventModel deleteEvent = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                    deleteEvent.lastEdited = DateTime.Now;
                    deleteEvent.lastEditedBy = UmbracoContext.Security.CurrentUser.Name;
                    deleteEvent.mediaNodes = model.mediaNodes;
                    deleteEvent.userExceptions = model.userExceptions;
                    scope.Database.Update(deleteEvent);
                }
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to update Media Protector settings for the delete event",ex.Message);
            }
            return model;
        }

    }
}