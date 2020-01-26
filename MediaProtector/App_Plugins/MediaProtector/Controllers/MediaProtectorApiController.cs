using MediaProtector.App_Plugins.MediaProtector.Models;
using System;
using System.Collections.Generic;
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

        public IEnumerable<EventModel> GetAll() {
            try {
                IEnumerable<EventModel> value;
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>();
                    value = scope.Database.Fetch<EventModel>(sql);
                }
                return value;
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to get Media Protector settings",ex.Message);
            }
            return null;
        }
        public EventModel GetById(int id) {
            try {
                EventModel value;
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {
                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                    .Where<EventModel>(x => x.id == id);
                    value = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                }
                return value;
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to get Media Protector settings",ex.Message);
            }
            return null;
        }

        public EventModel Save(EventModel model) {

            try {
                using(var scope = _scopeProvider.CreateScope(autoComplete: true)) {

                    var sql = scope.SqlContext.Sql().Select("*").From<EventModel>()
                                    .Where<EventModel>(x => x.id == model.id);

                    EventModel value = scope.Database.Fetch<EventModel>(sql).FirstOrDefault();
                    value.disableEvent = model.disableEvent;
                    value.lastEdited = DateTime.Now;
                    value.lastEditedBy = UmbracoContext.Security.CurrentUser.Name;
                    value.mediaNodes = model.mediaNodes;
                    value.userExceptions = model.userExceptions;
                    scope.Database.Update(value);
                }
            }
            catch(Exception ex) {
                Logger.Error<EventModel>("Failed to save Media Protector settings for the" + model.eventType + " action",ex.Message);
            }
            return model;
        }
    }
}